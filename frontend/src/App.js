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
import { createNewToken } from './services/createtoken';
import { setAPIToken } from './services/config'; // Import the setAPIToken function
import  { useEffect, useState } from 'react';

function App() {
  const [token, setToken] = useState(null);

  useEffect(() => {
    // Define your credentials
    const credentials = {
      userName: 'karpagam',
      password: 'karpagam',
    };
    console.log("app.js is loading");
    // Fetch the token when the component mounts
    createNewToken(credentials)
      .then(response => {
      
        setToken(response.Token); // Save the token in state
        setAPIToken(response.Token); // Set the token in config.js
        console.log("token",response.Token);
      })
      .catch(error => {
        console.error("Error fetching token:", error);
      });
  }, []); // Empty dependency array ensures this runs only once when the component mounts

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
