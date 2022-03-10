import React from "react";
import { Container, Row, Col } from "react-bootstrap";
import { BiLinkExternal } from "react-icons/bi";

const VendorResources = () => {
  return (
    <Container>
      <Row className="h1 mb-5">
        <Col>Vendor Resources</Col>
      </Row>

      <Row className="mb-3">
        <Col className="h6 text-justify">
          <p>
            The purpose of this page is to provide vendors with resources that
            provide information about the operation of the Data Hub and links to
            external technology sources that underpin this system.
          </p>
        </Col>
      </Row>

      <Row className="mb-5">
        <Col className="text-justify">
          <div className="font-weight-bold h6">
            SDCOE Interoperability Overview
          </div>

          <p>
            The San Diego County Office of Education Data Hub integrates a
            number of technologies and services to give the districts we support
            access to timely information and various systems.
          </p>

          <p>
            There are two types of data integrations supported by the Data Hub:
            Ed-Fi interoperability solutions and API connections.
          </p>

          <p>
            Ed-Fi is an open source database standard that includes associated
            management software and can provide a data interoperability
            structure that can be used to combine data from multiple, different
            student information systems sources. Once in the standard format,
            these data can be used for analytics and reporting purposes or to
            provide data exchanges with external system (e.g. OneRoster, Clever,
            etc.). For more information about Ed-Fi, see
            <a href="https://www.ed-fi.org/" target="_blank" rel="noopener noreferrer">
              {" "}
              https://www.ed-fi.org/ {" "}
              <BiLinkExternal />
            </a>
          </p>

          <p>
            API connections are used within the SDCOE Data Hub to bring together
            information from various systems into a single district view, thus
            providing a single source for district and SDCOE contacts, services
            and programs a district is participating in, and a portal to
            software-as-a-service systems that SDCOE manages on behalf of area
            districts.
            {/* <br/><small>Last retrieved: 2-11-2021</small> */}
          </p>
        </Col>
      </Row>

      <Row className="mb-5">
        <Col className="text-justify">
          <div className="font-weight-bold h6">
            SDCOE Data Hub Vendor Access
          </div>

          <p>
            3rd party software application products that SDCOE manages on
            behalf of area districts can be accessed through the Data Hub as a
            single source of information and data systems. SDCOE manages these
            interfaces by working with application publishers who are contracted
            to provide services.
          </p>
        </Col>
      </Row>

      <Row className="mb-5">
        <Col className="text-justify">
          <div className="font-weight-bold h6">Data Hub Transcript project</div>

          <p>
            SDCOE is working to contribute to the evolution of technologies to
            protect and allow for student/parent control over learner records.
            Our efforts include development of a blockchain-enabled transcript
            system with distributed validation of digital student records to
            development of methods to retrieve student records from student
            information systems and Ed-Fi ODS databases and convert these data
            to a standard transcript JSON digital document. This work is
            partially funded by a grant from the Michael and Susan Dell
            Foundation. For more information, please contact SDCOE Data
            Sciences.
          </p>
        </Col>
      </Row>
    </Container>
  );
};

export default VendorResources;
