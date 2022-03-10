import React from "react";
import { observer } from "mobx-react";
import { Container, Row, Col, Nav, Navbar } from "react-bootstrap";

const Footer = observer(() => {
  return (
    <Container fluid className="text-white">
      <Row className="footer-top">
        <Col>
          <Navbar className="justify-content-center py-4">
            <Navbar.Brand>
              <img src="footer-logo.svg" alt="SDCOE Logo"/>
            </Navbar.Brand>
            <Nav>
              <Nav.Item>
                <p className="font-small text-left mb-0">
                  [YOUR ORGANIZATION] <br />
                  [ADDRESS] <br />
                  [CITY-STATE-ZIP]<br />
                  [PHONE]
                </p>
              </Nav.Item>
            </Nav>
          </Navbar>
        </Col>
      </Row>
      <Row className="theme-bg footer-bottom">
        <Col>
          <div>© 2020 [YOUR ORGANIZATION] All rights reserved</div>
        </Col>
      </Row>
    </Container>
  );
});

export default Footer;
