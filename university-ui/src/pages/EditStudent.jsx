import { Card, message, Spin } from "antd";
import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import StudentForm from "../components/StudentForm";
import api from "../services/api";

function EditStudent() {
  const navigate = useNavigate();
  const { id } = useParams();
  const [studentData, setStudentData] = useState({});
  const [loading, setLoading] = useState(true);
  const [messageApi, contextHolder] = message.useMessage();

  useEffect(() => {
    const fetchStudent = async () => {
      try {
        const response = await api.get(`/api/students/${id}`);
        setStudentData(response.data.result);
      } catch (error) {
        messageApi.error("Error fetching student data: " + error.message);
      } finally {
        setLoading(false);
      }
    };
    fetchStudent();
  }, [id, messageApi]);

  const onSuccess = () => {
    messageApi.success("Student updated successfully.");
    navigate("/students");
  };

  const onCancel = () => {
    navigate("/students");
  };

  return (
    <>
      {contextHolder}
      <Spin spinning={loading} fullscreen="true" />
      <Card title="Edit Student">
        <StudentForm
          mode="edit"
          initalValuse={studentData}
          onSuccess={onSuccess}
          onCancel={onCancel}
        />
      </Card>
    </>
  );
}

export default EditStudent;
