import {
  AuthenticatedTemplate,
  UnauthenticatedTemplate,
} from "@azure/msal-react";
import { observer } from "mobx-react";
import { RouterLink } from "mobx-state-router";
import React from "react";
import { BiLinkExternal } from "react-icons/bi";
import { FaUser } from "react-icons/fa";
import { NavLink, NavDropdown, Nav, Navbar, Button, Container, Row, Col } from "react-bootstrap";
import { RequireUser } from ".";
import { useUserStore } from "../stores";
import { loginRequest, msalInstance } from "../utilities";

interface Props { }

const Navigation = observer((props: Props) => {
  const { displayName } = useUserStore();

  return (
    <Container fluid>
      <Row className="header-top">
        <Col>
          <Navbar
            variant="light"
            expand="md"
          >
            <Navbar.Brand>
              <a href="https://[YOUR SITE URL]/Pages/Home.aspx" target="_blank" rel="noopener noreferrer" className="header-logo">
                <img src="header-logo.svg" alt="SDCOE Logo" />
              </a>
              <span>
                Data Hub
              </span>
            </Navbar.Brand>
            <Nav className="ml-auto">
              <UnauthenticatedTemplate>
                <Button
                  variant="light"
                  onClick={() => msalInstance.loginRedirect(loginRequest)}
                >
                  <FaUser style={{ margin: "-5px 5px 0 0" }} />
                    Sign in
                  </Button>
              </UnauthenticatedTemplate>
              <AuthenticatedTemplate>
                <NavDropdown title={displayName} id="nav-user-menu">
                  <NavDropdown.Item onClick={() => msalInstance.logout()}>
                    Log out
                    </NavDropdown.Item>
                </NavDropdown>
              </AuthenticatedTemplate>
            </Nav>
          </Navbar>
        </Col>
      </Row>
      <Row className="header-bottom">
        <Col>
          <Navbar
            variant="light"
            expand="md"
            className="py-0"
          >
            <Navbar.Toggle aria-controls="nav-collapse-menu" className="my-1"/>
            <Navbar.Collapse className="justify-content-end" id="nav-collapse-menu">
              <Nav>
                <RouterLink
                  routeName="home"
                  className="nav-link"
                  activeClassName="active"
                >
                  Home
                </RouterLink>
                <RequireUser withPermissions={["ViewMyOrganization"]}>
                  <RouterLink
                    routeName="dashboard"
                    className="nav-link"
                    activeClassName="active"
                  >
                    Dashboard
                  </RouterLink>
                </RequireUser>
                <RouterLink
                  routeName="openDataPortal"
                  params={{ id: "open-data-portal" }}
                  className="nav-link"
                  activeClassName="active"
                >
                  Open Data Portal
                </RouterLink>
                <RouterLink
                  routeName="vendorResources"
                  className="nav-link"
                  activeClassName="active"
                >
                  Vendor Resources
                </RouterLink>
                <RouterLink
                  routeName="about"
                  className="nav-link"
                  activeClassName="active"
                >
                  About Us
                </RouterLink>
                <UnauthenticatedTemplate>
                  <NavLink active={false} href="https://[YOUR SITE URL]" target="_blank" rel="noopener noreferrer">
                    Student Transcript <BiLinkExternal />
                  </NavLink>
                </UnauthenticatedTemplate>
              </Nav>
            </Navbar.Collapse>
          </Navbar>
        </Col>
      </Row>
    </Container>
  );
});

export default Navigation;
