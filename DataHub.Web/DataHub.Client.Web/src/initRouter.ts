import { browserHistory, createRouterState, HistoryAdapter, RouterStore } from 'mobx-state-router';

const notFound = createRouterState('notFound');

export const routes = [
    {
        name: 'home',
        pattern: '/',
    },
    {
        name: 'dashboard',
        pattern: '/dashboard',
    },
    {
        name: 'vendorResources',
        pattern: '/vendor-resources',
    },    
    {
      name: 'openDataPortal',
      pattern: '/open-data-portal',
    },
    {
        name: 'about',
        pattern: '/about',
    },
    {
        name: 'notFound',
        pattern: '/not-found',
    },
    {
        name: 'unauthorized',
        pattern: '/unauthorized',
    },
    {
        name: 'error',
        pattern: '/error',
    },
];

export function initRouter() {
    const routerStore = new RouterStore(routes, notFound);
    const historyAdapter = new HistoryAdapter(routerStore, browserHistory);
    historyAdapter.observeRouterStateChanges();
    return routerStore;
}
