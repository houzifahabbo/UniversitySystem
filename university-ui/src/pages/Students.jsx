import { useEffect, useState, useRef } from "react";
import api from "../services/api";
import { App, Spin, Table, Card } from "antd";
import Search from "antd/es/transfer/search";

function Students() {
  const [loading, setLoading] = useState(false);
  const [students, setStudents] = useState([]);
  const [filteredStudents, setFilteredStudents] = useState([]);
  const { message } = App.useApp();
  const hasFetched = useRef(false);

  useEffect(() => {
    const fetchStudents = async () => {
      if (hasFetched.current) return; // Prevent duplicate calls

      try {
        setLoading(true);
        hasFetched.current = true;
        const response = await api.get("/api/students");
        if (response.status === 200) {
          message.success("Students data fetched successfully.");
          setStudents(response.data.result);
          setFilteredStudents(response.data.result);
          console.log(response.data);
        } else {
          message.error("Failed to fetch students data.");
        }
      } catch (error) {
        message.error("Error fetching students data: " + error.message);
        hasFetched.current = false; // Reset for future calls
      } finally {
        setLoading(false);
      }
    };

    fetchStudents();
  }, [message]);

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
    <Card
      title="Students"
      extra={
        <Search
          placeholder="Search students by name or email..."
          onSearch={handleSearch}
          onChange={(e) => handleSearch(e.target.value)}
          allowClear
        />
      }
    >
      <Spin spinning={loading}>
        <Table
          dataSource={filteredStudents}
          columns={columns}
          rowKey="id"
          pagination={{ pageSize: 10 }}
        />
      </Spin>
    </Card>
  );
}
export default Students;
//  <Card title="Students">

//       <Spin spinning={loading}>
//         <Table
//           dataSource={filteredStudents}
//           columns={columns}
//           rowKey="id"
//           pagination={{ pageSize: 10 }}
//         />
//       </Spin>
//     </Card>
//   );
