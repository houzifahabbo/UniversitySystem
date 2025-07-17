import { Form, Input, Button, message } from "antd";
import { useEffect } from "react";
import api from "../services/api";

function StudentForm({
  mode = "create",
  initalValuse = {},
  onSuccess,
  onCancel,
}) {
  const [form] = Form.useForm();
  const [messageApi, contextHolder] = message.useMessage();
  const isCreateMode = mode === "create";

  useEffect(() => {
    if (mode === "edit") {
      form.setFieldsValue(initalValuse);
    }
  }, [mode, initalValuse, form]);

  const handleSubmit = async (values) => {
    try {
      if (mode === "create") {
        await api.post("/api/students", values);
        messageApi.success("Student created successfully.");
      } else if (mode === "edit") {
        await api.put(`/api/students/${initalValuse.id}`, values);
        messageApi.success("Student updated successfully.");
      }
      form.resetFields();
      if (onSuccess) onSuccess();
    } catch (error) {
      messageApi.error(
        `Failed to ${isCreateMode ? "create" : "update"} student: ${
          error.message
        }`,
      );
    }
  };

  return (
    <>
      {contextHolder}
      <Form
        form={form}
        layout="vertical"
        onFinish={handleSubmit}
        initialValues={initalValuse}
      >
        <Form.Item
          label="First Name"
          name="firstName"
          rules={[{ required: true, message: "Please input the first name!" }]}
        >
          <Input placeholder="First Name" />
        </Form.Item>
        <Form.Item
          label="Last Name"
          name="lastName"
          rules={[{ required: true, message: "Please input the last name!" }]}
        >
          <Input placeholder="Last Name" />
        </Form.Item>
        <Form.Item
          label="Email"
          name="email"
          rules={[
            {
              required: isCreateMode,
              message: "Please input the email!",
            },
            { type: "email", message: "Please enter a valid email!" },
          ]}
        >
          <Input placeholder="Email" disabled={!isCreateMode} />
        </Form.Item>
        <Form.Item
          label="Phone"
          name="phone"
          rules={[
            { required: true, message: "Please input the phone number!" },
          ]}
        >
          <Input placeholder="Phone" />
        </Form.Item>
        <Form.Item>
          <Button type="primary" htmlType="submit" style={{ marginRight: 8 }}>
            {mode === "create" ? "Create" : "Update"}
          </Button>
          <Button
            onClick={() => {
              form.resetFields();
              if (onCancel) onCancel();
            }}
          >
            Cancel
          </Button>
        </Form.Item>
      </Form>
    </>
  );
}

export default StudentForm;
