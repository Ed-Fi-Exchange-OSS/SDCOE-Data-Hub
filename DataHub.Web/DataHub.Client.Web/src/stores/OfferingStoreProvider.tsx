import React, { ReactNode } from 'react';

import { StoreProvider } from '../utilities';
import { offeringStore, offeringStoreContext } from './OfferingStore';

export const OfferingStoreProvider = ({
  children,
}: {
  children: ReactNode;
}) => {
  return (
    <StoreProvider
      store={offeringStore}
      storeContext={offeringStoreContext}
    >
      {children}
    </StoreProvider>
  );
};
