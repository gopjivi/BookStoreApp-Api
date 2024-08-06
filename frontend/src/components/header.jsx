import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import { NavLink } from "reactstrap";
import { NavLink as ReactLink } from "react-router-dom";

function Header() {
  return (
    <header className="header_section">
      <Container>
        <Navbar expand="lg" className="custom_nav-container bg-body-tertiary">
          <Container>
            <Navbar.Brand href="#home">
              <img width="100" src="/images/logo.png" alt="Logo" />
              <span className="store_name">NOVEL NOOK</span>
            </Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
              <Nav className="navbar-nav-center ml-auto">
                <NavLink tag={ReactLink} to="/" activeclassname="active">
                  Home
                </NavLink>
                <NavLink tag={ReactLink} to="/book" activeclassname="active">
                  Books
                </NavLink>
                <NavLink tag={ReactLink} to="/author" activeclassname="active">
                  Authors
                </NavLink>
                <NavLink tag={ReactLink} to="/genre" activeclassname="active">
                  Genres
                </NavLink>
              </Nav>
            </Navbar.Collapse>
          </Container>
        </Navbar>
      </Container>
    </header>
  );
}

export default Header;
