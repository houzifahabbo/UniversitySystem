import { Card, message } from "antd";
import { useNavigate } from "react-router-dom";
import StudentForm from "../components/StudentForm";

function AddStudent() {
  const navigate = useNavigate();
  const [messageApi, contextHolder] = message.useMessage();

  const onSuccess = () => {
    messageApi.success("Student added successfully.");
    navigate("/students");
  };
  const onCancel = () => {
    navigate("/students");
  };

  return (
    <>
      {contextHolder}
      <Card title="Add New Student">
        <StudentForm mode="create" onSuccess={onSuccess} onCancel={onCancel} />
      </Card>
    </>
  );
}

export default AddStudent;
