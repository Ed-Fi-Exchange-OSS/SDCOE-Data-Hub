import React from 'react';
import { Col, Container, Row } from 'react-bootstrap';
import { MdCloudDownload } from 'react-icons/md';
import {BiLinkExternal} from 'react-icons/bi';

const OpenDataPortal = () => {

  return (
    <Container>
      <Row className="h1 mb-5"><Col>Open Data Portal</Col></Row>

      <Row className="mb-3">
        <Col className='h6 text-justify'>
          <p>
            Welcome to the San Diego County Office of Education Open Data Portal – 
            This section of our DataHub provides data-related resources to our community 
            and is an example of SDCOE's commitment to transparency. 
          </p>

          <p>
            Below is a list of current dashboards and data sources.  
            Explore a range of resources about San Diego County's schools, districts and the impact of education on students and the community.            
          </p>
        </Col>
      </Row>

      <Row className='mb-5'>
        <Col>

          <a className='font-weight-bold h6'
            href={require("./../public/research-data/CDESchoolDirectoryExport.xlsx")} 
            target="_blank" rel="noopener noreferrer">
            <MdCloudDownload className='mr-2'/>
            San Diego County School and District list
          </a>

          <p>
            This Excel document contains a list of districts, public and charter schools in the county.
            Column Contents: CDS Code, Federal School ID, Federal Charter District ID, County, District, School, 
            Status, Charter Yes/No, Entity Type, Low Grade, High Grade, Public Yes/No, Website, Latitude, Longitude, Last Update, Street, Zip
            <br/><small>Last retrieved: 2-11-2021</small>
          </p>

          <div className='mt-2'>
            <p>QR Code to access source for updated data:</p>
            <img src={require("./../images/qrcode_www.cde.ca.gov.png")} alt='' style={{width:300}}/>
          </div>
          
        </Col>
      </Row>
     
      <Row className='mb-5'>
        <Col>
            <a className='font-weight-bold h6'
              href="https://datastudio.google.com/reporting/2b064b3d-c389-4eeb-9316-077e9340c815/page/Mu4ZB"  
              target="_blank" rel="noopener noreferrer">
                <BiLinkExternal className='mr-2' />
              San Diego County School K-12 District/School locations
            </a>

            <p>
              This dashboard maps the locations of San Diego County school district offices, schools, and COVID testing sites.
            </p>        
        </Col>
      </Row>

      <Row className='mb-5'>
        <Col>
            <a className='font-weight-bold h6'
              href="https://datastudio.google.com/reporting/477fe27c-faec-4809-b364-f333f2b66ddd/page/K5FeB"  
              target="_blank" rel="noopener noreferrer">
                <BiLinkExternal className='mr-2' />
                San Diego County COVID-19 Cases Dashboard
            </a>

            <p>
              This dashboard displays CO​VID-19 cases by Zip Code both in a list and on an area map, 
              with the ability to filter the list by date range.
            </p>        
        </Col>
      </Row>

    </Container>
  );
};

export default OpenDataPortal;
