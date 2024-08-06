import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import { useState } from "react";
import Offcanvas from "react-bootstrap/Offcanvas";
import { createNewData } from "../services/service";
import { checkGenreName } from "../services/genreservice";
import CustomAlert from "./customalert";

export default function NewGenre({ show, handleClose, errors, setErrors }) {
  const [genre, setGenre] = useState({
    genreName: "",
  });
  const [showAlert, setShowAlert] = useState(false);
  const genresApiUrl = "https://localhost:7088/api/Genres";

  async function validateGenre() {
    const errors = {};

    if (!genre.genreName) {
      errors.genreName = " Genre Name is Required";
    } else {
      const response = await checkGenreName(genre.genreName.trim());
      console.log("response",response)
      
      if (response) {
        errors.genreName = "Genre  Name already exists";
      }
    }

    return errors;
  }

  function handleCloseAlert() {
    setShowAlert(false);
  }

  async function handleSubmit(e) {
    e.preventDefault();
    const validationErrors = await validateGenre();
    console.log(validationErrors);
    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors);
    } else {
      try {
        setErrors({});
        const newGenre = await createNewData(genresApiUrl, genre);
       console.log("added result",newGenre);
        setShowAlert(true);
        setGenre({});
        handleClose();
      } catch (error) {
        console.error("Failed to create genre:", error);
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
          <Offcanvas.Title>Create New Genre</Offcanvas.Title>
        </Offcanvas.Header>
        <Offcanvas.Body>
          <Form className="formpadding ms-2">
            <Form.Group className="mb-3" controlId="formBasicName">
              <Form.Label>
                <span className="required">*</span>Genre Name:
              </Form.Label>
              <Form.Control
                type="text"
                onChange={(e) =>
                  setGenre({ ...genre, genreName: e.target.value })
                }
                isInvalid={!!errors.genreName}
              />
              <Form.Control.Feedback type="invalid">
                {errors.genreName}
              </Form.Control.Feedback>
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
        name={"Genre"}
        action={"Created"}
      />
    </>
  );
}
