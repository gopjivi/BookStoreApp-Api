import { Button } from "react-bootstrap";
import Modal from "react-bootstrap/Modal";
import { deleteData } from "../services/service";
import CustomAlert from "./customalert";
import { useState } from "react";

export default function DeleteBook({ show, handleClose, book }) {
  const [showDeleteAlert, setDeleteShowAlert] = useState(false);
const booksApiUrl = "https://localhost:7088/api/Books";

  function handleCloseAlert() {
    setDeleteShowAlert(false);
  }
 

  async function deleteBook(id) {
    if (!book || !id) {
      console.error("Book ID is not defined");
      return;
    }

    try {
     
      const response = await deleteData(booksApiUrl, id);

      console.log("response", response);
      if (response !== 204) {
        throw new Error("Network response was not ok");
      } else {
        setDeleteShowAlert(true);
        console.log("book deleted successfully:", id);
       
        handleClose();
      }
    } catch (error) {
      console.error("Failed to delete book:", error);
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
          <Modal.Title>Delete Book</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className="container-fluid">
            <div className="row" style={{ marginBottom: 15 }}>
              <p>
                Are you sure you want to delete this{" "}
                <b className="highlight">{book.title} </b> book?
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
            onClick={() => deleteBook(book.bookID)}
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
        showAlert={showDeleteAlert}
        handleCloseAlert={handleCloseAlert}
        name={"Book"}
        action={"Deleted"}
      />
    </>
  );
}
