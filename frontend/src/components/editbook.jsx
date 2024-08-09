import Col from "react-bootstrap/Col";
import { Form, InputGroup, Button } from "react-bootstrap";
import Row from "react-bootstrap/Row";
import Offcanvas from "react-bootstrap/Offcanvas";
import { useFetch } from "../services/useFetch";
import { useState, useEffect } from "react";
import { updateData } from "../services/service";
import CustomAlert from "./customalert";

export default function EditBook({
  show,
  handleClose,
  book,
  setBook,
  errors,
  setErrors,
}) {
  //console.log("book loading",book);
  const [showAlert, setShowAlert] = useState(false);
  const [offerAvailable, setOfferAvailable] = useState(false);
  const [authors, setAuthors] = useState({});
  const [genres, setGenres] = useState({});
  const [languages, setLanguages] = useState({});
  const [maxDate, setMaxDate] = useState("");
  const [selectedLanguage, setSelectedLanguage] = useState("");
  const [selectedGenre, setSelectedGenre] = useState("");
  const [closeModalAfterAlert, setCloseModalAfterAlert] = useState(false);

  const [file, setFile] = useState(null);
  const [preview, setPreview] = useState(null);

  const booksApiUrl = "https://localhost:7088/api/Books";
  const authorsApiUrl = "https://localhost:7088/api/Authors";
  const genresApiUrl = "https://localhost:7088/api/Genres";
  const languageApiUrl = "https://localhost:7088/api/Languages";

  const editions = [
    "First Edition",
    "Second Edition",
    "Third Edition",
    "Revised Edition",
    "Special Edition",
  ];

  const [authorsData] = useFetch(authorsApiUrl);
  const [genresData] = useFetch(genresApiUrl);
  const [languageData] = useFetch(languageApiUrl);

  useEffect(() => {
    if (authorsData) {
      setAuthors(authorsData);
    }
    if (genresData) {
      setGenres(genresData);
    }
    if (languageData) {
      setLanguages(languageData);
    }

    // Set max date
    const today = new Date();
    const yyyy = today.getFullYear();
    const mm = String(today.getMonth() + 1).padStart(2, "0"); // Months are zero-based
    const dd = String(today.getDate()).padStart(2, "0");

    const formattedDate = `${yyyy}-${mm}-${dd}`;
    setMaxDate(formattedDate);

   
  }, [authorsData, genresData, languageData]);

  // useEffect(() => {
  //   if (languages.length > 0 && book.languageID) {
  //     const selectedLanguage = languages.find(
  //       (language) => language.languageID === parseInt(book.languageID)
  //     );
  //     const selectedLanguageName = selectedLanguage?.languageName || "";
  //     setSelectedLanguage(selectedLanguageName);
  //   }
  // }, [languages, book.languageID]);
 
  useEffect(() => {
    if(book)
    {
    setPreview(book.bookImageURL);
    setOfferAvailable(book.isOfferAvailable);
    }
   
  }, [book]);

  const handleRadioChange = (value) => {
    setOfferAvailable(value);
  };

  async function validateBook() {
    const errors = {};

    if (!book.title) {
      errors.title = "Book Title is Required";
    }
    if (!book.price) {
      errors.price = "Book Price is Required";
    }
    if (!book.authorID) {
      errors.authorID = "Author is Required";
    }
    if (!book.languageID) {
      errors.languageID = "Language is Required";
    }
    if (!book.genreID) {
      errors.genreID = "Genre is Required";
    }
    if (!book.publicationDate) {
      errors.publicationDate = "Publication date is Required";
    }
    if (!book.stockQuantity) {
      errors.stockQuantity = "Stock quantity is Required";
    }
    if (!book.storageSection) {
      errors.storageSection = "Stock Section is Required";
    }

    return errors;
  }

  function handleCloseAlert() {
    setShowAlert(false);
    if (closeModalAfterAlert) {
      handleClose();
      setCloseModalAfterAlert(false); // Reset for the next use
    }
  }

  function handleLanguageChange(event) {
    const selectedLanguageId = event.target.value;
    setBook({ ...book, languageID: selectedLanguageId });

    const selectedLanguage = languages.find(
      (language) => language.languageID === parseInt(selectedLanguageId)
    );
    const selectedLanguageName = selectedLanguage?.languageName || "";

    setSelectedLanguage(selectedLanguageName);
    updateStorageSection(selectedLanguageName, selectedGenre);
  }

  function handleGenreChange(event) {
    const selectedGenreId = event.target.value;
    const selectedGenre = genres.find(
      (genre) => genre.genreID === parseInt(selectedGenreId)
    );
    const selectedGenreName = selectedGenre?.genreName || "";
    setBook({ ...book, genreID: selectedGenreId });
    setSelectedGenre(selectedGenreName);
    updateStorageSection(selectedLanguage, selectedGenreName);
  }

  function updateStorageSection(languageName, genreName) {
    if (languageName || genreName) {
      const newStorageSection = `${languageName} - ${genreName}`;
      setBook((prevBook) => ({
        ...prevBook,
        storageSection: newStorageSection,
      }));
    }
  }

  async function handleSubmit(e) {
    e.preventDefault();

    // Validate the book data
    const validationErrors = await validateBook();
    book.isOfferAvailable = offerAvailable;

    // If there are validation errors, set the errors and return early
    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors);
      return;
    }

    // Reset any previous errors
    setErrors({});

    // Prepare the form data for file upload
    const formData = new FormData();
    formData.append("file", file);

    try {
      if (file) {
        const byteArray = await fileToByteArray(file);
        console.log("bytearry",byteArray);
        console.log("bytearry to array", Array.from(byteArray));
      
         // Directly modify the book object
         book.bookImage = Array.from(byteArray);
       
        console.log("book with image",book);
      }
      // Update the book information
      console.log("book",book)
      const response = await updateData(booksApiUrl, book.bookID, book);
      console.log("updated response",response);
      if(response){
      // Set success state and reset form fields
      setShowAlert(true);
      setBook({});
      setFile(null);
      setPreview(null);
      
      setCloseModalAfterAlert(true);
      
      }
    } catch (error) {
      console.error("Failed to update book:", error);
    }
  }

  const handleFileChange = (event) => {
    const file = event.target.files[0];

    // Check if a file was selected
    if (!file) {
      setFile(null);
      setPreview(null);
      return;
    }

    setFile(file);

    const reader = new FileReader();
    const url = URL.createObjectURL(file);
    reader.onloadend = () => {
      setPreview(reader.result);
    };
    reader.readAsDataURL(file);
  };

  const fileToByteArray = (file) => {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onloadend = () => {
            const arrayBuffer = reader.result;
            const byteArray = new Uint8Array(arrayBuffer);
            resolve(byteArray);
        };
        reader.onerror = reject;
        reader.readAsArrayBuffer(file);
    });
};

