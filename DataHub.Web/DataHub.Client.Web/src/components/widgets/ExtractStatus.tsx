import React, { useState } from 'react';
import { observer } from 'mobx-react';
import { Button, Modal, Table } from 'react-bootstrap';
import { ExtractEntity, useExtractStore } from '../../stores';
import { LoadingPlaceholder } from '../../utilities';

const ExtractStatus = observer(() => {
  const { extracts, isLoading } = useExtractStore();

  const [showModal, setShowModal] = useState(false);

  const handleClose = () => setShowModal(false);
  const handleShow = () => setShowModal(true);


  return (
    <LoadingPlaceholder isLoading={isLoading}>
      <div className='table-responsive' style={{ maxHeight: '250px', overflowX: 'auto', fontSize: '.75rem' }}>
        <Table striped bordered hover size='sm' className='m-0'>
          <thead>
            <tr>
              <th>Name</th>
              <th>Job</th>
              <th>Frequency</th>
              <th>Status</th>
              <th>Date</th>
            </tr>
          </thead>
          <tbody>
            {extracts.map((extract: ExtractEntity, i: number) => (
              <tr key={extract.extractId} style={{whiteSpace: 'nowrap'}}>
                <td>{extract.organizationAbbreviation}</td>
                <td>{extract.extractJobName}</td>
                <td>{extract.extractFrequency}</td>
                <td>{extract.extractLastStatus}</td>
                <td>{new Date(extract.extractLastDate).toLocaleDateString("en-US")}</td>
              </tr>
            ))}
          </tbody>
        </Table>
      </div>
      
      <div className='text-right'>
        <button type="button" className="btn btn-link" onClick={(e) => { handleShow(); }}>Expand</button>
      </div>

      <Modal centered size='lg' show={showModal} onHide={handleClose} >
        <Modal.Header closeButton>
          <Modal.Title>Extracts Status</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className='table-responsive modal-table'>
            <Table striped bordered hover className='m-0 border-primary'>
              <thead>
                <tr>
                  <th>Name</th>
                  <th>Job</th>
                  <th>Frequency</th>
                  <th>Status</th>
                  <th>Date</th>
                </tr>
              </thead>
              <tbody>
                {extracts.map((extract: ExtractEntity) => (
                  <tr key={extract.extractId} style={{whiteSpace: 'nowrap'}}>
                    <td>{extract.organizationAbbreviation}</td>
                    <td>{extract.extractJobName}</td>
                    <td>{extract.extractFrequency}</td>
                    <td>{extract.extractLastStatus}</td>
                    <td>{new Date(extract.extractLastDate).toLocaleDateString("en-US")}</td>
                  </tr>
                ))}
              </tbody>
            </Table>
          </div>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>Close</Button>
        </Modal.Footer>
      </Modal>        
    </LoadingPlaceholder>
  );

});

export default ExtractStatus;
