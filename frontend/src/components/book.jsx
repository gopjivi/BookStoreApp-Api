import AddBook from "./addbook";
import Button from "react-bootstrap/Button";
import { Table, Pagination, Form } from "react-bootstrap";
import { Card, Row, Col, Container } from "react-bootstrap";
import Spinner from "react-bootstrap/Spinner";
import { useState, useEffect, useReducer } from "react";
import { useFetch } from "../services/useFetch";
import { getDataById } from "../services/service";
import { getAllData } from "../services/service";
import EditBook from "./editbook";
import ViewBook from "./viewbook";
import DeleteBook from "./deletebook";

const initialState = {
  showAddBook: false,
  showEditBook: false,
  showDeleteBook: false,
  errors: {},
};
function reducer(state, action) {
  switch (action.type) {
    case "SHOW_ADD_BOOK":
      return { ...state, showAddBook: true, errors: {} };
    case "HIDE_ADD_BOOK":
      return { ...state, showAddBook: false, errors: {} };
    case "SHOW_VIEW_BOOK":
      return { ...state, showViewBook: true, errors: {} };
    case "HIDE_VIEW_BOOK":
      return { ...state, showViewBook: false, errors: {} };
    case "SHOW_EDIT_BOOK":
      return { ...state, showEditBook: true, errors: {} };
    case "HIDE_EDIT_BOOK":
      return { ...state, showEditBook: false, errors: {} };
    case "SHOW_DELETE_BOOK":
      return { ...state, showDeleteBook: true, errors: {} };
    case "HIDE_DELETE_BOOK":
      return { ...state, showDeleteBook: false, errors: {} };
    case "SET_ERRORS":
      return { ...state, errors: action.payload };
    default:
      return state;
  }
}

