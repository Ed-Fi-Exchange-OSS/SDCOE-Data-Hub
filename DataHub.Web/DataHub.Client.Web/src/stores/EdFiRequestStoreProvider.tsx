import React, { ReactNode } from 'react';

import { StoreProvider } from '../utilities';
import { edFiRequestStore, edFiRequestStoreContext } from './EdFiRequestStore';

export const EdFiRequestStoreProvider = ({
  children,
}: {
  children: ReactNode;
}) => {
  return (
    <StoreProvider
      store={edFiRequestStore}
      storeContext={edFiRequestStoreContext}
    >
      {children}
    </StoreProvider>
  );
};
