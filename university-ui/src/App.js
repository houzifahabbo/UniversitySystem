import { Layout, Menu } from "antd";
import { Header, Content } from "antd/es/layout/layout";
import { Link, BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { logout } from "./auth/auth";
import Login from "./pages/Login";
import Students from "./pages/Students";
import Grades from "./pages/Grades";
import PrivateRoute from "./components/PrivateRoute";
import RoleRoute from "./components/RoleRoute";

function App() {
  const menuItems = [
    {
      key: "login",
      label: <Link to="/login">Login</Link>,
    },
    {
      key: "students",
      label: <Link to="/students">Students</Link>,
    },
    {
      key: "grades",
      label: <Link to="/grades">Grades</Link>,
    },
    {
      key: "logout",
      label: "Logout",
      onClick: logout,
    },
  ];

  return (
    <Router>
      <Layout>
        <Header style={{ color: "white" }}>
          <Menu
            mode="horizontal"
            defaultSelectedKeys={["login"]}
            theme="dark"
            items={menuItems}
          />
        </Header>
        <Content style={{ padding: "20px" }}>
          <Routes>
            <Route path="/login" element={<Login />} />
            <Route
              path="/students"
              element={
                <PrivateRoute>
                  <Students />
                </PrivateRoute>
              }
            />
            <Route
              path="/grades"
              element={
                <RoleRoute allowedRoles={["Teacher"]}>
                  <Grades />
                </RoleRoute>
              }
            />
            <Route path="/unauthorized" element={<div>Unauthorized</div>} />
          </Routes>
        </Content>
      </Layout>
    </Router>
  );
}

export default App;
