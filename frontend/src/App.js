import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/js/bootstrap.bundle.min";

import Home from "./components/home";
import Genre from "./components/genre";
import Book from "./components/book";
import Author from "./components/author";
import Header from "./components/header";
import Footer from "./components/footer";

function App() {
  return (
    <React.Fragment>
      <div className="hero_area">
        <Header></Header>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/genre" element={<Genre />} />
          <Route path="/book" element={<Book />} />
          <Route path="/author" element={<Author />} />
        </Routes>
      </div>
      <Footer></Footer>
    </React.Fragment>
  );
}

export default App;
