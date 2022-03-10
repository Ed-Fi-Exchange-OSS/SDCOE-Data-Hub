import React, { ReactNode } from 'react';

import { StoreProvider } from '../utilities';
import { supportStore, supportStoreContext } from './SupportStore';

export const SupportStoreProvider = ({
  children,
}: {
  children: ReactNode;
}) => {
  return (
    <StoreProvider
      store={supportStore}
      storeContext={supportStoreContext}
    >
      {children}
    </StoreProvider>
  );
};
