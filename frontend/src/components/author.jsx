import NewAuthor from "./addauthor";
import Button from "react-bootstrap/Button";
import { Table, Pagination } from "react-bootstrap";
import Spinner from "react-bootstrap/Spinner";
import { useState, useEffect, useReducer } from "react";
import { useFetch } from "../services/useFetch";
import EditAuthor from "./editauthor";
import { getDataById } from "../services/service";
import { getAllData } from "../services/service";
import DeleteAuthor from "./deleteauthor";

const initialState = {
  showAddAuthor: false,
  showEditAuthor: false,
  showDeleteAuthor: false,
  errors: {},
};
function reducer(state, action) {
  switch (action.type) {
    case "SHOW_ADD_AUTHOR":
      return { ...state, showAddAuthor: true, errors: {} };
    case "HIDE_ADD_AUTHOR":
      return { ...state, showAddAuthor: false, errors: {} };
    case "SHOW_EDIT_AUTHOR":
      return { ...state, showEditAuthor: true, errors: {} };
    case "HIDE_EDIT_AUTHOR":
      return { ...state, showEditAuthor: false, errors: {} };
    case "SHOW_DELETE_AUTHOR":
      return { ...state, showDeleteAuthor: true, errors: {} };
    case "HIDE_DELETE_AUTHOR":
      return { ...state, showDeleteAuthor: false, errors: {} };
    case "SET_ERRORS":
      return { ...state, errors: action.payload };
    default:
      return state;
  }
}

