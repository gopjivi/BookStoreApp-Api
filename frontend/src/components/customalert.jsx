import Modal from "react-bootstrap/Modal";

export default function CustomAlert({
  showAlert,
  handleCloseAlert,
  name,
  action,
}) {
  return (
    <>
      <Modal
        show={showAlert}
        onHide={handleCloseAlert}
        backdrop="static"
        keyboard={false}
        animation={true}
      >
        <Modal.Header className="modal-header custom-alert" closeButton>
          <Modal.Title>
            {" "}
            <i className="bi bi-check-lg"></i> Success Message
          </Modal.Title>
        </Modal.Header>
        <Modal.Body className="custom-alert">
          <div className="container-fluid">
            <div className="row" style={{ marginBottom: 15 }}>
              <p>
                {name} {action} successfully.
              </p>
            </div>
          </div>
        </Modal.Body>
      </Modal>
    </>
  );
}
