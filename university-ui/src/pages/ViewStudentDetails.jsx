import { Card, message, Spin, Button } from "antd";
import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import api from "../services/api";

function ViewStudentDetails() {
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

  const onBack = () => {
    navigate("/students");
  };

  return (
    <>
      {contextHolder}
      <Spin spinning={loading} fullscreen="true" />
      <Card title=" Student Details">
        <div>
          <p>
            <strong>First Name:</strong> {studentData.firstName}
          </p>
          <p>
            <strong>Last Name:</strong> {studentData.lastName}
          </p>
          <p>
            <strong>Email:</strong> {studentData.email}
          </p>
          <p>
            <strong>Phone:</strong> {studentData.phone}
          </p>
        </div>
        <Button onClick={onBack} style={{ marginTop: 16 }}>
          Back to Students
        </Button>
      </Card>
    </>
  );
}

export default ViewStudentDetails;
