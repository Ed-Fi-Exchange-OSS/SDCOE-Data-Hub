import { action, computed, makeObservable, observable, runInAction } from 'mobx';
import { createContext, useContext } from 'react';
import { toast } from 'react-toastify';

import { api } from '../api';
import { EdFiRequestModel } from '../models';

export class EdFiRequestEntity {
  @observable edFiRequestId: number;
  @observable description: string;
  @observable requestDate: Date | string;
  @observable requestStatus: number;
  @observable isArchived: boolean;
  constructor(edFiRequest: EdFiRequestModel) {
    this.edFiRequestId = edFiRequest.edFiRequestId;
    this.description = edFiRequest.description;
    this.requestDate = edFiRequest.requestDate;
    this.requestStatus = edFiRequest.requestStatus;
    this.isArchived = edFiRequest.isArchived;
  }
}
export class EdFiRequestStore {
  
  constructor () {
    makeObservable(this);
  }

  @observable isRequestsLoading: boolean = false;
  @observable isRequestTypesLoading: boolean = false;
  @observable hasRequestTypesBeenInitialized: boolean = false;
  
  edfiRequestRegistry = observable.map<number, EdFiRequestEntity>();
  edfiRequestTypeRegistry = observable.map<number, any>();

  @computed get edfiRequests() {
    return Array.from(this.edfiRequestRegistry.values());
  }

  @computed get edfiRequestTypes() {
    return Array.from(this.edfiRequestTypeRegistry.values());    
  }

  // async stuff
  @action
  getEdFiRequests = async (refresh:boolean = false) => {
    if (!this.isRequestsLoading) {
      this.isRequestsLoading = true;
      try {
        if(refresh) this.edfiRequestRegistry.clear();
        const resp = await api.getEdFiRequests();
        runInAction(() => {      
          resp.forEach((a: EdFiRequestModel) => {
            this.edfiRequestRegistry.set(a.edFiRequestId, new EdFiRequestEntity(a));
          });
        });
      }
      catch (err) {
        toast.error('Error loading Ed-Fi Self Service Requests...');
        console.error(err);
      }
      finally {
        this.isRequestsLoading = false;
      }
    }
  };

  @action
  getEdFiRequestTypes = async () => {
    if (!this.isRequestTypesLoading && !this.hasRequestTypesBeenInitialized) {
      this.isRequestTypesLoading = true;
      try {
        const resp = await api.getEdFiRequestTypes();
        runInAction(() => {      
          resp.forEach((a: any) => {
            this.edfiRequestTypeRegistry.set(a.id, a);
          });
        });
      }
      catch (err) {
        toast.error('Error loading Ed-Fi Self-Service request types...');
        console.error(err);
      }
      finally {
        this.isRequestTypesLoading = false;
        this.hasRequestTypesBeenInitialized = true;
      }
    }
  };

  @action
  addEdFiRequest = async(req: EdFiRequestModel) => {
    try {
      const resp = await api.addEdFiRequest(req);
      runInAction(()=>{
        this.edfiRequestRegistry.set(resp.edFiRequestId, new EdFiRequestEntity(resp)); 
      })
    }
    catch (err) {
      toast.error('Error adding Ed-Fi Self-Service request...');
      console.error(err);
    }
  }

  @action
  updateEdFiRequest = async(req: EdFiRequestEntity) => {
    try {
      const resp = await api.updateEdFiRequest(req.edFiRequestId, (req as EdFiRequestModel));
      runInAction(()=>{
        this.edfiRequestRegistry.set(resp.edFiRequestId, new EdFiRequestEntity(resp)); 
      })
    }
    catch (err) {
      toast.error('Error updating Ed-Fi Self-Service request...');
      console.error(err);
    }
  }

  @action
  archiveEdFiRequest = async (req: EdFiRequestEntity) => {
    try {
      const resp = await api.updateEdFiRequest(req.edFiRequestId, { ...req, isArchived: true } as EdFiRequestModel);
      runInAction(() => {
        this.edfiRequestRegistry.delete(req.edFiRequestId);
      })
    }
    catch (err) {
      toast.error('Error archiving Ed-Fi Self-Service request...');
    }
  }
}

export const edFiRequestStore = new EdFiRequestStore();

export const edFiRequestStoreContext = createContext<EdFiRequestStore | null>(null);

export const useEdFiRequestStore = () => {
  const store = useContext(edFiRequestStoreContext);
  if (!store) {
    throw new Error('You forgot to use EdFiRequestStore!');
  }
  return store;
};
