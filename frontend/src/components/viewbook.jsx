import { Button } from "react-bootstrap";
import Offcanvas from "react-bootstrap/Offcanvas";
import { useEffect, useState } from "react";

export default function ViewBook({ show, handleClose, book }) {
  const [file, setFile] = useState(null);
  const [preview, setPreview] = useState(null);
 console.log(book);
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
          <Offcanvas.Title>View Book Details</Offcanvas.Title>
        </Offcanvas.Header>
        <Offcanvas.Body>
          <div className="container-fluid">
            <div className="row view-book">
              <div className="col">Book Title:</div>
              <div className="col">{book.title}</div>
              <div className="col"></div>
            </div>
            <div className="row view-book">
              <div className="col">Book Price:</div>
              <div className="col">{book.price}</div>
              <div className="col"></div>
            </div>
            <div className="row view-book">
              <div className="col">Language:</div>
              <div className="col">
                {book.language ? book.language.languageName : "Unknown"}
              </div>
              <div className="col"></div>
            </div>
            <div className="row view-book">
              <div className="col">Book Genre:</div>
              <div className="col">
                {book.genre ? book.genre.genreName : "Unknown"}
              </div>
              <div className="col"></div>
            </div>
            <div className="row view-book">
              <div className="col">Author:</div>
              <div className="col">
                {book.author ? book.author.displayName : "Unknown"}
              </div>
              <div className="col"></div>
            </div>
            <div className="row view-book">
              <div className="col">Publication Date:</div>
              <div className="col">{book.publicationDate}</div>
              <div className="col"></div>
            </div>
            <div className="row view-book">
              <div className="col">Edition:</div>
              <div className="col">{book.edition}</div>
              <div className="col"></div>
            </div>
            <div className="row view-book">
              <div className="col">Stock Quantity::</div>
              <div className="col">{book.stockQuantity}</div>
              <div className="col"></div>
            </div>
            <div className="row view-book">
              <div className="col">Storage Section:</div>
              <div className="col">{book.storageSection}</div>
              <div className="col"></div>
            </div>
            <div className="row view-book">
              <div className="col">Storage Shelf:</div>
              <div className="col">
                {" "}
                {book.storageShelf ? book.storageShelf : "N/A"}
              </div>
              <div className="col"></div>
            </div>
            <div className="row view-book">
              <div className="col">Is Offer Available:</div>
              <div className="col">
                {book.isOfferAvailable ? "Yes" : "No"}
              </div>
              <div className="col"></div>
            </div>
            <div className="row view-book">
              <div className="col">Book Cover:</div>
              <div className="col">
                {book.bookImageURL && (
                  <img src={book.bookImageURL} alt="Preview" className="img-thumbnail" />
                )}
              </div>
              <div className="col"></div>
            </div>
            <Button
              variant="custom-orange btnorange"
              type="button"
              onClick={handleClose}
            >
              Back
            </Button>
          </div>
        </Offcanvas.Body>
      </Offcanvas>
    </>
  );
}
