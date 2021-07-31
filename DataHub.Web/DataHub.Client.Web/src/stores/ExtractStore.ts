import { action, computed, makeObservable, observable, runInAction } from 'mobx';
import { createContext, useContext } from 'react';
import { toast } from 'react-toastify';

import { api } from '../api';
import { ExtractModel } from '../models';

export class ExtractEntity {
  @observable extractId: number;
  @observable extractJobName: string;
  @observable extractLastDate: Date;
  @observable extractLastStatus: string;
  @observable extractFrequency: string;
  @observable organizationAbbreviation: string;
  
  constructor(extract: ExtractModel) {
    this.extractId = extract.extractId;
    this.extractJobName = extract.extractJobName;
    this.extractLastDate = extract.extractLastDate;
    this.extractLastStatus = extract.extractLastStatus;
    this.extractFrequency = extract.extractFrequency;
    this.organizationAbbreviation = extract.organizationAbbreviation;
  }
}

export class ExtractStore {
  
  constructor () {
    makeObservable(this);
  }

  @observable isLoading: boolean = false;

  extractRegistry = observable.map<number, ExtractEntity>();

  @computed get extracts() {
    return Array.from(this.extractRegistry.values());
  }

  // async stuff
  @action
  getExtracts = async (refresh: boolean = false) => {
    if (!this.isLoading) {
      this.isLoading = true;
      try {
        if(refresh) this.extractRegistry.clear();
        const resp = await api.getExtracts();
        runInAction(() => {      
          resp.forEach((a: ExtractModel) => {
            this.extractRegistry.set(a.extractId, new ExtractEntity(a));
          });
        });
      }
      catch (err) {
        toast.error('Error loading Extracts...');
        console.error(err);
      }
      finally {
        this.isLoading = false;
      }
    }
  };
}

export const extractStore = new ExtractStore();

export const extractStoreContext = createContext<ExtractStore | null>(null);

export const useExtractStore = () => {
  const store = useContext(extractStoreContext);
  if (!store) {
    throw new Error('You forgot to use ExtractStore!');
  }
  return store;
};
