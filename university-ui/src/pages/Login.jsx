import { Button, Form, Input, message } from "antd";
import api from "../services/api";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

function Login() {
  const [loading, setLoading] = useState(false);
  const [messageApi, contextHolder] = message.useMessage();
  const navigate = useNavigate();

  const handleLogin = async (values) => {
    try {
      setLoading(true);
      const res = await api.post("/api/auth/login", values);
      if (res.status === 200) {
        localStorage.setItem("token", res.data.result);
        navigate("/students");
      } else {
        messageApi.error("Login failed. Please check your credentials.");
      }
    } catch (error) {
      messageApi.error("Login failed. Please try again.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      {contextHolder}
      <Form
        onFinish={handleLogin}
        name="login"
        layout="vertical"
        style={{ maxWidth: 400, margin: "auto", marginTop: 100 }}
      >
        <Form.Item
          label="Email"
          name="email"
          initialValue={"user@test.com"}
          rules={[{ required: true, message: "Please input your email!" }]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          label="Password"
          name="password"
          initialValue={"l*QaO8%Y!FKcS^;"}
          rules={[{ required: true, message: "Please input your password!" }]}
        >
          <Input.Password />
        </Form.Item>
        <Form.Item>
          <Button loading={loading} type="primary" htmlType="submit" block>
            Login
          </Button>
        </Form.Item>
      </Form>
    </div>
  );
}

export default Login;
