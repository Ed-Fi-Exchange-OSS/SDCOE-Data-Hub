import React, { useEffect, useState } from 'react';
import { observer } from 'mobx-react';
import { Button, Col, Dropdown, Modal, Row, Table } from 'react-bootstrap';
import { EdFiRequestEntity, useEdFiRequestStore } from '../../stores';
import { EdFiRequestForm } from '.';
import { RequireUser } from '..';
import { LoadingPlaceholder } from '../../utilities';

const EdFiRequestList = observer(() => {
  const { edfiRequests, updateEdFiRequest, archiveEdFiRequest, getEdFiRequestTypes, isRequestsLoading, isRequestTypesLoading } = useEdFiRequestStore();

  const [showExpandedModal, setShowExpandedModal] = useState(false);
  const [showUpdateForm, setShowUpdateForm] = useState(false);
  const [updateRequest, setUpdateRequest] = useState<EdFiRequestEntity | null>(null);
  const [showArchiveModal, setShowArchiveModal] = useState(false);
  const [archiveRequest, setArchiveRequest] = useState<EdFiRequestEntity | null>(null);


  useEffect(() => {
    getEdFiRequestTypes();
  }, [getEdFiRequestTypes]);

  const handleCloseExpand = () => setShowExpandedModal(false);
  const handleShowExpand = () => setShowExpandedModal(true);

  const handleCloseForm = () => setShowUpdateForm(false);
  const handleShowForm = () => setShowUpdateForm(true);

  const handleCloseConfirmUpdate = () => setUpdateRequest(null);
  const handleOnSelectStatus = (request: EdFiRequestEntity, status: number) => setUpdateRequest({ ...request, requestStatus: status });

  const handleCloseArchive = () => setShowArchiveModal(false);
  const handleShowArchive = (request: EdFiRequestEntity) => {
    setArchiveRequest(request);
    setShowArchiveModal(true);
  }
  const handleCloseConfirmArchive = () => setArchiveRequest(null);

  const sortedList = edfiRequests.slice().sort((a, b) => {
    let c = (a.requestDate) ? new Date(a.requestDate).getTime() : new Date().getTime();
    let d = (b.requestDate) ? new Date(b.requestDate).getTime() : new Date().getTime();
    return d - c;
  });

  const statusMap = [
    { value: 1, text: "Requested", color: "primary" },
    { value: 2, text: "In Progress", color: "warning" },
    { value: 3, text: "Completed", color: "success" },
    { value: 4, text: "Denied", color: "danger" }
  ];

  const getStatus = (status: number) => statusMap.find(o => o.value === status) || { value: status, text: "Unknown", color: "secondary" };

  return (
    <LoadingPlaceholder isLoading={isRequestsLoading || isRequestTypesLoading}>
      <Table bordered hover size='sm' className='m-0' style={{ whiteSpace: "nowrap" }}>
        <thead>
          <tr>
            <th>Request Date</th>
            <th>Description</th>
            <th>Status</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {sortedList.map((edfiRequest: EdFiRequestEntity) => (
            <tr key={edfiRequest.edFiRequestId}>
              <td className="text-center">{edfiRequest.requestDate && new Date(edfiRequest.requestDate).toLocaleDateString('us')}</td>
              <td style={{ whiteSpace: "normal" }}>{edfiRequest.description}</td>
              <td className="text-center">
                <RequireUser withPermissions={['CanUpdateEdFiRequestStatus']}
                  showIfNotMet={<Button variant={getStatus(edfiRequest.requestStatus).color} disabled>{getStatus(edfiRequest.requestStatus).text}</Button>}>
                  <Dropdown >
                    <Dropdown.Toggle size="sm" variant={getStatus(edfiRequest.requestStatus).color} style={{ minWidth: 100 }}>
                      {getStatus(edfiRequest.requestStatus).text}
                    </Dropdown.Toggle>
                    <Dropdown.Menu>
                      {statusMap.map((status: any) => (
                        <Dropdown.Item key={status.value} onSelect={() => { handleOnSelectStatus(edfiRequest, status.value) }} disabled={status.value === edfiRequest.requestStatus}>
                          {status.text}
                        </Dropdown.Item>
                      ))}
                    </Dropdown.Menu>
                  </Dropdown>
                </RequireUser>
              </td>
                <td>
                  <RequireUser withPermissions={['CanUpdateEdFiRequestStatus']}>
                    <Button variant="link" onClick={() => { handleShowArchive(edfiRequest); }}>X</Button>
                  </RequireUser>
                </td>
            </tr>
          ))}
        </tbody>
      </Table>

      <Row xs={2} className="mt-2">
        <Col>
          <RequireUser withPermissions={['CanCreateEdFiRequest']}>
            <Button variant="primary" onClick={(e) => { handleShowForm(); }}>New</Button>
          </RequireUser>
        </Col>
        <Col className="text-right"><Button variant="link" onClick={(e) => { handleShowExpand(); }}>Expand</Button></Col>
      </Row>

      <EdFiRequestForm showForm={showUpdateForm} handleCloseForm={handleCloseForm} />

      <Modal centered size='lg' show={showExpandedModal} onHide={handleCloseExpand} >
        <Modal.Header closeButton>
          <Modal.Title>Ed-Fi Self-Service</Modal.Title>
        </Modal.Header>
        <Modal.Body>
            <Table striped bordered hover className='m-0 border-primary'>
            <thead>
              <tr>
                <th>Request Date</th>
                <th>Description</th>
                <th>Status</th>
                <th></th>
              </tr>
            </thead>
            <tbody>
              {sortedList.map((edfiRequest: EdFiRequestEntity) => (
                <tr key={edfiRequest.edFiRequestId}>
                  <td className="text-center">{edfiRequest.requestDate && new Date(edfiRequest.requestDate).toLocaleDateString('us')}</td>
                  <td style={{ whiteSpace: "normal" }}>{edfiRequest.description}</td>
                  <td className="text-center">
                    <RequireUser withPermissions={['CanUpdateEdFiRequestStatus']}
                      showIfNotMet={<Button variant={getStatus(edfiRequest.requestStatus).color} disabled>{getStatus(edfiRequest.requestStatus).text}</Button>}>
                      <Dropdown >
                        <Dropdown.Toggle size="sm" variant={getStatus(edfiRequest.requestStatus).color} style={{ minWidth: 100 }}>
                          {getStatus(edfiRequest.requestStatus).text}
                        </Dropdown.Toggle>
                        <Dropdown.Menu>
                          {statusMap.map((status: any) => (
                            <Dropdown.Item key={status.value} onSelect={() => { handleOnSelectStatus(edfiRequest, status.value) }} disabled={status.value === edfiRequest.requestStatus}>
                              {status.text}
                            </Dropdown.Item>
                          ))}
                        </Dropdown.Menu>
                      </Dropdown>
                    </RequireUser>
                  </td>
                  <td>
                    <RequireUser withPermissions={['CanUpdateEdFiRequestStatus']}>
                      <a href="javascript:void;" onClick={() => { handleShowArchive(edfiRequest); }}>X</a>
                    </RequireUser>
                  </td>
                  
                </tr>
              ))}
            </tbody>
            </Table>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleCloseExpand}>Close</Button>
        </Modal.Footer>
      </Modal>

      <Modal centered show={updateRequest !== null} onHide={handleCloseConfirmUpdate}>
        <Modal.Header closeButton>
          <Modal.Title>Confirm Ed-Fi Request Update</Modal.Title>
        </Modal.Header>

        <Modal.Body>
          <p>Confirm status update for Ed-Fi request:</p>
          <p><b>{updateRequest !== null && updateRequest.description}</b></p>
          <p>Do you want to proceed?</p>
        </Modal.Body>

        <Modal.Footer>
          <Button variant="secondary" onClick={handleCloseConfirmUpdate}>Cancel</Button>
          <Button variant="primary" onClick={() => {
            if (updateRequest !== null) {
              updateEdFiRequest(updateRequest);
            }
            handleCloseConfirmUpdate();
          }}>Confirm</Button>
        </Modal.Footer>

      </Modal>

      <Modal centered show={archiveRequest !== null} onHide={handleCloseConfirmArchive}>
        <Modal.Header closeButton>
          <Modal.Title>Confirm Archive Ed-Fi Request</Modal.Title>
        </Modal.Header>

        <Modal.Body>
          <p>This will archive the Ed-Fi Self-Service request, and it will no longer be visible.</p>
          <p><b>{archiveRequest !== null && archiveRequest.description}</b></p>
          <p><b>{archiveRequest !== null && new Date(archiveRequest.requestDate).toLocaleDateString("en-US")}</b></p>
          <p>Do you want to proceed?</p>
        </Modal.Body>

        <Modal.Footer>
          <Button variant="secondary" onClick={handleCloseConfirmArchive}>Cancel</Button>
          <Button variant="primary" onClick={() => {
            if (archiveRequest !== null) {
              archiveEdFiRequest(archiveRequest);
            }
            handleCloseConfirmArchive();
          }}>Confirm</Button>
        </Modal.Footer>

      </Modal>

    </LoadingPlaceholder>
  );

});

export default EdFiRequestList;