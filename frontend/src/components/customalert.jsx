import Modal from "react-bootstrap/Modal";
import { Button } from "react-bootstrap";

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
            <div className="row" style={{ marginBottom: 35 }}>
              <p>
                {name} {action} successfully.
              </p>
            </div>
          </div>
        </Modal.Body>
        <Modal.Footer className="custom-alert">
      
        </Modal.Footer>
      </Modal>
    </>
  );
}
