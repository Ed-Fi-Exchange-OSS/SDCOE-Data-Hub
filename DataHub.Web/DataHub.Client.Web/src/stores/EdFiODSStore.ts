import { action, computed, makeObservable, observable, runInAction } from 'mobx';
import { createContext, useContext } from 'react';
import { toast } from 'react-toastify';

import { api } from '../api';
import { EdFiODSStatusModel, EdFiODSResourceCountModel, EdFiODSClientModel } from '../models';

export class EdFiODSStatusEntity {
  @observable edFiODSNo: number;
  @observable odsName: string;
  @observable odsVersion: string;
  @observable odsUrl: string;
  @observable odsPath: string;
  @observable status: string;
  @observable lastCheckedDate: string;
  @observable edFiOdsClients: EdFiODSClientModel[];
  @observable resourceCounts: EdFiODSResourceCountModel[];

  constructor(edfiODSStatus: EdFiODSStatusModel){
    this.edFiODSNo=edfiODSStatus.edFiOdsNo;
    this.odsName=edfiODSStatus.odsName;
    this.odsVersion=edfiODSStatus.odsVersion;
    this.odsUrl=edfiODSStatus.odsUrl;
    this.odsPath=edfiODSStatus.odsPath;
    this.status=edfiODSStatus.status;
    this.lastCheckedDate=edfiODSStatus.lastCheckedDate;
    this.edFiOdsClients=edfiODSStatus.edFiOdsClients.map((c) => new EdFiODSClientEntity(c));
    this.resourceCounts=edfiODSStatus.resourceCounts.map((rc) => new EdFiODSResourceCountEntity(rc));
  }
}

export class EdFiODSClientEntity {
  @observable applicationName: string;
  @observable claimSetName: string;
  @observable clientName: string;
  @observable odsKey: string;
  @observable odsSecret: string;
  @observable vendorName: string;

  constructor(edfiODSClient: EdFiODSClientModel){
    this.applicationName=edfiODSClient.applicationName;
    this.claimSetName=edfiODSClient.claimSetName;
    this.clientName=edfiODSClient.clientName;
    this.odsKey=edfiODSClient.odsKey;
    this.odsSecret=edfiODSClient.odsSecret;
    this.vendorName=edfiODSClient.vendorName;
  }
}

export class EdFiODSResourceCountEntity {
  @observable resourceName: string;
  @observable resourceCount: number;
  @observable lastCheckedDate: string;

  constructor(edfiODSResourceCount: EdFiODSResourceCountModel){
    this.resourceName=edfiODSResourceCount.resourceName;
    this.resourceCount=edfiODSResourceCount.resourceCount;
    this.lastCheckedDate=edfiODSResourceCount.lastCheckedDate;
  }
}

export class EdFiODSStore {
  constructor () {
    makeObservable(this);
  }

  @observable isLoading: boolean = false;

  edfiODSStatusRegistry = observable.map<number, EdFiODSStatusEntity>();

  @computed get edfiODSStatuses() {
    return Array.from(this.edfiODSStatusRegistry.values());
  }

  // async stuff
  @action
  getEdFiODSStatuses = async (refresh:boolean = false) => {
    if (!this.isLoading) {
      this.isLoading = true;
      try {
        if (refresh) this.edfiODSStatusRegistry.clear();
        const resp = await api.getEdFiODSStatuses();
        runInAction(() => {      
          resp.forEach((a: EdFiODSStatusModel) => {
            this.edfiODSStatusRegistry.set(a.edFiOdsNo, new EdFiODSStatusEntity(a));
          });
        });
      }
      catch (err) {
        toast.error('Error loading Ed-Fi ODS statuses...');
        console.error(err);
      }
      finally {
        this.isLoading = false;
      }
    }
  };
}

export const edFiODSStore = new EdFiODSStore();

export const edFiODSStoreContext = createContext<EdFiODSStore | null>(null);

export const useEdFiODSStore = () => {
  const store = useContext(edFiODSStoreContext);
  if (!store) {
    throw new Error('You forgot to use EdFiODSStore!');
  }
  return store;
};
