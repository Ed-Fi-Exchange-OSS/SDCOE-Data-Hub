import { action, computed, makeObservable, observable, runInAction } from 'mobx';
import { createContext, useContext } from 'react';
import { toast } from 'react-toastify';

import { api } from '../api';
import { CRMContactModel } from '../models';

export class CRMContactEntity {
  @observable crmContactId: number;
  @observable contactEmail: string;
  @observable contactName: string;
  @observable contactPhone: string;
  @observable contactTitle: string;
  @observable localOrganizationID: string;

  constructor(CRMContact: CRMContactModel) {
    this.crmContactId = CRMContact.crmContactId;
    this.contactEmail = CRMContact.contactEmail;
    this.contactName = CRMContact.contactName;
    this.contactPhone = CRMContact.contactPhone;
    this.contactTitle = CRMContact.contactTitle;
    this.localOrganizationID = CRMContact.localOrganizationID;
  }
}

export class CRMContactStore {
  
  constructor () {
    makeObservable(this);
  }

  @observable isLoading: boolean = false;

  crmContactRegistry = observable.map<number, CRMContactEntity>();

  @computed get crmContacts() {
    return Array.from(this.crmContactRegistry.values());
  }

  // async stuff
  @action
  getCRMContacts = async (refresh:boolean = false) => {
    if (!this.isLoading) {
      this.isLoading = true;
      try {
        if(refresh) this.crmContactRegistry.clear();
        const resp = await api.getCRMContacts();
        runInAction(() => {
          resp.forEach((a: CRMContactModel) => {
            this.crmContactRegistry.set(a.crmContactId, new CRMContactEntity(a));
          });
        });
      }
      catch (err) {
        toast.error('Error loading contacts...');
        console.error(err);
      }
      finally {
        this.isLoading = false;
      }
    }
    
  };
}

export const crmContactStore = new CRMContactStore();

export const crmContactStoreContext = createContext<CRMContactStore | null>(null);

export const useCRMContactStore = () => {
  const store = useContext(crmContactStoreContext);
  if (!store) {
    throw new Error('You forgot to use CRMContactStore!');
  }
  return store;
};
