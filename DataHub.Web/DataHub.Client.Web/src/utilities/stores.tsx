import React from 'react';
import { Context, ReactNode, useContext, useState } from 'react';

export function StoreProvider<T>({
  store: storeProp,
  storeContext,
  children,
}: {
  store: any;
  storeContext: Context<T>;
  children: ReactNode;
}) {
  const [store] = useState(storeProp);
  return (
    <storeContext.Provider value={store}>{children}</storeContext.Provider>
  );
}

// Not used yet
export function useStoreHook<T>(storeContext: React.Context<T>) {
  const store = useContext(storeContext);
  if (!store) {
    // this is especially useful in TypeScript so you don't need to be checking for null all the time
    throw new Error("You didn't use a StoreProvider.");
  }
  return store;
}