export default function Author() {
  const [authors, setAuthors] = useState([]);
  const [author, setAuthor] = useState([]);
  const [state, dispatch] = useReducer(reducer, initialState);
  const { showAddAuthor, showEditAuthor, showDeleteAuthor, errors } = state;

  const [currentPage, setCurrentPage] = useState(1);
  const [loading, setLoading] = useState(true); // Loading state
  const [searchByAuthorName, setsearchByAuthorName] = useState("");
  const [authorsCopy, setAuthorsCopy] = useState([]);

  const authorsApiUrl = "https://localhost:7088/api/Authors";
  const [authorsData] = useFetch(authorsApiUrl);

  useEffect(() => {
    if (authorsData) {
      setAuthors(authorsData);
      setLoading(false);
      setAuthorsCopy(authorsData);
      console.log(authorsData);
    }
  }, [authorsData]);

  const itemsPerPage = 5;
  const totalItems = authors.length;
  const totalPages = Math.ceil(totalItems / itemsPerPage);
  const startIndex = (currentPage - 1) * itemsPerPage;
  const endIndex = Math.min(startIndex + itemsPerPage, totalItems);
  const paginatedData = authors.slice(startIndex, endIndex);

  function handleShowAddAuthor() {
    dispatch({ type: "SHOW_ADD_AUTHOR" });
  }
  function handleCloseAddAuthor() {
    dispatch({ type: "HIDE_ADD_AUTHOR" });
    refetchAuthors();
  }

  function handlePageChange(pageNumber) {
    setCurrentPage(pageNumber);
  }

  function refetchAuthors() {
    getAllData(authorsApiUrl)
      .then((data) => {
        setAuthors(data);
        setAuthorsCopy(data);
        setLoading(false); // Set loading to false once data is fetched
      })
      .catch((error) => console.error("Error fetching authors:", error));
  }

  function handelShowEditAuthor(id) {
    dispatch({ type: "SHOW_EDIT_AUTHOR" });
    getDataById(authorsApiUrl, id)
      .then((data) => {
        setAuthor(data);
      })
      .catch((error) => console.error("Error fetching author:", error));
  }

  function handleCloseEditAuthor() {
    dispatch({ type: "HIDE_EDIT_AUTHOR" });
    refetchAuthors();
  }

  function handelShowDeleteAuthor(id) {
    dispatch({ type: "SHOW_DELETE_AUTHOR" });
    getDataById(authorsApiUrl, id)
      .then((data) => {
        setAuthor(data);
      })
      .catch((error) => console.error("Error fetching author:", error));
  }

  function handleCloseDeleteAuthor() {
    dispatch({ type: "HIDE_DELETE_AUTHOR" });
    refetchAuthors();
  }
  function handleSearchByName(e) {
    setsearchByAuthorName(e.target.value);
    let [filteredAuthors] = [authors];
    let input = e.target.value;
    console.log(filteredAuthors);
    if (input.length > 3) {
      filteredAuthors = filteredAuthors.filter((author) =>
        author.name.toLowerCase().includes(input.toLowerCase())
      );
      setAuthors(filteredAuthors);
    } else {
      setAuthors(authorsCopy);
    }
  }

  return (
    <div className="container mt-5">
      <div className="row">
        <div className="col-md-1 col-sm-0"></div>
        <div className="col-md-6 col-sm-12 mb-3">
          <div className="input-group">
            <span className="input-group-text" id="basic-addon1">
              <i className="bi bi-search"></i>
            </span>
            <input
              type="text"
              className="form-control"
              placeholder="Search by Author Name"
              value={searchByAuthorName}
              onChange={(e) => handleSearchByName(e)}
            />
          </div>
        </div>
        <div className="col-md-4 col-sm-12 mb-3 text-end">
          <Button
            variant="custom-orange"
            className="btn btnorange"
            onClick={handleShowAddAuthor}
          >
            <i className="bi bi-plus-lg"></i> Add New Author
          </Button>
        </div>
      </div>

      <div className="row">
        <div className="col-md-1 col-sm-0"></div>
        <div className="col-md-10 col-sm-12">
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
              <Table
                striped
                bordered
                hover
                responsive
                className="shadow table-striped"
              >
                <thead>
                  <tr className="table-header">
                    <th style={{ backgroundColor: "#daa556" }}>Author Name</th>
                    <th style={{ backgroundColor: "#daa556" }}>Display Name</th>
                    <th style={{ backgroundColor: "#daa556" }}>Biography</th>
                    <th style={{ backgroundColor: "#daa556" }}>Edit</th>
                    <th style={{ backgroundColor: "#daa556" }}>Delete</th>
                  </tr>
                </thead>
                <tbody>
                  {paginatedData.length > 0 ? (
                    paginatedData.map((author) => (
                      <tr key={author.authorID}>
                        <td>{author.name}</td>
                        <td>{author.displayName}</td>
                        <td>{author.biography}</td>
                        <td>
                          <button
                            type="button"
                            className="btn btnorange"
                            onClick={() =>
                              handelShowEditAuthor(author.authorID)
                            }
                          >
                            <i className="bi bi-pen-fill"></i>
                          </button>
                        </td>
                        <td>
                          <button
                            type="button"
                            className="btn btnorange"
                            onClick={() =>
                              handelShowDeleteAuthor(author.authorID)
                            }
                          >
                            <i className="bi bi-trash-fill"></i>
                          </button>
                        </td>
                      </tr>
                    ))
                  ) : (
                    <tr>
                      <td colSpan="5">
                        <div
                          id="noresult"
                          className="text-center bg-light text-danger"
                        >
                          No Results Found !!!
                        </div>
                      </td>
                    </tr>
                  )}
                </tbody>
              </Table>
              <div className="d-flex justify-content-between align-items-center">
                <span>
                  Showing {startIndex + 1} to {endIndex} of {totalItems} results
                </span>
                <Pagination className="custom-pagination">
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
        <div className="col-md-1 col-sm-0"></div>
      </div>

      <NewAuthor
        show={showAddAuthor}
        handleClose={handleCloseAddAuthor}
        errors={errors}
        setErrors={(errors) =>
          dispatch({ type: "SET_ERRORS", payload: errors })
        }
      />

      <EditAuthor
        show={showEditAuthor}
        handleClose={handleCloseEditAuthor}
        errors={errors}
        author={author}
        setAuthor={setAuthor}
        setErrors={(errors) =>
          dispatch({ type: "SET_ERRORS", payload: errors })
        }
      />

      <DeleteAuthor
        show={showDeleteAuthor}
        handleClose={handleCloseDeleteAuthor}
        author={author}
      />
    </div>
  );
}
