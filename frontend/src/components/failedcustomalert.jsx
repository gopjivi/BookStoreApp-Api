import Modal from "react-bootstrap/Modal";

export default function FailedCustomAlert({
  showAlert,
  handleCloseAlert,
  name,
  
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
        <Modal.Header className="modal-header custom-alert-failed" closeButton>
          <Modal.Title>
            {" "}
            <i className="bi bi-x-lg"></i>  Message
          </Modal.Title>
        </Modal.Header>
        <Modal.Body className="custom-alert-failed">
          <div className="container-fluid">
            <div className="row" style={{ marginBottom: 15 }}>
              <p>
                {name} 
              </p>
            </div>
          </div>
        </Modal.Body>
      </Modal>
    </>
  );
}
