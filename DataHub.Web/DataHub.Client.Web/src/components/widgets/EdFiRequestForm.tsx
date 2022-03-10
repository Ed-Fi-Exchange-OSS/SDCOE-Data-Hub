import React, { useEffect, useState } from 'react';
import { observer } from 'mobx-react';
import { Button, Form, Modal } from 'react-bootstrap';
import { useAppStateStore, useEdFiRequestStore } from '../../stores';
import { EdFiRequestModel } from '../../models';

type EdFiRequestFormProps = { 
  showForm: boolean; 
  handleCloseForm: () => void; 
};

const EdFiRequestForm = observer((props:EdFiRequestFormProps) => {
  const { isReadOnly } = useAppStateStore();
  const { edfiRequestTypes, getEdFiRequestTypes, addEdFiRequest } = useEdFiRequestStore();

  const [validated, setValidated] = useState(false);
  const [newRequest, setNewRequest] = useState({ requestDate: new Date(), description: "", comments: "" });

  useEffect(() => {
    getEdFiRequestTypes();
  }, [getEdFiRequestTypes]);

  const handleCloseForm = () => {
    setNewRequest({ requestDate: new Date(), description: "", comments: "" });    
    setValidated(false);    
    props.handleCloseForm();
  }

  const handleInputChange = (event: any) => {
    const target = event.target;
    const value = target.type === 'checkbox' ? target.checked : target.value;
    const name = target.name;
    setNewRequest((prevState) => { return { ...prevState, [name]: value } });
  }

  return (
    <Modal show={props.showForm} onHide={handleCloseForm}>
      <Modal.Header closeButton>
        <Modal.Title>New ODS Request</Modal.Title>
      </Modal.Header>

      <Modal.Body>
        <Form noValidate validated={validated} onSubmit={(e) => {

          const form = e.currentTarget;
          e.preventDefault();
          e.stopPropagation();

          if (form.checkValidity() === true) {

            let req: EdFiRequestModel = {
              edFiRequestId: 0,
              requestDate: new Date(newRequest.requestDate),
              description: newRequest.description + "\n" + newRequest.comments,
              requestStatus: 1,
              isArchived: false
            }
            addEdFiRequest(req);
            handleCloseForm();

          } else {
            setValidated(true);
          }

        }}>

          <Form.Group controlId="formRequestDate" >
            <Form.Label>Date</Form.Label>
            <Form.Control type="date" name="requestDate" className="col-6" onChange={handleInputChange} value={newRequest.requestDate.toString()} required />
            <Form.Text className="text-muted">
              The date on which the ODS is to be ready.
              </Form.Text>
          </Form.Group>

          <Form.Group controlId="formDescription">
            <Form.Label>Description</Form.Label>
            <Form.Control as="select" custom name="description" onChange={handleInputChange} required>
              <option hidden value="">Choose...</option>
              {edfiRequestTypes.map((requestType: any, i: number) => (
                <option key={i}>{requestType.description}</option>
              ))}
            </Form.Control>
          </Form.Group>

          <Form.Group controlId="formComments">
            <Form.Label>Comments</Form.Label>
            <Form.Control as="textarea" placeholder="* Additional comments" name="comments" value={newRequest.comments} onChange={handleInputChange} />
          </Form.Group>

          {!isReadOnly && (<Button variant="primary" type="submit">Submit</Button>)}

        </Form>
      </Modal.Body>
    </Modal>
  );

});

export default EdFiRequestForm;