import React from "react";
import { Container, Row, Col } from "react-bootstrap";

interface Props {}

const Home = (props: Props) => {
  return (
    <Container>
      <Row className="h1 mb-5">
        <Col>Welcome to the SDCOE Data Hub Prototype</Col>
      </Row>
      <Row>
        <Col>
          <p>
            With assistance from the Michael and Susan Dell Foundation and the Ed-Fi Alliance, the purpose of this Data Hub is to provide San Diego County districts and schools access to information, analytics and updates on various data and Ed-Fi systems.  Additionally, an important goal for the Data Hub is to facilitate shared knowledge about the services, applications and supports provided so that both SDCOE and district personnel can manage and monitor our collaborations. Behind the scenes, the Data Hub prototype contains real connections to Ed-Fi ODS's and simulates a number of API connections to various systems, bringing information together from these systems into a single interface.
          </p>
          <p>
            Getting Started: If you have been provided credentials for the hub, use Sign In above right to log into the system.  For visitors without credentials, you are welcome to explore the Open Data Portal and Vendor Resources pages.
          </p>
          <p>
            Note: Sample data is currently displayed within this prototype. Some of the service and application offerings are based on real SDCOE support programs.
          </p>
        </Col>
      </Row>
    </Container>
  );
};

export default Home;
