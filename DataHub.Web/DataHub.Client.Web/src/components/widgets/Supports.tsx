import React from 'react';
import { observer } from 'mobx-react';
import { Table } from 'react-bootstrap';
import { useSupportStore, SupportEntity } from '../../stores';
import { LoadingPlaceholder } from '../../utilities';

const ActiveSubscriptions = observer(() => {
  const { supports, isLoading } = useSupportStore();

  return (
    <LoadingPlaceholder isLoading={isLoading}>
      <div className='table-responsive' style={{ maxHeight: '250px', overflowX: 'auto', fontSize: '.75rem' }}>
        <Table striped bordered hover size='sm' className='m-0' style={{ whiteSpace: 'nowrap' }}>
          <thead>
            <tr>
              <th>System ID</th>
              <th>Ticket ID</th>
              <th>Description</th>
              <th>Status</th>
            </tr>
          </thead>
          <tbody>
            {supports.map((support: SupportEntity) => (
              <tr key={support.supportId}>
                <td>{support.systemId}</td>
                <td>{support.ticketId}</td>
                <td>{support.description}</td>
                <td>
                  {support.status===0 && "New"}
                  {support.status===1 && "Active"}
                  {support.status===2 && "Closed"}
                </td>
              </tr>
            ))}
          </tbody>
        </Table>
      </div>
    </LoadingPlaceholder>
  );

});

export default ActiveSubscriptions;