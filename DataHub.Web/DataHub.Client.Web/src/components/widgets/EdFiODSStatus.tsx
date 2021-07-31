import React, { useState } from 'react';
import { observer } from 'mobx-react';
import { Button, Modal, Table } from 'react-bootstrap';
import { BsEye } from 'react-icons/bs'
import { EdFiODSStatusEntity, useEdFiODSStore } from '../../stores';
import { LoadingPlaceholder } from '../../utilities';
import { EdFiODSClientsModal } from '.';

const EdFiODSStatus = observer(() => {
  const { edfiODSStatuses, isLoading } = useEdFiODSStore();

  const [showModal, setShowModal] = useState(false);
  const [showClientsModal, setShowClientsModal] = useState(false);  
  const [selectedEdFiODSStatus, setSelectedEdFiODSStatus] = useState<EdFiODSStatusEntity | null>(null);

  const handleClose = () => setShowModal(false);
  const handleShow = () => setShowModal(true);

  const handleCloseClientsModal = () => setShowClientsModal(false);
  const handleShowClientsModal = (edfiODSStatus: EdFiODSStatusEntity) => {
    setSelectedEdFiODSStatus(edfiODSStatus);
    setShowClientsModal(true);
  }

  return (
    <LoadingPlaceholder isLoading={isLoading}>
      <div className='table-responsive' style={{ maxHeight: '250px', overflowX: 'auto', fontSize: '.75rem' }}>
        <Table striped bordered hover size='sm' className='m-0'>
          <thead>
            <tr style={{ whiteSpace: 'nowrap' }}>
              <th>Name</th>
              <th>Version</th>
              <th>Status</th>
              <th>URL</th>
              <th>Path</th>
              <th>Last Checked</th>
              <th>Clients</th>
            </tr>
          </thead>
          <tbody>
            {edfiODSStatuses.map((edfiODSStatus: EdFiODSStatusEntity) => (
              <tr key={edfiODSStatus.edFiODSNo} style={{ whiteSpace: 'nowrap' }}>
                <td>{edfiODSStatus.odsName}</td>
                <td>{edfiODSStatus.odsVersion}</td>
                <td>{edfiODSStatus.status}</td>
                <td>{edfiODSStatus.odsUrl}</td>
                <td>{edfiODSStatus.odsPath}</td>
                <td>{new Date(edfiODSStatus.lastCheckedDate).toLocaleDateString('en-US')}</td>
                <td><Button variant="link" size="sm" className="py-0" style={{lineHeight:0}} onClick={() => handleShowClientsModal(edfiODSStatus)}><BsEye fontSize="1rem"/></Button></td>
              </tr>
            ))}
          </tbody>
        </Table>
      </div>

      <div className='text-right'>
        <button type="button" className="btn btn-link" onClick={(e) => { handleShow(); }}>Expand</button>
      </div>

      <EdFiODSClientsModal showModal={showClientsModal} edfiODSStatus={selectedEdFiODSStatus} handleClose={handleCloseClientsModal} />

      <Modal centered size='lg' show={showModal} onHide={handleClose} >
        <Modal.Header closeButton>
          <Modal.Title>Ed-Fi ODS Status</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className='table-responsive modal-table'>
            <Table striped bordered hover className='m-0 border-primary'>
              <thead>
                <tr style={{ whiteSpace: 'nowrap' }}>
                  <th>Name</th>
                  <th>Version</th>
                  <th>Status</th>
                  <th>URL</th>
                  <th>Path</th>
                  <th>Last Checked</th>
                  <th>Clients</th>
                </tr>
              </thead>
              <tbody>
                {edfiODSStatuses.map((edfiODSStatus: EdFiODSStatusEntity) => (
                  <tr key={edfiODSStatus.edFiODSNo} style={{ whiteSpace: 'nowrap' }}>
                    <td>{edfiODSStatus.odsName}</td>
                    <td>{edfiODSStatus.odsVersion}</td>
                    <td>{edfiODSStatus.status}</td>
                    <td>{edfiODSStatus.odsUrl}</td>
                    <td>{edfiODSStatus.odsPath}</td>
                    <td>{new Date(edfiODSStatus.lastCheckedDate).toLocaleDateString('en-US')}</td>
                    <td><Button variant="link" size="sm" className="py-0" style={{lineHeight:0}} onClick={() => handleShowClientsModal(edfiODSStatus)}><BsEye fontSize="1rem"/></Button></td>
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

export default EdFiODSStatus;