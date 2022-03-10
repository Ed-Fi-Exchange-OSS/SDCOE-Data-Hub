import { action, computed, makeObservable, observable, runInAction } from 'mobx';
import { createContext, useContext } from 'react';
import { toast } from 'react-toastify';

import { api } from '../api';
import { SupportModel } from '../models';

export class SupportEntity {
  @observable supportId: string;
  @observable systemId: string;
  @observable ticketId: string;
  @observable description: string;
  @observable status: number;

  constructor(support: SupportModel) {
    this.supportId = support.supportId;
    this.systemId = support.systemId;
    this.ticketId = support.ticketId;
    this.description = support.description;
    this.status = support.status;
  }
}

export class SupportStore {

  constructor () {
    makeObservable(this);
  }

  @observable isLoading: boolean = false;

  supportRegistry = observable.map<string, SupportEntity>();

  @computed get supports() {
    return Array.from(this.supportRegistry.values());
  }


  // async stuff
  @action
  getSupports = async (refresh: boolean = false) => {
    if (!this.isLoading) {
      this.isLoading = true;
      try {
        if(refresh) this.supportRegistry.clear();
        const resp: SupportModel[] = await api.getSupports();
        runInAction(() => {      
          resp.forEach((a: SupportModel) => {
            this.supportRegistry.set(a.supportId, new SupportEntity(a));
          });
        });
      }
      catch (err) {
        toast.error('Error loading Service Now tickets...');
        console.error(err);
      }
      finally {
        this.isLoading = false;
      }
    }
  };
}

export const supportStore = new SupportStore();

export const supportStoreContext = createContext<SupportStore | null>(null);

export const useSupportStore = () => {
  const store = useContext(supportStoreContext);
  if (!store) {
    throw new Error('You forgot to use SupportStore!');
  }
  return store;
};

