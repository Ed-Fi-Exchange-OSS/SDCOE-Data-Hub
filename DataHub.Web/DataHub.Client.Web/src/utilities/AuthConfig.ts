import { Configuration, PopupRequest, PublicClientApplication } from '@azure/msal-browser';

// Config object to be passed to Msal on creation
export const msalConfig: Configuration = {
    auth: {
        clientId: "7906cb5d-6892-4fe8-ae35-169209da1f38",
        authority: "https://login.microsoftonline.com/d3e8e085-75b6-493c-be84-79c3cf75247f",
        redirectUri: "http://localhost:3000",
    },
    cache: {
        cacheLocation: 'localStorage'
    },
    system: {
    }
};

// Add here scopes for id token to be used at MS Identity Platform endpoints.
export const loginRequest: PopupRequest = {
    scopes: ["openid"]
};

// Add here the endpoints for MS Graph API services you would like to use.
export const graphConfig = {
    graphMeEndpoint: "https://graph.microsoft.com/v1.0/me"
};

export const msalInstance = new PublicClientApplication(msalConfig);
