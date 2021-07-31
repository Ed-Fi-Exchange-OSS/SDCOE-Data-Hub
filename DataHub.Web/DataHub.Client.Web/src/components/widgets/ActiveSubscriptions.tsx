import React, { useState } from 'react';
import { observer } from 'mobx-react';
import { Button, Container, Row, Col, Form, Modal, Table } from 'react-bootstrap';
import { useOfferingStore, OfferingEntity } from '../../stores';
import RequireUser from '../RequireUser';
import { LoadingPlaceholder } from '../../utilities';
import ShowMore from 'react-show-more';
import { BiLinkExternal } from "react-icons/bi";
import { decodeHTMLEntity } from '../../utilities'

const ActiveSubscriptions = observer(() => {
  const { offerings, removeParticipation, isParticipatingLoading } = useOfferingStore();

  const [showModal, setShowModal] = useState(false);
  const [showUnsubscribe, setShowUnsubscribe] = useState(false);
  const [selectedService, setSelectedService] = useState({ offeringId: 0, itemNo: 0, name: '' });

  const handleClose = () => setShowModal(false);
  const handleShow = () => setShowModal(true);

  const handleCloseUnsubscribe = () => setShowUnsubscribe(false);
  const handleShowUnsubscribe = () => setShowUnsubscribe(true);

  const handleRowSelect = (offering: OfferingEntity) => {
    if (selectedService.offeringId !== offering.offeringId) {
      setSelectedService(prevState => ({
        ...prevState,
        offeringId: (offering.offeringId) ? offering.offeringId : 0,
        itemNo: offering.itemNo,
        name: offering.itemName
      }));
    } else {
      setSelectedService(prevState => ({ ...prevState, offeringId: 0, itemNo: 0, name: '' }))
    }


  }

  const services = offerings.filter(o => o.itemCategoryType === 2);

  return (
    <LoadingPlaceholder isLoading={isParticipatingLoading}>
      <div className='table-responsive' style={{ maxHeight: '250px', overflowX: 'auto', fontSize: '.75rem' }}>
        <Table striped bordered hover size='sm' className='m-0'>
          <thead>
            <tr>
              <th></th>
              <th>#</th>
              <th style={{ width: '35%' }}>Service Name</th>
              <th style={{ width: '60%' }}>Description</th>
              <th style={{ width: '05%' }}>Contact</th>
            </tr>
          </thead>
          <tbody>
            {services.map((offering: OfferingEntity, i: number) => (
              <tr key={offering.offeringId} >
                <td>
                  <Form.Check type="radio" label="" readOnly
                    checked={offering.offeringId === selectedService.offeringId}
                    onClick={() => handleRowSelect(offering)} />
                </td>
                <td>{offering.itemNo}</td>
                <td>
                  {!offering.productUrl && offering.itemName}
                  {offering.productUrl && (<a href={offering.productUrl} target="_blank" rel="noopener noreferrer">{offering.itemName} <BiLinkExternal /></a>)}
                </td>
                <td>
                  <ShowMore>
                    {decodeHTMLEntity(offering.itemDescription)}
                  </ShowMore>
                </td>
                <td>
                  <b>{offering.contactName}</b><br />
                  {offering.contactEmail}<br />
                  {offering.contactPhone}
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
      </div>

      <Row xs={2} className="mt-2">
        <Col>
          <RequireUser withPermissions={['ManageMyOrganization']}>
            <Button variant="primary" onClick={handleShowUnsubscribe} disabled={selectedService.offeringId === 0}>Remove Subscription</Button>
          </RequireUser>
        </Col>
        <Col className="text-right"><Button variant="link" onClick={(e) => { handleShow(); }}>Expand</Button></Col>
      </Row>

      <Modal centered size='lg' show={showModal} onHide={handleClose} >
        <Modal.Header closeButton>
          <Modal.Title>Active SaaS Subscriptions</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className='table-responsive modal-table'>
            <Table striped bordered hover className='m-0 border-primary'>
              <thead>
                <tr>
                  <th></th>
                  <th>#</th>
                  <th style={{ width: '35%' }}>Service Name</th>
                  <th style={{ width: '60%' }}>Description</th>
                  <th style={{ width: '05%' }}>Contact</th>
                </tr>
              </thead>
              <tbody>
                {services.map((offering: OfferingEntity, i: number) => (
                  <tr key={offering.offeringId} >
                    <td>
                      <Form.Check type="radio" label="" readOnly
                        checked={offering.offeringId === selectedService.offeringId}
                        onClick={() => handleRowSelect(offering)} />
                    </td>
                    <td>{offering.itemNo}</td>
                    <td>
                      {!offering.productUrl && offering.itemName}
                      {offering.productUrl && (<a href={offering.productUrl} target="_blank" rel="noopener noreferrer">{offering.itemName} <BiLinkExternal /></a>)}
                    </td>
                    <td>
                      <ShowMore>
                        {decodeHTMLEntity(offering.itemDescription)}
                      </ShowMore>
                    </td>
                    <td>
                      <b>{offering.contactName}</b><br />
                      {offering.contactEmail}<br />
                      {offering.contactPhone}
                    </td>
                  </tr>
                ))}
              </tbody>
            </Table>
          </div>
        </Modal.Body>
        <Modal.Footer>
          <Container fluid>
            <Row xs={2}>
              <Col className="p-0">
                <RequireUser withPermissions={['ManageMyOrganization']}>
                  <Button variant="primary" onClick={handleShowUnsubscribe} disabled={selectedService.offeringId === 0}>Remove Subscription</Button>
                </RequireUser>
              </Col>
              <Col className="text-right p-0"><Button variant="secondary" onClick={handleClose} className="align-self-end">Close</Button></Col>
            </Row>
          </Container>
        </Modal.Footer>
      </Modal>

      <Modal centered show={showUnsubscribe} onHide={handleCloseUnsubscribe}>

        <Modal.Header closeButton>
          <Modal.Title>Confirm Delete</Modal.Title>
        </Modal.Header>

        <Modal.Body>
          <p>You are about to delete &quot;<b>#{selectedService.itemNo} - {selectedService.name}</b>&quot;.</p>
          <p>Do you want to proceed?</p>
        </Modal.Body>

        <Modal.Footer>
          <Button variant="secondary" onClick={handleCloseUnsubscribe}>Cancel</Button>
          <Button variant="danger" onClick={() => {
            removeParticipation(selectedService.itemNo);
            setSelectedService(prevState => ({ ...prevState, offeringId: 0, itemNo: 0, name: '' }));
            handleCloseUnsubscribe();
          }}>Delete</Button>
        </Modal.Footer>

      </Modal>
    </LoadingPlaceholder>
  );
});

export default ActiveSubscriptions;