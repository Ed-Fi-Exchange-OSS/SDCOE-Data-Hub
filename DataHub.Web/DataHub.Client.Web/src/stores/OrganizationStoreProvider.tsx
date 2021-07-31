import React, { ReactNode } from 'react';

import { StoreProvider } from '../utilities';
import { organizationStore, organizationStoreContext } from './OrganizationStore';

export const OrganizationStoreProvider = ({
  children,
}: {
  children: ReactNode;
}) => {
  return (
    <StoreProvider
      store={organizationStore}
      storeContext={organizationStoreContext}
    >
      {children}
    </StoreProvider>
  );
};
