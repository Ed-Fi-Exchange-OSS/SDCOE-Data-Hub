import React, { ReactNode } from 'react';

import { StoreProvider } from '../utilities';
import { edFiODSStore, edFiODSStoreContext } from './EdFiODSStore';

export const EdFiODSStoreProvider = ({
  children,
}: {
  children: ReactNode;
}) => {
  return (
    <StoreProvider
      store={edFiODSStore}
      storeContext={edFiODSStoreContext}
    >
      {children}
    </StoreProvider>
  );
};
