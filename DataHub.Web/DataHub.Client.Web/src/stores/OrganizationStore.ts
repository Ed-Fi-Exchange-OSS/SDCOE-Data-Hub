import { action, computed, makeObservable, observable, runInAction } from 'mobx';
import { createContext, useContext } from 'react';
import { toast } from 'react-toastify';

import { api } from '../api';
import { OrganizationModel } from '../models';

export class OrganizationEntity {
  organizationId: number;
  organizationName: string;
  localOrganizationID: string;
  sis: string;
  domainantDataSystem: string;
  analyticsSystem: string;
  interimAssessments: string;
  constructor(organization: OrganizationModel) {
    this.organizationId = organization.organizationId;
    this.organizationName = organization.organizationName;
    this.localOrganizationID = organization.localOrganizationID;
    this.sis = organization.sis;
    this.domainantDataSystem = organization.dominantDataSystem;
    this.analyticsSystem = organization.analyticsSystem;
    this.interimAssessments = organization.interimAssessments;
  }
}

export class OrganizationStore {

  constructor () {
    makeObservable(this);
  }

  @observable isLoading: boolean = false;

  organizationRegistry = observable.map<string, OrganizationEntity>();

  @observable currentOrganization: OrganizationEntity | null = null;

  @computed get organizations() {
    return Array.from(this.organizationRegistry.values());
  }

  @action
  getCurrentOrganization = async () => {
    try {
      const resp: OrganizationModel = await api.getOrganization();
      runInAction(() => {
        this.currentOrganization = new OrganizationEntity(resp);
      });
    }
    catch (err) {
      toast.error('Error loading current Organization...');
      console.error(err);
    }
  }

  @action
  getOrganizations = async (all: boolean = false) => {
    if (!this.isLoading) {
      this.isLoading = true;
      try {
        const resp: OrganizationModel[] = (all) ? await api.getAllOrganizations() : [await api.getOrganization()];
        runInAction(() => {
          resp.forEach((a: OrganizationModel) => {
            this.organizationRegistry.set(a.localOrganizationID, new OrganizationEntity(a));
          });
        });
      }
      catch (err) {
        toast.error('Error loading Organizations...');
        console.error(err);
      }
      finally {
        this.isLoading = false;
      }
    }
  };
}

export const organizationStore = new OrganizationStore();

export const organizationStoreContext = createContext<OrganizationStore | null>(null);

export const useOrganizationStore = () => {
  const store = useContext(organizationStoreContext);
  if (!store) {
    throw new Error('You forgot to use OrganizationStore!');
  }
  return store;
};
