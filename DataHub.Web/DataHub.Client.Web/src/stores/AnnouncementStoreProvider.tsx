import React, { ReactNode } from 'react';

import { StoreProvider } from '../utilities';
import { announcementStore, announcementStoreContext } from './AnnouncementStore';

export const AnnouncementStoreProvider = ({
  children,
}: {
  children: ReactNode;
}) => {
  return (
    <StoreProvider
      store={announcementStore}
      storeContext={announcementStoreContext}
    >
      {children}
    </StoreProvider>
  );
};
