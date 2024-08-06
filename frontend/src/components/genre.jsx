import Button from "react-bootstrap/Button";
import { Table, Pagination } from "react-bootstrap";
import Spinner from "react-bootstrap/Spinner";
import { useState, useEffect, useReducer } from "react";
import { useFetch } from "../services/useFetch";
import { getAllData } from "../services/service";
import NewGenre from "./addgenre";

const initialState = {
  showAddGenre: false,
  errors: {},
};

function reducer(state, action) {
  switch (action.type) {
    case "SHOW_ADD_GENRE":
      return { ...state, showAddGenre: true, errors: {} };
    case "HIDE_ADD_GENRE":
      return { ...state, showAddGenre: false, errors: {} };

    case "SET_ERRORS":
      return { ...state, errors: action.payload };
    default:
      return state;
  }
}

export default function Genre() {
  const [genres, setGenres] = useState([]);
  const [state, dispatch] = useReducer(reducer, initialState);
  const { showAddGenre, errors } = state;

  const [currentPage, setCurrentPage] = useState(1);
  const [loading, setLoading] = useState(true); // Loading state

  const genresApiUrl = "https://localhost:7088/api/Genres/GetAllGenresWithBookCount";
  const [genresData] = useFetch(genresApiUrl);

  useEffect(() => {
    if (genresData) {
      setGenres(genresData);
      console.log("genres",genresData);
      setLoading(false);
    }
  }, [genresData]);

  const itemsPerPage = 5;
  const totalItems = genres.length;
  const totalPages = Math.ceil(totalItems / itemsPerPage);
  const startIndex = (currentPage - 1) * itemsPerPage;
  const endIndex = Math.min(startIndex + itemsPerPage, totalItems);
  const paginatedData = genres.slice(startIndex, endIndex);

  function handleShowAddGenre() { 
    dispatch({ type: "SHOW_ADD_GENRE" });
  }
  function handleCloseAddGenre() {
    dispatch({ type: "HIDE_ADD_GENRE" });
    refetchGenres();
  }

  function handlePageChange(pageNumber) {
    setCurrentPage(pageNumber);
  }

  function refetchGenres() {
    getAllData(genresApiUrl)
      .then((data) => {
        setGenres(data);
        setLoading(false); // Set loading to false once data is fetched
      })
      .catch((error) => console.error("Error fetching  genres:", error));
  }

  return (
    <div>
      <div className="container mt-5">
        <div className="row mb-3">
          <div className="col-11 text-end">
            <Button
              variant="custom-orange"
              className="btn btnorange"
              onClick={handleShowAddGenre}
            >
              <i className="bi bi-plus-lg"></i> Add New Genre
            </Button>
          </div>
        </div>
        <div className="row">
          <div className="col-1"></div>
          <div className="col-10">
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
                  responsive="true"
                  hover
                  className="shadow table-striped"
                >
                  <thead>
                    <tr className="table-header">
                      <th style={{ backgroundColor: "#daa556" }}>Genre Name</th>
                      <th style={{ backgroundColor: "#daa556" }}>
                        Total No Books
                      </th>
                    </tr>
                  </thead>
                  <tbody>
                    {paginatedData.length > 0 ? (
                      paginatedData.map((genre) => (
                        <tr key={genre.genreID}>
                          <td>{genre.genreName}</td>
                          <td>{genre.bookCount}</td>
                        </tr>
                      ))
                    ) : (
                      <tr>
                        <td colSpan="7">
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
                    Showing {startIndex + 1} to {endIndex} of {totalItems}{" "}
                    results
                  </span>
                  <Pagination responsive="true" className="custom-pagination">
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
          <div className="col-1"></div>
        </div>
      </div>
      <NewGenre
        show={showAddGenre}
        handleClose={handleCloseAddGenre}
        errors={errors}
        setErrors={(errors) =>
          dispatch({ type: "SET_ERRORS", payload: errors })
        }
      />
    </div>
  );
}
