import wretch from 'wretch';
import { API_URL } from '../Config';
import { loginRequest, msalInstance, sleep } from '../utilities';
import { OrganizationModel, AnnouncementModel, OfferingModel, EdFiRequestModel, CRMContactModel, ExtractModel, EdFiODSStatusModel, SupportModel, UserModel } from '../models';
import { BrowserAuthError } from '@azure/msal-browser';

export class Api {
  _makeRequest = async (url: string, opts: object = {}) => {

    let wretchBase = wretch(API_URL)
      .url(url)
      .query(opts);


    // There's a chance that the first API request comes before the token,
    // so retry a few times before assuming failure
    let accessToken = null;
    let retriesRemaining = 3;
    do {
      retriesRemaining--;
      try {
        // Note: MSAL appears to expect client applications to offer a UI
        // mechanism to "select" an account currently authenticated. Currently
        // taking a naive approach of selecting the first account.
        let accounts = await msalInstance.getAllAccounts();
        if (accounts && accounts.length > 0) {
          msalInstance.setActiveAccount(accounts[0]);
        }

        let token = await msalInstance.acquireTokenSilent(loginRequest);
        accessToken = token.idToken;
        wretchBase = wretchBase.auth(`Bearer ${accessToken}`);
      }
      catch (err) {
        if (retriesRemaining < 0) {
          if (err instanceof BrowserAuthError) {
            console.info('User not logged in.')
          }
          else {
            console.error(err);
          }
          return Promise.reject();
        }
      }
      await sleep(500);
    } while (accessToken === null && retriesRemaining >= 0);

    let localOrganizationId:string = sessionStorage.getItem("LocalOrganizationID") || "";
    if (localOrganizationId !== "") {
      localOrganizationId = JSON.parse(localOrganizationId);
      wretchBase = wretchBase.headers({ "x-local-organization-id": localOrganizationId })
    }

    return wretchBase
      .catcher(401, async (err, req) => {
        return req;
      })
      .catcher(500, (err) => {
        console.error(err);
        return Promise.reject(err.message);
      })
      .catcher(404, err => {
        return Promise.reject(err.message);
      })
      .catcher(400, err => {
        return Promise.reject(err.message);
      });
  }
  // todo: handle connection refused (server down/not there)

  _base = async (url: string, opts: object = {}) => {
    return (await this._makeRequest(url, opts));
  }

  getOrganization = async () => (await this._base('/api/Organization')).get().json() as Promise<OrganizationModel>;
  getAllOrganizations = async () => (await this._base('/api/Organization/all')).get().json() as Promise<OrganizationModel[]>;
  getAnnouncements = async () => (await this._base('/api/Announcement')).get().json() as Promise<AnnouncementModel[]>;
  getParticipatingOfferings = async () => (await this._base('/api/Offering/participating')).get().json() as Promise<OfferingModel[]>;
  getAvailableOfferings = async () => (await this._base('/api/Offering/available')).get().json() as Promise<OfferingModel[]>;
  addParticipation = async (itemNo:number) => (await this._base("/api/Offering/participating/" + itemNo)).put(null) as Promise<Response>;
  removeParticipation = async (itemNo: number) => (await this._base("/api/Offering/participating/" + itemNo)).delete() as Promise<Response>;
  getEdFiRequests = async () => (await this._base('/api/EdFiRequest')).get().json() as Promise<EdFiRequestModel[]>;
  getEdFiRequestTypes = async () => (await this._base('/api/EdFiRequest/RequestType')).get().json() as Promise<any>;
  addEdFiRequest = async (req:EdFiRequestModel) => (await this._base('/api/EdFiRequest')).post(req).json() as Promise<EdFiRequestModel>;
  updateEdFiRequest = async (id:number, req:EdFiRequestModel) => (await this._base('/api/EdFiRequest/'+id)).put(req).json() as Promise<EdFiRequestModel>;
  getEdFiODSStatuses = async () => (await this._base('/api/EdFiODS')).get().json() as Promise<EdFiODSStatusModel[]>;
  getExtracts = async () => (await this._base('/api/Extract')).get().json() as Promise<ExtractModel[]>;
  getCRMContacts = async () => (await this._base('/api/CRMContact')).get().json() as Promise<CRMContactModel[]>;
  getSupports = async () => (await this._base('/api/Support')).get().json() as Promise<SupportModel[]>;
  getMe = async () => (await this._base('/api/me')).get().json() as Promise<UserModel>;


  // Mocked server responses can be created using a pattern like the one below. ServerMock.ts intercepts
  // requests beginning with /mock/api and passes all other requests through.
  // NOTE: The .env variable REACT_APP_USE_MOCK_SERVER must be set to true for the mock server to be run.

  // getEdFiODSStatuses = async () => (await fetch("/mock/api/ODSStatus").then((response) => response.json())) as Promise<EdFiODSStatusModel[]>;

}

export default new Api();
