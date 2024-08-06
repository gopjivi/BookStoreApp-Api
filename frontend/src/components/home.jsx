import React from "react";
import { Container, Row, Col, Card } from "react-bootstrap";

export default function Home() {
  return (
    <>
      <Container
        fluid
        className="d-flex align-items-center bg-light home-container"
      >
        <Row className="w-100">
          <Col
            md={6}
            className="d-flex justify-content-center align-items-center"
          >
            <div>
              <h1>
                WELCOME TO
                <br />
                <span>NOVEL NOOK</span>
              </h1>
              <p>
                Your journey to a world of books starts here.
                <br /> Explore available options for handling Genres,
                <br /> Authors and Books details and manage your bookstore
                efficiently.
              </p>
              <a href="#cardindex" className="main_btn">
                Explore
              </a>
            </div>
          </Col>
          <Col
            md={6}
            className="d-flex justify-content-center align-items-center"
          >
            <img
              src="/images/homeimage.png"
              alt="Welcome"
              className="img-fluid"
            />
          </Col>
        </Row>
      </Container>
      <div className="services">
        <div className="services_box">
          <div className="services_card">
            <i className="fa-solid fa-truck-fast"></i>
            <h3>Availability</h3>
            <p>Check whether the book is currently available.</p>
          </div>

          <div className="services_card">
            <i className="fa-solid fa-headset"></i>
            <h3>Tracking</h3>
            <p>Assign and track the specific storage location of each book.</p>
          </div>

          <div className="services_card">
            <i className="fa-solid fa-tag"></i>
            <h3>Offers</h3>
            <p>Create and manage special offers for selected books.</p>
          </div>

          <div className="services_card">
            <i className="fa-solid fa-lock"></i>
            <h3>Search & Filter</h3>
            <p>Easily track books using advanced search and filter options.</p>
          </div>
        </div>
      </div>

      <div className="container" id="cardindex">
        <Card
          style={{ marginBottom: "30px", marginTop: "30px" }}
          className="shadow"
        >
          <Row className="g-0">
            <Col xs={12} md={5} style={{ paddingRight: "10px" }}>
              <Card.Img
                src="/images/genres.png"
                variant="top"
                style={{ height: "100%", objectFit: "cover" }}
              />
            </Col>
            <Col xs={12} md={7}>
              <Card.Header
                style={{ backgroundColor: "#daa556", fontSize: "2rem" }}
              >
                GENRES
              </Card.Header>
              <Card.Body>
                <Card.Text>
                  <p style={{ lineHeight: "2" }}>
                    The Genres section is used to manage the available genres
                    for books in the store. Here’s what you can do:
                  </p>
                  <div>
                    <ul>
                      <li>
                        Add New Genre: Use the "
                        <i className="bi bi-plus-lg"></i> Add New Genre" button
                        to add new genres to the list.
                      </li>
                      <li>
                        Total Number of Books: The section displays the total
                        number of books available for each genre.
                      </li>
                    </ul>
                    Each book is mapped to particular genre.
                  </div>
                </Card.Text>
              </Card.Body>
            </Col>
          </Row>
        </Card>

        <Card
          style={{ marginBottom: "30px", marginTop: "30px" }}
          className="shadow"
        >
          <Row className="g-0">
            <Col xs={12} md={5} style={{ paddingRight: "10px" }}>
              <Card.Img
                src="/images/authors.jpg"
                variant="top"
                style={{ height: "100%", objectFit: "cover" }}
              />
            </Col>
            <Col xs={12} md={7}>
              <Card.Header
                style={{ backgroundColor: "#daa556", fontSize: "2rem" }}
              >
                AUTHORS
              </Card.Header>
              <Card.Body>
                <Card.Text>
                  <p style={{ lineHeight: "2" }}>
                    The Authors section is used to manage author details. Here’s
                    what you can do:
                  </p>
                  <div>
                    <ul>
                      <li>
                        Add New Author: Use the "
                        <i className="bi bi-plus-lg"></i> Add New Author" button
                        to add new authors.
                        <ul>
                          <li>Author Name: Enter the name of the author.</li>
                          <li>
                            Display Name: Enter a unique display name for
                            identification.
                          </li>
                          <li>Biography: Enter the biography of the author.</li>
                        </ul>
                      </li>
                      <li>
                        Edit Author Details: Use this icon{" "}
                        <i className="bi bi-pen-fill"></i> to edit author
                        details.
                      </li>
                      <li>
                        Delete Author: Use this icon{" "}
                        <i className="bi bi-trash-fill"></i> to delete an
                        author. Note: Deleting an author will also delete the
                        books associated with that author.
                      </li>
                      <li>
                        Search: Use the search option to search for authors by
                        name.
                      </li>
                    </ul>
                    Each book is mapped to a particular Author.
                  </div>
                </Card.Text>
              </Card.Body>
            </Col>
          </Row>
        </Card>

        <Card
          style={{ marginBottom: "30px", marginTop: "30px" }}
          className="shadow"
        >
          <Row className="g-0">
            <Col xs={12} md={5} style={{ paddingRight: "10px" }}>
              <Card.Img
                src="/images/books.jpeg"
                variant="top"
                style={{ height: "100%", objectFit: "cover" }}
              />
            </Col>
            <Col xs={12} md={7}>
              <Card.Header
                style={{ backgroundColor: "#daa556", fontSize: "2rem" }}
              >
                BOOKS
              </Card.Header>
              <Card.Body>
                <Card.Text>
                  <p style={{ lineHeight: "2" }}>
                    The Books section is used to manage book details. Here’s
                    what you can do:
                  </p>
                  <div>
                    <ul>
                      <li>
                        Add New Book: Use the "<i className="bi bi-plus-lg"></i>{" "}
                        Add New Book" button to add new books.
                        <ul>
                          <li>Book Title: Enter the title of the book.</li>
                          <li>Language: Choose the language of the book.</li>
                          <li>Author: Select the author of the book.</li>
                          <li>Genre: Choose the genre of the book.</li>
                          <li>Price: Enter the price of the book.</li>
                          <li>
                            Publication Date: Enter the publication date of the
                            book.
                          </li>
                          <li>
                            Available Quantity: Enter the available quantity of
                            the book.
                          </li>
                          <li>
                            Storage Section and Shelf: Assign the book to a
                            storage section and shelf. It's suggested to group
                            books by language and genre, but you can change this
                            if needed.
                          </li>
                          <li>
                            Offer List: Add the book to the offer list if
                            applicable.
                          </li>
                          <li>Book Cover: Upload the book cover.</li>
                        </ul>
                      </li>
                      <li>
                        Edit Book Details: Use this icon{" "}
                        <i className="bi bi-pen-fill"></i> to edit book details.
                      </li>
                      <li>
                        Delete Book: Use this icon{" "}
                        <i className="bi bi-trash-fill"></i> to delete a book.
                      </li>
                      <li>
                        View Book Details: Use this icon{" "}
                        <i className="bi bi-binoculars-fill"></i> to view all
                        details of a book.
                      </li>
                      <li>
                        Advanced Search and Filter: Use the advanced search and
                        filter options to filter book details.
                      </li>
                    </ul>
                  </div>
                </Card.Text>
              </Card.Body>
            </Col>
          </Row>
        </Card>
      </div>
    </>
  );
}
