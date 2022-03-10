import React from 'react';
import { About, Dashboard, Error, Home, NotFound, VendorResources, OpenDataPortal } from './components';

export const viewMap = {
  about: <About />,
  dashboard: <Dashboard />,
  home: <Home />,
  vendorResources: <VendorResources />,
  notFound: <NotFound />,
  error: <Error />,
  openDataPortal: <OpenDataPortal />
};
