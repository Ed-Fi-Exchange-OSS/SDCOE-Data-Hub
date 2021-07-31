import { createContext, useContext } from 'react';
import { action, computed, makeObservable, observable, runInAction } from 'mobx';

import { api } from '../api';
import { OfferingModel } from '../models';
import { toast } from 'react-toastify';

const itemCategoryTypeLookup = new Map<number, string>([
  [1, "SDCOE service or program"],
  [2, "3rd party product"]
]);

export class OfferingEntity {
  @observable offeringId?: number;
  @observable itemNo: number;
  @observable itemCategoryType: number;
  @observable itemCategory: string;
  @observable itemName: string;
  @observable itemDescription: string;
  @observable itemType: number;
  @observable associatedCost: string;
  @observable productUrl: string;
  @observable contactName: string;
  @observable contactPhone: string;
  @observable contactEmail: string;

  constructor(offering: OfferingModel) {
    this.offeringId = offering.offeringId;
    this.itemNo = offering.itemNo;
    this.itemCategoryType = offering.itemCategoryType;
    this.itemCategory = itemCategoryTypeLookup.get(offering.itemCategoryType) || "Unknown";
    this.itemName = offering.itemName;
    this.itemDescription = offering.itemDescription;
    this.itemType = offering.itemType;
    this.associatedCost = offering.associatedCost;
    this.productUrl = offering.productUrl;
    this.contactName = offering.contactName;
    this.contactPhone = offering.contactPhone;
    this.contactEmail = offering.contactEmail;
  }
}

export class OfferingStore {
  
  constructor () {
    makeObservable(this);
  }

  @observable isParticipatingLoading: boolean = false;
  @observable isAvailableLoading: boolean = false;

  offeringRegistry = observable.map<number, OfferingEntity>();

  availableServicesRegistry = observable.map<number, OfferingEntity>();

  @computed get offerings() {
    return Array.from(this.offeringRegistry.values()).sort((a, b) => a.itemNo - b.itemNo);
  }

  @computed get availableServices() {
    return Array.from(this.availableServicesRegistry.values()).sort((a, b) => a.itemNo - b.itemNo);
  }

  // async stuff
  @action
  getParticipatingOfferings = async (refresh:boolean = false) => {
    if (!this.isParticipatingLoading) {
      this.isParticipatingLoading = true;
      try {
        if(refresh) this.offeringRegistry.clear();
        const resp = await api.getParticipatingOfferings();
        runInAction(() => {
          resp.forEach((a: OfferingModel, index: number) => {
            this.offeringRegistry.set(a.itemNo, new OfferingEntity(a));
          });
        });
      }
      catch (err) {
        toast.error('Error loading participating offerings...');
        console.error(err);
      }
      finally {
        this.isParticipatingLoading = false;
      }
    }
  };

  @action
  getAvailableOfferings = async (refresh:boolean = false) => {
    if (!this.isAvailableLoading) {
      this.isAvailableLoading = true;
      try {
        if(refresh) this.availableServicesRegistry.clear();
        const resp = await api.getAvailableOfferings();
        runInAction(() => {
          resp.forEach((a: OfferingModel, index: number) => {
            this.availableServicesRegistry.set(a.itemNo, new OfferingEntity(a));
          });
        });
      }
      catch (err) {
        toast.error('Error loading available offerings...');
        console.error(err);
      }
      finally {
        this.isAvailableLoading = false;
      }
    }
  };

  addParticipation = async (itemNo: number) => {
    try {
      const offering = this.availableServicesRegistry.get(itemNo);
      if (!offering) return
  
      await api.addParticipation(itemNo);
      runInAction(() => {
        this.offeringRegistry.set(itemNo, offering);
        this.availableServicesRegistry.delete(itemNo);
      });
    }
    catch (err) {
      toast.error('Error adding participation in offering...');
      console.error(err);
    }
  };

  removeParticipation = async (itemNo: number) => {
    try {
      const offering = this.offeringRegistry.get(itemNo);
      if (!offering) return
      
      await api.removeParticipation(itemNo);
      runInAction(() => {
        this.availableServicesRegistry.set(itemNo, offering);
        this.offeringRegistry.delete(itemNo);
      });
    }
    catch (err) {
      toast.error('Error removing participation in offering...');
      console.error(err);
    }
  }
}

export const offeringStore = new OfferingStore();

export const offeringStoreContext = createContext<OfferingStore | null>(null);

export const useOfferingStore = () => {
  const store = useContext(offeringStoreContext);
  if (!store) {
    throw new Error('You forgot to use OfferingStore!');
  }
  return store;
};
