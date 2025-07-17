import { useEffect, useState, useRef } from "react";
import api from "../services/api";
import {
  Spin,
  Table,
  Card,
  message,
  Button,
  Flex,
  Col,
  Row,
  Input,
} from "antd";
import { useNavigate } from "react-router-dom";
import StudentCard from "../components/StudentCard";

const { Search } = Input;

function Students() {
  const [loading, setLoading] = useState(false);
  const [students, setStudents] = useState([]);
  const [filteredStudents, setFilteredStudents] = useState([]);
  const [messageApi, contextHolder] = message.useMessage();
  const navigate = useNavigate();

  const hasFetched = useRef(false);

  useEffect(() => {
    const fetchStudents = async () => {
      if (hasFetched.current) return; // Prevent duplicate calls

      try {
        setLoading(true);
        hasFetched.current = true;
        const response = await api.get("/api/students");
        if (response.status === 200) {
          messageApi.success("Students data fetched successfully.");
          setStudents(response.data.result);
          setFilteredStudents(response.data.result);
          console.log(response.data);
        } else {
          messageApi.error("Failed to fetch students data.");
        }
      } catch (error) {
        messageApi.error("Error fetching students data: " + error.message);
        hasFetched.current = false; // Reset for future calls
      } finally {
        setLoading(false);
      }
    };

    fetchStudents();
  }, [messageApi]);
  const handleSearch = (value) => {
    if (!value) {
      setFilteredStudents(students);
    } else {
      const filtered = students.filter(
        (student) =>
          `${student.firstName} ${student.lastName}`
            .toLowerCase()
            .includes(value.toLowerCase()) ||
          student.email.toLowerCase().includes(value.toLowerCase()),
      );
      setFilteredStudents(filtered);
    }
  };

  const NavigateTo = (to = "create", id = null) => {
    if (id && to === "edit") navigate(`/students/${id}/${to}`);
    else if (id && to === "view") navigate(`/students/${id}`);
    else if (to === "create") navigate(`/students/${to}`);
  };

  const handleDelete = async (studentId) => {
    try {
      setLoading(true);
      const response = await api.delete(`/api/students/${studentId}`);
      if (response.status === 200) {
        messageApi.success("Student deleted successfully.");
        setStudents((prev) => prev.filter((s) => s.id !== studentId));
        setFilteredStudents((prev) => prev.filter((s) => s.id !== studentId));
      } else {
        messageApi.error("Failed to delete student.");
      }
    } catch (error) {
      messageApi.error("Error deleting student: " + error.message);
    } finally {
      setLoading(false);
    }
  };

  const columns = [
    {
      title: "ID",
      dataIndex: "id",
      key: "id",
    },
    {
      title: "First Name",
      dataIndex: "firstName",
      key: "firstName",
    },
    {
      title: "Last Name",
      dataIndex: "lastName",
      key: "lastName",
    },
    {
      title: "Email",
      dataIndex: "email",
      key: "email",
    },
    {
      title: "Phone",
      dataIndex: "phone",
      key: "phone",
    },
  ];

  return (
    <>
      {contextHolder}
      <Flex gap="small">
        <Search
          placeholder="Search students by name or email..."
          onSearch={handleSearch}
          onChange={(e) => handleSearch(e.target.value)}
          allowClear
        />
        <Button type="primary" onClick={() => NavigateTo()}>
          Add
        </Button>
      </Flex>
      <Row gutter={[16, 16]} style={{ marginBottom: 20 }}>
        {filteredStudents.map((student) => (
          <Col key={student.id} xs={24} sm={12} md={8} lg={6}>
            <StudentCard
              student={student}
              onViewDetails={() => NavigateTo("view", student.id)}
              onEdit={() => NavigateTo("edit", student.id)}
              onDelete={() => handleDelete(student.id)}
            />
          </Col>
        ))}
      </Row>
      <Card title="Students">
        <Spin spinning={loading}>
          <Table
            dataSource={filteredStudents}
            columns={columns}
            rowKey="id"
            pagination={{ pageSize: 10 }}
          />
        </Spin>
      </Card>
    </>
  );
}

export default Students;