export default function Book() {
  const [books, setBooks] = useState([]);
  const [booksCopy, setBooksCopy] = useState([]);
  const [book, setBook] = useState([]);
  const [state, dispatch] = useReducer(reducer, initialState);
  const { showAddBook, showViewBook, showEditBook, showDeleteBook, errors } =
    state;

  const [currentPage, setCurrentPage] = useState(1);
  const [loading, setLoading] = useState(true); // Loading state
  const [searchByTitle, setSearchByTitle] = useState("");

  const [authors, setAuthors] = useState({});
  const [genres, setGenres] = useState({});
  const [languages, setLanguages] = useState({});

  const [languageValue, setLanguageValue] = useState("");
  const [authorValue, setAuthorValue] = useState("");
  const [genreValue, setGenreValue] = useState("");
  const [isOfferAvailable, setIsOfferAvailable] = useState(false);
  const [sortByPrice, setSortByPrice] = useState("ASC");
  const [sortByQuantity, setSortByQuantity] = useState("ASC");

  const booksApiUrl = "https://localhost:7088/api/Books";
  const [booksData] = useFetch(booksApiUrl);

  const authorsApiUrl = "https://localhost:7088/api/Authors";
  const genresApiUrl = "https://localhost:7088/api/Genres";
  const languageApiUrl = "https://localhost:7088/api/Languages";

  const [authorsData] = useFetch(authorsApiUrl);
  const [genresData] = useFetch(genresApiUrl);
  const [languageData] = useFetch(languageApiUrl);

  useEffect(() => {
    if (booksData) {
      setBooks(booksData);
      setLoading(false);
      setBooksCopy(booksData);
      console.log("books",booksData);
    }
  }, [booksData]);

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
  });

  const itemsPerPage = 5;
  const totalItems = books.length;
  const totalPages = Math.ceil(totalItems / itemsPerPage);
  const startIndex = (currentPage - 1) * itemsPerPage;
  const endIndex = Math.min(startIndex + itemsPerPage, totalItems);
  const paginatedData = books.slice(startIndex, endIndex);

  function handleShowAddBook() {
    dispatch({ type: "SHOW_ADD_BOOK" });
  }
  function handleCloseAddBook() {
    dispatch({ type: "HIDE_ADD_BOOK" });
    refetchBooks();
  }

  function handlePageChange(pageNumber) {
    setCurrentPage(pageNumber);
  }

  function refetchBooks() {
    getAllData(booksApiUrl)
      .then((data) => {
        console.log("updated data", data);
        setBooks(data);
        setBooksCopy(data);
        setLoading(false); // Set loading to false once data is fetched
      })
      .catch((error) => console.error("Error fetching books:", error));
  }

  function handelShowEditBook(id) {
    dispatch({ type: "SHOW_EDIT_BOOK" });
    getDataById(booksApiUrl, id)
      .then((data) => {
        setBook(data);
      })
      .catch((error) => console.error("Error fetching book:", error));
  }

  function handleCloseEditBook() {
    dispatch({ type: "HIDE_EDIT_BOOK" });
    refetchBooks();
    //FilterBooks(languageValue, genreValue, authorValue);
  }

  function handelShowViewBook(id) {
    dispatch({ type: "SHOW_VIEW_BOOK" });
    getDataById(booksApiUrl, id)
      .then((data) => {
        setBook(data);
      })
      .catch((error) => console.error("Error fetching book:", error));
  }

  function handleCloseViewBook() {
    dispatch({ type: "HIDE_VIEW_BOOK" });
    refetchBooks();
    //FilterBooks(languageValue, genreValue, authorValue);
  }
  function handelShowDeleteBook(id) {
    dispatch({ type: "SHOW_DELETE_BOOK" });
    getDataById(booksApiUrl, id)
      .then((data) => {
        setBook(data);
      })
      .catch((error) => console.error("Error fetching book:", error));
  }

  function handleCloseDeleteBook() {
    dispatch({ type: "HIDE_DELETE_BOOK" });
    refetchBooks();
  }

  function handleSearchByTitle(e) {
    setSearchByTitle(e.target.value);
    let [filteredBooks] = [books];
    let input = e.target.value;
    console.log(filteredBooks);
    if (input.length > 3) {
      filteredBooks = filteredBooks.filter((book) =>
        book.title.toLowerCase().includes(input.toLowerCase())
      );
      setBooks(filteredBooks);
    } else {
      setBooks(booksCopy);
    }
  }
  function handleLanguageChange(e) {
    const selectedValue = e.target.value;
    console.log("language", selectedValue);
    setLanguageValue(e.target.value);

    FilterBooks(selectedValue, genreValue, authorValue);
  }
  function handleGenreChange(e) {
    const selectedValue = e.target.value;
    console.log("genre", selectedValue);
    setGenreValue(e.target.value);

    FilterBooks(languageValue, selectedValue, authorValue);
  }
  function handleAuthorChange(e) {
    const selectedValue = e.target.value;
    console.log("Author", selectedValue);
    setAuthorValue(e.target.value);

    FilterBooks(languageValue, genreValue, selectedValue);
  }

  function FilterBooks(languageValue, genreValue, authorValue) {
    if (languageValue === "" && genreValue === "" && authorValue === "") {
      console.log("books", books);

      setBooks(booksCopy);
      return;
    }

    let filteredBooks = [...books]; // Assuming books is your original array

    console.log("before filtering", filteredBooks);
    console.log("languageValue", languageValue);
    console.log("genreValue", genreValue);
    console.log("authorValue", authorValue);

    // Apply language filter if value is provided
    if (languageValue !== "") {
      filteredBooks = booksCopy.filter(
        (book) => book.languageID === parseInt(languageValue, 10)
      );
      console.log("filteredbooks by language", filteredBooks);
    }

    // Apply genre filter if value is provided
    if (genreValue !== "") {
      filteredBooks = filteredBooks.filter(
        (book) => book.genreID === parseInt(genreValue, 10)
      );
      console.log("filteredbooks by genre", filteredBooks);
    }

    // Apply author filter if value is provided
    if (authorValue !== "") {
      filteredBooks = filteredBooks.filter(
        (book) => book.authorID === parseInt(authorValue, 10)
      );
      console.log("filtered books by author", filteredBooks);
    }

    setBooks(filteredBooks);
    setCurrentPage(1); // Reset to first page after filtering
  }

  function handelClearAll() {
    setLanguageValue("");
    setGenreValue("");
    setAuthorValue("");
    setBooks(booksCopy);
    setIsOfferAvailable(false);
  }
  function sortByPriceASC() {
    const sorted = [...books].sort((a, b) => a.price - b.price);
    setBooks(sorted);
  }
  function sortByPriceDESC() {
    const sorted = [...books].sort((a, b) => b.price - a.price);
    setBooks(sorted);
  }

  function sortByQuantityASC() {
    const sorted = [...books].sort(
      (a, b) => a.stockQuantity - b.stockQuantity
    );
    setBooks(sorted);
  }
  function sortByQuantityDESC() {
    const sorted = [...books].sort(
      (a, b) => b.stockQuantity - a.stockQuantity
    );
    setBooks(sorted);
  }
  function handleSortByPrice() {
    if (sortByPrice == "ASC") {
      sortByPriceASC();
      setSortByPrice("DESC");
    } else {
      sortByPriceDESC();
      setSortByPrice("ASC");
    }
  }
  // function handleSortByQuantity() {
  //   if (sortByQuantity == "ASC") {
  //     sortByQuantityASC();
  //     setSortByQuantity("DESC");
  //   } else {
  //     sortByQuantityDESC();
  //     setSortByQuantity("ASC");
  //   }
  // }

  function handleFilterOffer(e) {
    const isChecked = e.target.checked;
    console.log("Switch is", isChecked ? "on" : "off");
    setIsOfferAvailable(isChecked);

    if (isChecked) {
      let filteredBooks = booksCopy.filter(
        (book) => book.isOfferAvailable === true
      );
      setBooks(filteredBooks);
      console.log("filtered books by author", filteredBooks);
    } else {
      setBooks(booksCopy);
    }
    setLanguageValue("");
    setGenreValue("");
    setAuthorValue("");
    setCurrentPage(1);
  }


  return (
    <div>
      <div className="container mt-5">
        <div className="row">
          <div
            className="col-lg-2 col-md-3 col-sm-4 col-12"
            style={{ backgroundColor: "#f8f9fa" }}
          >
            <div className="row mb-2">
              <div className="col-6">
                <h5>Filter By:</h5>
              </div>
              <div className="col-6 text-end">
                <button
                  type="button"
                  className="custom-orange btnorange"
                  onClick={handelClearAll}
                >
                  Clear All
                </button>
              </div>
            </div>
            <div className="row mb-2">
              <div className="col">
                <h6>Language</h6>
              </div>
            </div>
            <div className="row mb-2">
              <div className="col">
                <Form.Select
                  onChange={(e) => handleLanguageChange(e)}
                  value={languageValue}
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
              </div>
            </div>
            <div className="row mb-2">
              <div className="col">
                <h6>Genre</h6>
              </div>
            </div>
            <div className="row mb-2">
              <div className="col">
                <Form.Select
                  onChange={(e) => handleGenreChange(e)}
                  value={genreValue}
                >
                  <option value="">Choose...</option>
                  {genres.length > 0 &&
                    genres.map((genre) => (
                      <option key={genre.genreID} value={genre.genreID}>
                        {genre.genreName}
                      </option>
                    ))}
                </Form.Select>
              </div>
            </div>
            <div className="row mb-2">
              <div className="col">
                <h6>Authors</h6>
              </div>
            </div>
            <div className="row mb-2">
              <div className="col" style={{ marginBottom: "20px" }}>
                <Form.Select
                  onChange={(e) => handleAuthorChange(e)}
                  value={authorValue}
                >
                  <option value="">Choose...</option>
                  {authors.length > 0 &&
                    authors.map((author) => (
                      <option key={author.authorID} value={author.authorID}>
                        {author.displayName}
                      </option>
                    ))}
                </Form.Select>
              </div>
              <div className="horizontal-line"></div>
            </div>
            <div className="row mb-2">
              <div className="col">
                <div className="d-flex justify-content-between align-items-center">
                  <label
                    className="form-check-label"
                    htmlFor="flexSwitchCheckDefault"
                  >
                    Offers
                  </label>
                  <div className="form-check form-switch">
                    <input
                      className="form-check-input"
                      type="checkbox"
                      role="switch"
                      id="flexSwitchCheckDefault"
                      checked={isOfferAvailable}
                      onChange={(e) => handleFilterOffer(e)}
                    />
                  </div>
                </div>
              </div>
              <div className="horizontal-line"></div>
            </div>
            <div className="row mb-2">
              <div className="col-6">
                <h5>Sort By:</h5>
              </div>
            </div>
            <div className="row mb-2">
              <div className="col-6">
                <button onClick={handleSortByPrice}>Price</button>
              </div>
            </div>
            {/* <div className="row mb-2">
              <div className="col-6" onClick={handleSortByPrice}>
                <button onClick={handleSortByQuantity}>
                  Available Quantity
                </button>
              </div>
            </div> */}
          </div>
          <div className="col-lg-10 col-md-9 col-sm-8 col-12">
            <div className="row">
              <div className="col-md-6 col-sm-12">
                <div className="input-group">
                  <span className="input-group-text" id="basic-addon1">
                    <i className="bi bi-search"></i>
                  </span>
                  <input
                    type="text"
                    className="form-control"
                    placeholder="Search by Book Title"
                    value={searchByTitle}
                    onChange={(e) => handleSearchByTitle(e)}
                  />
                </div>
              </div>
              <div className="col-md-6 col-sm-12 text-end">
                <Button
                  variant="custom-orange"
                  className="btn btnorange"
                  onClick={handleShowAddBook}
                >
                  <i className="bi bi-plus-lg"></i> Add New Book
                </Button>
              </div>
            </div>
            <div className="row">
              <div className="col">
                {loading ? (
                  <div
                    className="d-flex justify-content-center align-items-center"
                    style={{ height: "50vh" }}
                  >
                    <Spinner animation="border" role="status">
                      <span className="visually-hidden">Loading...</span>
                    </Spinner>
                  </div>
                ) : (
                  <>
                    <div className="shadow card-container mt-4">
                      {paginatedData.length > 0 ? (
                        paginatedData.map((book, index) => (
                          <Container
                            className={`my-4 horizontal-card-container${
                              index % 2 === 0 ? "card-even" : "card-odd"
                            }`}
                            key={book.bookID}
                          >
                            <Card
                              className={`horizontal-card ${
                                index % 2 === 0 ? "card-even" : "card-odd"
                              }`}
                            >
                              <Row className="g-0">
                                <Col xs={12} md={2} className="image-container">
                                <Card.Img variant="top" src={book.bookImageURL} className="card-image" alt="Book Image" />
                               
                                </Col>
                                <Col xs={12} md={10}>
                                  <Card.Header
                                    className="card-header"
                                    style={{ backgroundColor: "#daa556" }}
                                  >
                                    <div className="d-flex justify-content-between bold-title">
                                      <span>{book.title}</span>
                                      <span>₹ {book.price}</span>
                                    </div>
                                  </Card.Header>
                                  <Card.Body>
                                    <Card.Text>
                                      <div className="mb-2">
                                        Author:{" "}
                                        {book.author
                                          ? book.author.displayName
                                          : "Unknown"}
                                      </div>
                                      <div className="mb-2">
                                        Available Quantity:{" "}
                                        {book.stockQuantity}
                                      </div>
                                      <div className="mb-2">
                                        Location: {book.storageSection} -{" "}
                                        {book.storageShelf
                                          ? book.storageShelf
                                          : "N/A"}
                                      </div>
                                    </Card.Text>{" "}
                                    {/* Example text or description */}
                                  </Card.Body>
                                  <Card.Footer
                                    className={`d-flex justify-content-end ${
                                      index % 2 === 0 ? "card-even" : "card-odd"
                                    }`}
                                  >
                                    <button
                                      type="button"
                                      className="btn btnorange mx-2"
                                      onClick={() =>
                                        handelShowViewBook(book.bookID)
                                      }
                                    >
                                      <i className="bi bi-binoculars-fill"></i>
                                    </button>
                                    <button
                                      type="button"
                                      className="btn btnorange mx-2"
                                      onClick={() =>
                                        handelShowEditBook(book.bookID)
                                      }
                                    >
                                      <i className="bi bi-pen-fill"></i>
                                    </button>
                                    <button
                                      type="button"
                                      className="btn btnorange mx-2"
                                      onClick={() =>
                                        handelShowDeleteBook(book.bookID)
                                      }
                                    >
                                      <i className="bi bi-trash-fill"></i>
                                    </button>
                                  </Card.Footer>
                                </Col>
                              </Row>
                            </Card>
                          </Container>
                        ))
                      ) : (
                        <div
                          id="noresult"
                          className="text-center bg-light text-danger"
                        >
                          No Results Found !!!
                        </div>
                      )}
                    </div>

                    <div className="d-flex justify-content-between align-items-center">
                      <span>
                        Showing {startIndex + 1} to {endIndex} of {totalItems}{" "}
                        results
                      </span>
                      <Pagination responsive className="custom-pagination">
                        <Pagination.First
                          onClick={() => handlePageChange(1)}
                          disabled={currentPage === 1}
                        />
                        <Pagination.Prev
                          onClick={() => handlePageChange(currentPage - 1)}
                          disabled={currentPage === 1}
                        />
                        {Array.from({ length: totalPages }, (_, idx) => (
                          <Pagination.Item
                            key={idx + 1}
                            active={idx + 1 === currentPage}
                            onClick={() => handlePageChange(idx + 1)}
                          >
                            {idx + 1}
                          </Pagination.Item>
                        ))}
                        <Pagination.Next
                          onClick={() => handlePageChange(currentPage + 1)}
                          disabled={currentPage === totalPages}
                        />
                        <Pagination.Last
                          onClick={() => handlePageChange(totalPages)}
                          disabled={currentPage === totalPages}
                        />
                      </Pagination>
                    </div>
                  </>
                )}
              </div>
            </div>
          </div>
        </div>

        <AddBook
          show={showAddBook}
          handleClose={handleCloseAddBook}
          errors={errors}
          setErrors={(errors) =>
            dispatch({ type: "SET_ERRORS", payload: errors })
          }
        />
       
      </div>

      <EditBook
        show={showEditBook}
        handleClose={handleCloseEditBook}
        errors={errors}
        book={book}
        setBook={setBook}
        setErrors={(errors) =>
          dispatch({ type: "SET_ERRORS", payload: errors })
        }
      />
       <ViewBook
        show={showViewBook}
        handleClose={handleCloseViewBook}
        book={book}
        setBook={setBook}
      />
        <DeleteBook
        show={showDeleteBook}
        handleClose={handleCloseDeleteBook}
        book={book}
      />
    
    </div>
  );
}
