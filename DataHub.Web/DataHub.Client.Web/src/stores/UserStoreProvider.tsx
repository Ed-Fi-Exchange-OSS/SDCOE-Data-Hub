import React, { ReactNode } from 'react';
import { StoreProvider } from '../utilities';
import { userStore, userStoreContext } from './UserStore';

export const UserStoreProvider = ({
  children,
}: {
  children: ReactNode;
}) => {
  return (
    <StoreProvider
      store={userStore}
      storeContext={userStoreContext}
    >
      {children}
    </StoreProvider>
  );
};
