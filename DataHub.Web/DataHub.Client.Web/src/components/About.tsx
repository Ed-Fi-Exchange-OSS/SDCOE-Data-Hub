import React from 'react';
import { Col, Container, Row } from 'react-bootstrap';

const About = () => {
  return (
    <Container>
      <Row className="h1"><Col>About Us</Col></Row>
      <Row>
        <Col className="text-justify">
            <p className='pt-4 pb-3'>
              The San Diego County Office of Education Data Hub prototype project is envisioned to provide 
              county school districts with a single entry point data portal combining existing district 
              services and Ed-Fi based data product offerings.      
            </p>
            <p className='pb-0'>
              The benefits of a regional data hub include:
            </p>
            <ul className='pb-4'>
              <li className='pb-3'>Uniform high quality data analytics solutions and interoperability services to districts, large and small</li>
              <li className='pb-3'>A single sign-on system to access both specialized dashboards as well as other applications the district may license through SDCOE</li>
              <li className='pb-3'>A source of information for each district about the services and products SDCOE is actively providing, the status of work projects and SDCOE team member schedules and contact information</li>
              <li className='pb-3'>Information about the results of SDCOE’s work with districts, including data about the results and impact of SDCOE’s contribution to district goals and student outcomes</li>
              <li className='pb-0'>Ed-Fi ODS status, related analytics and the ability to request ODS setup and configuration changes</li>        
            </ul>
            <p className='mb-4'>
              The Data Hub prototype was partially funded by a 2019-21 grant from the Michael and Susan Dell Foundation.
            </p>
        </Col>
      </Row>
    </Container>
  );
};

export default About;
