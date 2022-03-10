import { MsalProvider } from "@azure/msal-react";
import { ToastContainer } from "react-toastify";
import { observer } from "mobx-react";
import { RouterContext, RouterView } from "mobx-state-router";
import React from "react";
import { Container } from "react-bootstrap";

import { Footer, Navigation } from "./components";
import { initRouter } from "./initRouter";
import { msalInstance } from "./utilities";
import { viewMap } from "./viewMap";
import {
  AppStateStore,
  OrganizationStoreProvider,
  AnnouncementStoreProvider,
  OfferingStoreProvider,
  EdFiRequestStoreProvider,
  ExtractStoreProvider,
  CRMContactStoreProvider,
  SupportStoreProvider,
  UserStoreProvider,
  EdFiODSStoreProvider,
} from "./stores";

const App = observer(() => {
  const routerStore = initRouter();

  return (
    <MsalProvider instance={msalInstance}>
      <UserStoreProvider>
        <AppStateStore>
          <AnnouncementStoreProvider>
            <OfferingStoreProvider>
              <OrganizationStoreProvider>
                <EdFiRequestStoreProvider>
                  <ExtractStoreProvider>
                    <CRMContactStoreProvider>
                      <SupportStoreProvider>
                        <EdFiODSStoreProvider>
                          <RouterContext.Provider value={routerStore}>
                            <Navigation />
                            <Container className="mt-sm-5" style={{minHeight: '100vh'}}>
                              <ToastContainer
                                position="top-right"
                                autoClose={5000}
                                newestOnTop={true}
                                pauseOnHover
                                style={{marginTop: '100px'}}
                              />
                              <RouterView viewMap={viewMap} />
                            </Container>
                            <Footer />
                          </RouterContext.Provider>
                        </EdFiODSStoreProvider>
                      </SupportStoreProvider>
                    </CRMContactStoreProvider>
                  </ExtractStoreProvider>
                </EdFiRequestStoreProvider>
              </OrganizationStoreProvider>
            </OfferingStoreProvider>
          </AnnouncementStoreProvider>
        </AppStateStore>
      </UserStoreProvider>
    </MsalProvider>
  );
});

export default App;
