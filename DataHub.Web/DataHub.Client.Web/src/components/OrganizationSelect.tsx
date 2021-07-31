import React from 'react';
import { observer } from 'mobx-react';
import { toast } from 'react-toastify';
import { Form } from 'react-bootstrap';
import { useStorageState } from 'react-storage-hooks';
import { OrganizationEntity, useAnnouncementStore, useCRMContactStore, useEdFiODSStore, useEdFiRequestStore, useExtractStore, useOfferingStore, useOrganizationStore, useSupportStore } from '../stores';

const OrganizationSelect = observer(() => {
  const { organizations, getCurrentOrganization } = useOrganizationStore();
  const { getAnnouncements } = useAnnouncementStore();
  const { getParticipatingOfferings, getAvailableOfferings } = useOfferingStore();
  const { getEdFiRequests } = useEdFiRequestStore();
  const { getEdFiODSStatuses } = useEdFiODSStore();
  const { getCRMContacts } = useCRMContactStore();
  const { getExtracts } = useExtractStore();
  const { getSupports } = useSupportStore();  


  const [localOrganizationID, setLocalOrganizationID] = useStorageState(sessionStorage, "LocalOrganizationID", '');

  const handleSetLocalOrganizationID = ((localOrgID: string = "") => {

    if (localOrganizationID !== "" && localOrgID === localOrganizationID)
      return;

    const organization = organizations.find(o => o.localOrganizationID === localOrgID) || organizations[0];

    if (organization) {
      toast.dismiss();
      setLocalOrganizationID(organization.localOrganizationID);
      getCurrentOrganization();
      getAnnouncements(true);
      getParticipatingOfferings(true);
      getAvailableOfferings(true);
      getEdFiRequests(true);
      getEdFiODSStatuses(true);
      getCRMContacts(true);
      getExtracts(true);
      getSupports(true);
    }
  })

  return (
    <Form.Control as="select" onChange={(e) => handleSetLocalOrganizationID(e.target.value)} value={localOrganizationID} style={{ margin: "0 5px" }} disabled={organizations.length<=1}>
      <option value="" hidden disabled>Select a district...</option>
      {organizations.map((organization: OrganizationEntity, i: number) => (
        <option key={organization.localOrganizationID} value={organization.localOrganizationID}>
          {organization.organizationName}
        </option>
      ))}
    </Form.Control>
  );
});

export default OrganizationSelect;
