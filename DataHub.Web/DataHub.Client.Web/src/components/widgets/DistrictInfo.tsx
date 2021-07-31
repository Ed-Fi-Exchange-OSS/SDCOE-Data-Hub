import React from 'react';
import { observer } from 'mobx-react';
import { Badge, Card, ListGroup, ListGroupItem, Table } from 'react-bootstrap';
import { useCRMContactStore, CRMContactEntity, useOrganizationStore } from '../../stores';
import { LoadingPlaceholder } from '../../utilities';

const DistrictInfo = observer(() => {
  const { crmContacts, isLoading } = useCRMContactStore();
  const { currentOrganization } = useOrganizationStore();

  return(
    <LoadingPlaceholder isLoading={isLoading}>
      <Card>
        <Card.Header>District Info</Card.Header>
        <Card.Body>
          <div>
            {(currentOrganization && currentOrganization.sis && <Badge variant="secondary" className="mr-2">SIS: {currentOrganization.sis}</Badge>)}
            {(currentOrganization && currentOrganization.domainantDataSystem && <Badge variant="secondary" className="mr-2">Dominant Data System: {currentOrganization.domainantDataSystem}</Badge>)}
            {(currentOrganization && currentOrganization.analyticsSystem && <Badge variant="secondary" className="mr-2">Analytics System: {currentOrganization.analyticsSystem}</Badge>)}
            {(currentOrganization && currentOrganization.interimAssessments && <Badge variant="secondary" className="mr-2">Interim Assessments: {currentOrganization.interimAssessments}</Badge>)}
          </div>
          <h6 className="mt-3">Contacts</h6>
          <div className='table-responsive' style={{ maxHeight: '250px', overflowX: 'auto', fontSize: '.75rem' }}>
            <Table striped bordered hover size='sm' className='m-0'>
              <thead>
                <tr>
                  <th>Name</th>
                  <th>Title</th>
                  <th>Email</th>
                  <th>Phone</th>
                </tr>
              </thead>
              <tbody>
                {crmContacts.map((contact: CRMContactEntity) => (
                  <tr key={contact.crmContactId} style={{whiteSpace: 'nowrap'}}>
                    <td>{contact.contactName}</td>
                    <td>{contact.contactTitle}</td>
                    <td>{contact.contactEmail}</td>
                    <td>{contact.contactPhone}</td>
                  </tr>
                ))}
              </tbody>
            </Table>
          </div>
        </Card.Body>
      </Card>
    </LoadingPlaceholder>
  );
});

export default DistrictInfo;