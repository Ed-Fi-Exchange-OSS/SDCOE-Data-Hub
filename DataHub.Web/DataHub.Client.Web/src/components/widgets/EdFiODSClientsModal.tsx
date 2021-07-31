import React from 'react';
import { Button, Modal, Table } from "react-bootstrap"
import { EdFiODSClientEntity, EdFiODSStatusEntity } from '../../stores';


type EdFiODSClientModalType = {
  showModal: boolean;
  edfiODSStatus: EdFiODSStatusEntity | null;
  handleClose: () => void;
};

const EdFiODSClientsModal = ({ showModal, edfiODSStatus, handleClose }: EdFiODSClientModalType) => {

  if (!edfiODSStatus) return null;

  return (
    <Modal show={showModal} centered size='lg' onHide={handleClose}>
      <Modal.Header closeButton>
        <Modal.Title>Ed-Fi ODS Clients</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <h5 className="mb-3">{edfiODSStatus.odsName}</h5>
        <div className='table-responsive modal-table'>
          <Table striped bordered hover className='m-0 border-primary'>
            <thead>
              <tr style={{ whiteSpace: 'nowrap' }}>
                <th>Name</th>
                <th>Claim</th>
                <th>Client</th>
                <th>Key</th>
                <th>Secret</th>
                <th>Vendor</th>
              </tr>
            </thead>
            <tbody>
              {edfiODSStatus.edFiOdsClients.map((edfiODSClient: EdFiODSClientEntity, i:number) => (
                <tr key={edfiODSClient.odsKey} style={{ whiteSpace: 'nowrap' }}>
                  <td>{edfiODSClient.applicationName}</td>
                  <td>{edfiODSClient.claimSetName}</td>
                  <td>{edfiODSClient.clientName}</td>
                  <td>{edfiODSClient.odsKey}</td>
                  <td>{edfiODSClient.odsSecret}</td>
                  <td>{edfiODSClient.vendorName}</td>
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
  )
};

export default EdFiODSClientsModal;