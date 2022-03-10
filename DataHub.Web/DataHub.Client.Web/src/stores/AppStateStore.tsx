import React, { createContext, useReducer } from 'react';

type AppState = {
  showAlerts: boolean;
  showDistrictInfo: boolean;
  showAnnouncements: boolean;
  showActiveSubscriptions: boolean;
  showActiveServices: boolean;
  showAvailableServices: boolean;
  showSupportTickets: boolean;
  showExtractStatus: boolean;
  showEdFiStatus: boolean;
  showEdFiRequests: boolean;
  showServiceNow: boolean;
  isReadOnly: boolean;
  error: string;
};

type AppStateChangeDispatch = (action: AppStateAction) => void;

const initialState: AppState = {
  showAlerts: true,
  showDistrictInfo: true,
  showAnnouncements: true,
  showActiveSubscriptions: true,
  showActiveServices: true,
  showAvailableServices: true,
  showSupportTickets: true,
  showExtractStatus: true,
  showEdFiStatus: true,
  showServiceNow: true,
  showEdFiRequests: true,
  isReadOnly: false,
  error: "",
};

type AppStateAction =
  | { type: "hideAnnouncements" }
  | { type: "setError"; error: string };

const AppStateContext = createContext<AppState | undefined>(initialState);
const AppStateDispatchContext = createContext<
  AppStateChangeDispatch | undefined
>(undefined);
type AppStateProviderProps = { children: React.ReactNode };

const AppStateReducer = (state: AppState, action: AppStateAction): AppState => {
  switch (action.type) {
    case "hideAnnouncements":
      return {
        ...state,
        showAnnouncements: false,
      };
    // case 'SET_POSTS':
    //     return {
    //         ...state,
    //         posts: action.payload
    //     };
    // case 'ADD_POST':
    //     return {
    //         ...state,
    //         posts: state.posts.concat(action.payload)
    //     };
    // case 'REMOVE_POST':
    //     return {
    //         ...state,
    //         posts: state.posts.filter(post => post.id !== action.payload)
    //     };
    case "setError":
      return {
        ...state,
        error: action.error,
      };
    default:
      return state;
  }
};

const AppStateStore = ({ children }: AppStateProviderProps) => {
  const [state, dispatch] = useReducer(AppStateReducer, initialState);
  return (
    <AppStateContext.Provider value={state}>
      <AppStateDispatchContext.Provider value={dispatch}>
        {children}
      </AppStateDispatchContext.Provider>
    </AppStateContext.Provider>
  );
};

function useAppStateStore() {
  const context = React.useContext(AppStateContext);
  if (context === undefined) {
    throw new Error(
      "useAppStateStore must be used from within an AppStateStore"
    );
  }
  return context;
}
function useAppStateDispatch() {
  const context = React.useContext(AppStateDispatchContext);
  if (context === undefined) {
    throw new Error(
      "useAppStateStore must be used from within an AppStateStore"
    );
  }
  return context;
}

export { useAppStateStore, useAppStateDispatch, AppStateStore };
