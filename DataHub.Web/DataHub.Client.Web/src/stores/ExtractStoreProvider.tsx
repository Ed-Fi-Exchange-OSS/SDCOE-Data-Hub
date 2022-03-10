import React, { ReactNode } from 'react';

import { StoreProvider } from '../utilities';
import { extractStore, extractStoreContext } from './ExtractStore';

export const ExtractStoreProvider = ({
  children,
}: {
  children: ReactNode;
}) => {
  return (
    <StoreProvider
      store={extractStore}
      storeContext={extractStoreContext}
    >
      {children}
    </StoreProvider>
  );
};