// Utility function to format date to yyyy-MM-dd
const formatDate = (dateString) => {
  const date = new Date(dateString);
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are 0-based
  const day = String(date.getDate()).padStart(2, '0');
  return `${year}-${month}-${day}`;
};

  return (
    <>
      <Offcanvas
        show={show}
        onHide={handleClose}
        placement="end"
        backdrop="static"
        className="custom-offcanvas"
      >
        <Offcanvas.Header closeButton className="divheader">
          <Offcanvas.Title>Edit Book Details</Offcanvas.Title>
        </Offcanvas.Header>
        <Offcanvas.Body>
          <Form>
            <Row className="mb-4">
              <Form.Group as={Col} controlId="formGridTitle">
                <Form.Label>
                  <span className="required">*</span> Book Title:
                </Form.Label>
                <Form.Control
                  type="text"
                  onChange={(e) => setBook({ ...book, title: e.target.value })}
                  isInvalid={!!errors.title}
                  value={book.title}
                />
                <Form.Control.Feedback type="invalid">
                  {errors.title}
                </Form.Control.Feedback>
              </Form.Group>

              <Form.Group as={Col} controlId="formGridPrice">
                <Form.Label>
                  <span className="required">*</span> Price:
                </Form.Label>
                <InputGroup>
                  <InputGroup.Text>₹</InputGroup.Text>
                  <Form.Control
                    type="number"
                    onChange={(e) =>
                      setBook({ ...book, price: e.target.value })
                    }
                    value={book.price}
                    isInvalid={!!errors.price}
                  />
                  <Form.Control.Feedback type="invalid">
                    {errors.price}
                  </Form.Control.Feedback>
                </InputGroup>
              </Form.Group>
            </Row>
            <Row className="mb-4">
              <Form.Group as={Col} controlId="formGridLanguage">
                <Form.Label>
                  <span className="required">*</span> Language:
                </Form.Label>

                <Form.Select
                  // onChange={(e) =>
                  //   setBook({ ...book, language_id: e.target.value })
                  // }
                  onChange={handleLanguageChange}
                  isInvalid={!!errors.languageID}
                  value={book.languageID}
                >
                  <option value="">Choose...</option>
                  {languages.length > 0 &&
                    languages.map((language) => (
                      <option
                        key={language.languageID}
                        value={language.languageID}
                      >
                        {language.languageName}
                      </option>
                    ))}
                </Form.Select>
                <Form.Control.Feedback type="invalid">
                  {errors.languageID}
                </Form.Control.Feedback>
              </Form.Group>
              <Form.Group as={Col} controlId="formGridGenre">
                <Form.Label>
                  <span className="required">*</span> Genre:
                </Form.Label>
                <Form.Select
                  onChange={handleGenreChange}
                  isInvalid={!!errors.genreID}
                  value={book.genreID}
                >
                  <option value="">Choose...</option>
                  {genres.length > 0 &&
                    genres.map((genre) => (
                      <option key={genre.genreID} value={genre.genreID}>
                        {genre.genreName}
                      </option>
                    ))}
                </Form.Select>
                <Form.Control.Feedback type="invalid">
                  {errors.genreID}
                </Form.Control.Feedback>
              </Form.Group>
            </Row>
            <Row className="mb-4">
              <Form.Group as={Col} controlId="formGridAuthor">
                <Form.Label>
                  <span className="required">*</span> Author:
                </Form.Label>
                <Form.Select
                  onChange={(e) =>
                    setBook({ ...book, authorID: e.target.value })
                  }
                  value={book.authorID}
                  isInvalid={!!errors.authorID}
                >
                  <option value="">Choose...</option>
                  {authors.length > 0 &&
                    authors.map((author) => (
                      <option key={author.authorID} value={author.authorID}>
                        {author.displayName}
                      </option>
                    ))}
                </Form.Select>
                <Form.Control.Feedback type="invalid">
                  {errors.authorID}
                </Form.Control.Feedback>
              </Form.Group>
              <Form.Group as={Col} controlId="formGridPublishDate">
                <Form.Label>
                  <span className="required">*</span> Publication Date:
                </Form.Label>
                <Form.Control
                  type="date"
                  max={maxDate}
                  onChange={(e) =>
                    setBook({ ...book, publicationDate: e.target.value })
                  }
                  value={formatDate(book.publicationDate)}
                  isInvalid={!!errors.publicationDate}
                />
                <Form.Control.Feedback type="invalid">
                  {errors.publicationDate}
                </Form.Control.Feedback>
              </Form.Group>
            </Row>
            <Row className="mb-3">
              <Form.Group as={Col} controlId="formGridEdition">
                <Form.Label>Edition:</Form.Label>

                <Form.Select
                  onChange={(e) =>
                    setBook({ ...book, edition: e.target.value })
                  }
                  value={book.edition}
                >
                  <option>Choose...</option>
                  {editions.map((edition, index) => (
                    <option key={index} eventKey={edition}>
                      {edition}
                    </option>
                  ))}
                </Form.Select>
              </Form.Group>

              <Form.Group as={Col} controlId="formGridStockQuantity">
                <Form.Label>
                  <span className="required">*</span>Stock Quantity:
                </Form.Label>
                <Form.Control
                  type="number"
                  onChange={(e) =>
                    setBook({ ...book, stockQuantity: e.target.value })
                  }
                  value={book.stockQuantity}
                  isInvalid={!!errors.stockQuantity}
                />
                <Form.Control.Feedback type="invalid">
                  {errors.stockQuantity}
                </Form.Control.Feedback>
              </Form.Group>
            </Row>
            <Row className="mb-4">
              <Form.Group as={Col} controlId="formGridSection">
                <Form.Label>
                  <span className="required">*</span> Storage Section:
                </Form.Label>
                <Form.Control
                  type="text"
                  value={book.storageSection}
                  onChange={(e) =>
                    setBook({ ...book, storageSection: e.target.value })
                  }
                  isInvalid={!!errors.storageSection}
                />
                <Form.Control.Feedback type="invalid">
                  {errors.storageSection}
                </Form.Control.Feedback>
              </Form.Group>

              <Form.Group as={Col} controlId="formGridShelf">
                <Form.Label>Storage Shelf:</Form.Label>
                <Form.Control
                  type="text"
                  onChange={(e) =>
                    setBook({ ...book, storageShelf: e.target.value })
                  }
                  value={book.storageShelf}
                  isInvalid={!!errors.storageShelf}
                />
                <Form.Control.Feedback type="invalid">
                  {errors.storageShelf}
                </Form.Control.Feedback>
              </Form.Group>
            </Row>
            <Row className="mb-4" rowSpan="2">
              <Form.Group as={Col}>
                <Form.Label style={{ marginRight: "5px" }}>
                  <span className="required">*</span>Offer Available:
                </Form.Label>
                <input
                  type="radio"
                  id="option1"
                  value="true"
                  checked={offerAvailable === true}
                  onChange={() => handleRadioChange(true)}
                />
                <label htmlFor="option1" style={{ marginRight: "5px" }}>
                  Yes
                </label>
                {"    "}
                <input
                  type="radio"
                  id="option2"
                  value="false"
                  checked={offerAvailable === false}
                  onChange={() => handleRadioChange(false)}
                />
                <label htmlFor="option2">No</label>
              </Form.Group>
              <Form.Group as={Col} rowSpan="2"></Form.Group>
            </Row>
            <Row className="mb-3">
              <Form.Group as={Col} controlId="formFile">
                <Form.Label>Update Book Cover:</Form.Label>
                <Form.Control type="file" onChange={handleFileChange} />
              </Form.Group>
              <Form.Group as={Col} controlId="formFileimage">
                {preview && (
                  <img src={preview} alt="Preview" className="img-thumbnail" />
                )}
              </Form.Group>
            </Row>
            <Button
              variant="custom-orange btnorange"
              type="submit"
              onClick={handleSubmit}
              style={{ marginRight: "10px" }}
            >
              Update
            </Button>
            <Button
              variant="custom-orange btnorange"
              type="button"
              onClick={handleClose}
            >
              Cancel
            </Button>
          </Form>
        </Offcanvas.Body>
      </Offcanvas>
       <CustomAlert
        showAlert={showAlert}
        handleCloseAlert={handleCloseAlert}
        name={"Book"}
        action={"Updated"}
      />
    </>
  );
}
