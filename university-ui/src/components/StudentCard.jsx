import { Card, Typography, Button, Popconfirm } from "antd";
const { Text } = Typography;

function StudentCard({ student, onEdit, onDelete, onViewDetails }) {
  return (
    <Card
      title={`${student.firstName} ${student.lastName}`}
      style={{ width: 300, margin: "10px" }}
      actions={[
        <Button color="primary" variant="link" onClick={onViewDetails}>
          View Details
        </Button>,
        <Button color="primary" variant="link" onClick={onEdit}>
          Edit
        </Button>,
        <Popconfirm
          title="Are you sure you want to delete this student?"
          onConfirm={onDelete}
        >
          <Button color="danger" variant="link">
            Delete
          </Button>
        </Popconfirm>,
      ]}
      hoverable
    >
      <div>
        <Text type="secondary" style={{ display: "block" }}>
          {student.email}
        </Text>
        <Text type="secondary" style={{ display: "block" }}>
          {student.phone}
        </Text>
      </div>
    </Card>
  );
}

export default StudentCard;
