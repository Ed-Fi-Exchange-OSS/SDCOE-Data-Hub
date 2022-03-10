import React, { useState } from 'react';
import { observer } from 'mobx-react';
import { Button, Modal, Table } from 'react-bootstrap';
import { EdFiODSResourceCountEntity, EdFiODSStatusEntity, useEdFiODSStore } from '../../stores';
import { LoadingPlaceholder } from '../../utilities';

const EdFiODSResourceCounts = observer(() => {
  const { edfiODSStatuses, isLoading } = useEdFiODSStore();

  const [showModal, setShowModal] = useState(false);

  const handleClose = () => setShowModal(false);
  const handleShow = () => setShowModal(true);


  return (
    <LoadingPlaceholder isLoading={isLoading}>
      <div className='table-responsive' style={{ maxHeight: '250px', overflowX: 'auto', fontSize: '.75rem' }}>
        <Table striped bordered hover size='sm' className='m-0'>
          <thead>
            <tr style={{ whiteSpace: 'nowrap' }}>
              <th>ODS Name</th>
              <th>Resource</th>
              <th>Count</th>
              <th>Last Checked</th>
            </tr>
          </thead>
          <tbody>
            {edfiODSStatuses.map((edfiODSStatus: EdFiODSStatusEntity) => (
              edfiODSStatus.resourceCounts.map((edfiODSResourceCount: EdFiODSResourceCountEntity, i) => (
              <tr key={`${edfiODSStatus.edFiODSNo}.${edfiODSResourceCount.resourceName}`} style={{ whiteSpace: 'nowrap' }}>
                {(i === 0  && (<td rowSpan={edfiODSStatus.resourceCounts.length}>{edfiODSStatus.odsName}</td>))}
                <td>{edfiODSResourceCount.resourceName}</td>
                <td>{edfiODSResourceCount.resourceCount}</td>
                <td>{new Date(edfiODSResourceCount.lastCheckedDate).toLocaleDateString('en-US')}</td>
              </tr>
              ))
            ))}
          </tbody>
        </Table>
      </div>

      <div className='text-right'>
        <button type="button" className="btn btn-link" onClick={(e) => { handleShow(); }}>Expand</button>
      </div>

      <Modal centered size='lg' show={showModal} onHide={handleClose} >
        <Modal.Header closeButton>
          <Modal.Title>Ed-Fi ODS Status</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className='table-responsive modal-table'>
            <Table striped bordered hover className='m-0 border-primary'>
              <thead>
                <tr style={{ whiteSpace: 'nowrap' }}>
                  <th>ODS Name</th>
                  <th>Resource</th>
                  <th>Count</th>
                  <th>Last Checked</th>
                </tr>
              </thead>
              <tbody>
                {edfiODSStatuses.map((edfiODSStatus: EdFiODSStatusEntity) => (
                  edfiODSStatus.resourceCounts.map((edfiODSResourceCount: EdFiODSResourceCountEntity, i) => (
                  <tr key={`${edfiODSStatus.edFiODSNo}.${edfiODSResourceCount.resourceName}`} style={{ whiteSpace: 'nowrap' }}>
                    {(i === 0  && (<td rowSpan={edfiODSStatus.resourceCounts.length}>{edfiODSStatus.odsName}</td>))}
                    <td>{edfiODSResourceCount.resourceName}</td>
                    <td>{edfiODSResourceCount.resourceCount}</td>
                    <td>{new Date(edfiODSResourceCount.lastCheckedDate).toLocaleDateString('en-US')}</td>
                  </tr>
                  ))
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

export default EdFiODSResourceCounts;