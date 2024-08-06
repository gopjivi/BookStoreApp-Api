import { Button } from "react-bootstrap";
import Modal from "react-bootstrap/Modal";
import { deleteData } from "../services/service";
import CustomAlert from "./customalert";
import FailedCustomAlert from "./failedcustomalert";
import { useState } from "react";
import { checkAuthorExistsInBook } from "../services/authorservice";


export default function DeleteAuthor({ show, handleClose, author }) {
  const [showAlert, setShowAlert] = useState(false);
  const [showFailedAlert, setShowFailedAlert] = useState(false);
  const authorsApiUrl = "https://localhost:7088/api/Authors";

  function handleCloseAlert() {
    setShowAlert(false);
  }
  function handleCloseFailedAlert() {
    setShowFailedAlert(false);
  }

  async function deleteAuthor(id) {
    if (!author || !id) {
      console.error("Author ID is not defined");
      return;
    }

    try {
      const response = await checkAuthorExistsInBook(author.authorID);
      if(!response)
      {
      const responsestatus = await deleteData(authorsApiUrl, id);
      // Assuming deleteAuthor returns the fetch response
      console.log("response", responsestatus);
      if (responsestatus !== 204) {
        throw new Error("Network response was not ok");
      } else {
        console.log("Author deleted successfully:", id);
        setShowAlert(true);
        handleClose();
      }
    }

    else{
      setShowFailedAlert(true);
      handleClose();
    }
    } catch (error) {
      console.error("Failed to delete author:", error);
    }
  }
  return (
    <>
      <Modal
        show={show}
        onHide={handleClose}
        backdrop="static"
        keyboard={false}
        animation={true}
      >
        <Modal.Header className="modal-header divheader" closeButton>
          <Modal.Title>Delete Author</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className="container-fluid">
            <div className="row" style={{ marginBottom: 15 }}>
              <p>
                Are you sure you want to delete this{" "}
                <b className="highlight">{author.name} </b> author?<br></br>
               
              </p>
            </div>
          </div>
        </Modal.Body>
        <Modal.Footer>
          <Button
            type="button"
            variant="custom-orange"
            className="btn btnorange"
            data-bs-dismiss="modal"
            onClick={() => deleteAuthor(author.authorID)}
          >
            Yes
          </Button>
          <Button
            type="button"
            variant="custom-orange"
            className="btn btnorange"
            data-bs-dismiss="modal"
            onClick={() => handleClose()}
          >
            No
          </Button>
        </Modal.Footer>
      </Modal>
      <CustomAlert
        showAlert={showAlert}
        handleCloseAlert={handleCloseAlert}
        name={"Author"}
        action={"Deleted"}
      />
       <FailedCustomAlert
        showAlert={showFailedAlert}
        handleCloseAlert={handleCloseFailedAlert}
        name={"Can not delete Author"}
       
      />
    </>
  );
}
