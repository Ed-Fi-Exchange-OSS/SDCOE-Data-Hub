import React, { ReactNode } from 'react';

import { StoreProvider } from '../utilities';
import { crmContactStore, crmContactStoreContext } from './CRMContactStore';

export const CRMContactStoreProvider = ({
  children,
}: {
  children: ReactNode;
}) => {
  return (
    <StoreProvider
      store={crmContactStore}
      storeContext={crmContactStoreContext}
    >
      {children}
    </StoreProvider>
  );
};
