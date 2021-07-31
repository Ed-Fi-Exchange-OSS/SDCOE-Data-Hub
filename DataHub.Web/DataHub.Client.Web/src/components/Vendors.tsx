import React from 'react';
import { Col, Container, Row } from 'react-bootstrap';

const Vendors = () => {
  return (
    <Container>
      <Row className="h1">Vendor Resources</Row>
      {Array.from({ length: 10 }, (_, i) => i + 1).map((i: number) => (
        <Row key={i}>
          <Col>
            <Row className="h2">Vendor {i}</Row>
            <Row>
              <Col>
                <p>
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed
                  in lectus in sem rutrum molestie. Pellentesque pulvinar sem id
                  odio dictum, ornare consectetur eros mattis. Praesent in eros
                  sit amet nulla sollicitudin posuere a consequat erat. Nullam
                  vitae luctus erat, ullamcorper fermentum magna. Suspendisse
                  aliquet ante eros, sed vestibulum tellus consequat vitae.
                </p>
              </Col>
              <Col xs={2} className="col-md-2">
                Come see us at <a href="/">https://vendor{i}.com</a>
              </Col>
            </Row>
          </Col>
        </Row>
      ))}
    </Container>
  );
};

export default Vendors;
