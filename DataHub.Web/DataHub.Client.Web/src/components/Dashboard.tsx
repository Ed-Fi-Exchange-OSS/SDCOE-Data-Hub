import { observer } from 'mobx-react';
import React from 'react';
import { Card, CardColumns, Col, Container, Row } from 'react-bootstrap';

import { Async, LoadingPlaceholder } from '../utilities';
import Announcements from './Announcements';
import { DistrictInfo, ServicesList, EdFiRequestList, ExtractStatus, ActiveSubscriptions, AvailableServices, EdFiODSStatus, Supports, EdFiODSResourceCounts } from './widgets';
import { useAppStateStore, useUserStore, useOrganizationStore, useAnnouncementStore, useOfferingStore, useEdFiRequestStore, useEdFiODSStore, useCRMContactStore, useExtractStore, useSupportStore } from '../stores';
import { RequireUser } from '.';
import OrganizationSelect from './OrganizationSelect';

const Dashboard = observer(() => {
  const {
    showDistrictInfo,
    showAnnouncements,
    showActiveSubscriptions,
    showActiveServices,
    showAvailableServices,
    showExtractStatus,
    showEdFiStatus,
    showEdFiRequests,
    showServiceNow
  } = useAppStateStore();

  const { userRole } = useUserStore();
  const { getAnnouncements } = useAnnouncementStore();
  const { getParticipatingOfferings, getAvailableOfferings } = useOfferingStore();
  const { getEdFiRequests } = useEdFiRequestStore();
  const { getEdFiODSStatuses } = useEdFiODSStore();
  const { getCRMContacts } = useCRMContactStore();
  const { getExtracts } = useExtractStore();
  const { getSupports } = useSupportStore();
  const { getOrganizations, getCurrentOrganization } = useOrganizationStore();

  return (
    <RequireUser withPermissions={['ViewMyOrganization']}>
      <Container>
        <Row><Col><h1>Dashboard</h1></Col></Row>
        <Row xs={1} md={2}>
          <Col>
            <Async
              promiseFn={() => getOrganizations(parseInt(userRole) === 1)}
              rejected={(err: Error) => <div>Unable to load data.</div>}
              fulfilled={() => <OrganizationSelect />}
            />
          </Col>
        </Row>
        <Row>
          {showAnnouncements && (
            <Async
              promiseFn={getAnnouncements}
              rejected={(err: Error) => <div>Unable to retrieve announcements. {err.message}</div>}
              fulfilled={() => <Announcements />}
            />
          )}
        </Row>
        <CardColumns className="dash-columns">

          {showDistrictInfo && (
            <Async
              promiseFn={async () => { await getCRMContacts(); await getCurrentOrganization(); }}
              pending={() => <LoadingPlaceholder />}
              rejected={(err: Error) => (
                <div>Unable to retrieve data. {err.message}</div>
              )}
              fulfilled={() => <DistrictInfo />}
            />
          )}

          {showActiveSubscriptions && (
            <Card >
              <Card.Header>Active SaaS Subscriptions</Card.Header>
              <Card.Body >
                <Async
                  promiseFn={getParticipatingOfferings}
                  pending={() => <LoadingPlaceholder />}
                  rejected={(err: Error) => (
                    <div>Unable to retrieve data. {err.message}</div>
                  )}
                  fulfilled={() => <ActiveSubscriptions />}
                />
              </Card.Body>
            </Card>
          )}

          {showActiveServices && (
            <Card >
              <Card.Header>Active SDCOE Services</Card.Header>
              <Card.Body >
                <Async
                  promiseFn={getParticipatingOfferings}
                  pending={() => <LoadingPlaceholder />}
                  rejected={(err: Error) => (
                    <div>Unable to retrieve data. {err.message}</div>
                  )}
                  fulfilled={() => <ServicesList />}
                />
              </Card.Body>
            </Card>
          )}

          {showAvailableServices && (
            <Card >
              <Card.Header>Available SaaS Subscriptions and SDCOE Services</Card.Header>
              <Card.Body >
                <Async
                  promiseFn={getAvailableOfferings}
                  pending={() => <LoadingPlaceholder />}
                  rejected={(err: Error) => (
                    <div>Unable to retrieve data. {err.message}</div>
                  )}
                  fulfilled={() => <AvailableServices />}
                />
              </Card.Body>
            </Card>
          )}

          {showServiceNow && (
            <Card >
              <Card.Header>Service Now</Card.Header>
              <Card.Body >
                <Async
                  promiseFn={getSupports}
                  pending={() => <LoadingPlaceholder />}
                  rejected={(err: Error) => (
                    <div>Unable to retrieve data. {err.message}</div>
                  )}
                  fulfilled={() => <Supports />}
                />
              </Card.Body>
            </Card>
          )}

          {showExtractStatus && (
            <Card >
              <Card.Header>Extracts Status</Card.Header>
              <Card.Body >
                <Async
                  promiseFn={getExtracts}
                  pending={() => <LoadingPlaceholder />}
                  rejected={(err: Error) => (
                    <div>Unable to retrieve data. {err.message}</div>
                  )}
                  fulfilled={() => <ExtractStatus />}
                />
              </Card.Body>
            </Card>
          )}

          {showEdFiStatus && (
            <Card >
              <Card.Header>Ed-Fi ODS Status</Card.Header>
              <Card.Body >
                <Async
                  promiseFn={getEdFiODSStatuses}
                  pending={() => <LoadingPlaceholder />}
                  rejected={(err: Error) => (
                    <div>Unable to retrieve data. {err.message}</div>
                  )}
                  fulfilled={() => <EdFiODSStatus />}
                />
              </Card.Body>
            </Card>
          )}

          {showEdFiStatus && (
            <Card >
              <Card.Header>Ed-Fi ODS Resource Counts</Card.Header>
              <Card.Body >
                <Async
                  promiseFn={getEdFiODSStatuses}
                  pending={() => <LoadingPlaceholder />}
                  rejected={(err: Error) => (
                    <div>Unable to retrieve data. {err.message}</div>
                  )}
                  fulfilled={() => <EdFiODSResourceCounts />}
                />
              </Card.Body>
            </Card>
          )}

          {showEdFiRequests && (
            <Card >
              <Card.Header>Ed-Fi Self-Service</Card.Header>
              <Card.Body >
                <Async
                  promiseFn={getEdFiRequests}
                  pending={() => <LoadingPlaceholder />}
                  rejected={(err: Error) => (
                    <div>Unable to retrieve data. {err.message}</div>
                  )}
                  fulfilled={() => <EdFiRequestList />}
                />
            </Card.Body>
          </Card>
          )}

        </CardColumns>
      </Container>

    </RequireUser>
  );
});

export default Dashboard;
