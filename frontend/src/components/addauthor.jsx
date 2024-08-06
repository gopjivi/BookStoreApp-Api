import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import { useState } from "react";
import Offcanvas from "react-bootstrap/Offcanvas";
import { createNewData } from "../services/service";
import { checkAuthorName } from "../services/authorservice";
import CustomAlert from "./customalert";

export default function NewAuthor({ show, handleClose, errors, setErrors }) {
  const [author, setAuthor] = useState({
    name: "",
    displayName: "",
    biography: "",
  });
  const [showAlert, setShowAlert] = useState(false);
  const authorsApiUrl = "https://localhost:7088/api/Authors";

  async function validateAuthor() {
    const errors = {};
    console.log("hi");
    if (!author.name) {
      errors.name = "Name is Required";
    }
    if (!author.displayName) {
      errors.displayName = "Display Name is Required";
    } else {
      const response = await checkAuthorName(author.displayName.trim());
      console.log(response);
      console.log("authorname available", response);
      if (response) {
        errors.displayName = "Author Display Name already exists";
      }
    }

    return errors;
  }

  function handleCloseAlert() {
    setShowAlert(false);
  }

  async function handleSubmit(e) {
    e.preventDefault();
    const validationErrors = await validateAuthor();
    console.log(validationErrors);
    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors);
    } else {
      try {
        setErrors({});
        const newAuthor = await createNewData(authorsApiUrl, author);
        setShowAlert(true);
        setAuthor({});
        handleClose();
      } catch (error) {
        console.error("Failed to create author:", error);
      }
    }
  }
  return (
    <>
      <Offcanvas
        show={show}
        onHide={handleClose}
        placement="end"
        backdrop="static"
      >
        <Offcanvas.Header closeButton className="divheader">
          <Offcanvas.Title>Create New Author</Offcanvas.Title>
        </Offcanvas.Header>
        <Offcanvas.Body>
          <Form className="formpadding ms-2">
            <Form.Group className="mb-3" controlId="formBasicName">
              <Form.Label>
                <span className="required">*</span>Author Name:
              </Form.Label>
              <Form.Control
                type="text"
                onChange={(e) => setAuthor({ ...author, name: e.target.value })}
                isInvalid={!!errors.name}
              />
              <Form.Control.Feedback type="invalid">
                {errors.name}
              </Form.Control.Feedback>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formBasicDisplayName">
              <Form.Label>
                <span className="required">*</span>Display Name:
              </Form.Label>
              <Form.Control
                type="text"
                onChange={(e) =>
                  setAuthor({ ...author, displayName: e.target.value })
                }
                isInvalid={!!errors.displayName}
              />
              <Form.Control.Feedback type="invalid">
                {errors.displayName}
              </Form.Control.Feedback>
            </Form.Group>

            <Form.Group className="mb-3" controlId="formBasicBiography">
              <Form.Label>Biography:</Form.Label>
              <Form.Control
                as="textarea"
                rows={3}
                onChange={(e) =>
                  setAuthor({ ...author, biography: e.target.value })
                }
              />
            </Form.Group>

            <Button
              variant="custom-orange btnorange"
              type="submit"
              onClick={handleSubmit}
            >
              Submit
            </Button>
          </Form>
        </Offcanvas.Body>
      </Offcanvas>
      <CustomAlert
        showAlert={showAlert}
        handleCloseAlert={handleCloseAlert}
        name={"Author"}
        action={"Created"}
      />
    </>
  );
}
