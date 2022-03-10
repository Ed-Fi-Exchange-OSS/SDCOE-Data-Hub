import { Server } from "miragejs"

// NOTE: The .env variable REACT_APP_USE_MOCK_SERVER must be set to true for the mock server to be run.
export function makeServer({ environment = "test" } = {}) {
  let server = new Server({ timing: 1000 });

  server.namespace = '/mock/api'

  // EXAMPLE
  // -------
  // server.get('/EdFiRequest', () => [
  //   {
  //     edFiRequestId: 1,
  //     requestDate: '2021-02-25T13:15:00.000Z',
  //     description: 'Set up a 3.4 Ed-Fi ODS with sample data for my districtThis is an example service mock',
  //     requestStatus: 1
  //   },
  //   {
  //     edFiRequestId: 2,
  //     requestDate: '2021-02-27T17:21:00.000Z',
  //     description: 'Set up a new 3.4 Ed-Fi ODS for my district (to connect our SIS to)',
  //     requestStatus: 2
  //   },
  //   {
  //     edFiRequestId: 3,
  //     requestDate: '2021-03-02T08:45:00.000Z',
  //     description: 'Set up a new 2.6 Ed-Fi ODS for my district (to connect our SIS to)',
  //     requestStatus: 3
  //   },
  // ]);

  server.passthrough(request => {
    return request.url.includes('http');
  });

  return server
}