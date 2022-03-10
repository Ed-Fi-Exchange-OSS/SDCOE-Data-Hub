import { action, computed, makeObservable, observable, runInAction } from 'mobx';
import { createContext, useContext } from 'react';
import { toast } from 'react-toastify';

import { api } from '../api';
import { AnnouncementModel } from '../models';

export class AnnouncementEntity {
  @observable announcementId: number;
  @observable localOrganizationID: string;
  @observable message: string;
  @observable displayUntilDate?: Date;
  @observable status: number;
  constructor(announcement: AnnouncementModel) {
    this.announcementId = announcement.announcementId;
    this.localOrganizationID = announcement.localOrganizationID;
    this.message = announcement.message;
    this.displayUntilDate = announcement.displayUntilDate;
    this.status = announcement.status;
  }
}

export class AnnouncementStore {
  
  constructor () {
    makeObservable(this);
  }

  @observable isLoading: boolean = false;

  announcementRegistry = observable.map<number, AnnouncementEntity>();

  @computed get announcements() {
    return Array.from(this.announcementRegistry.values());
  }


  // async stuff
  @action
  getAnnouncements = async (refresh:boolean = false) => {
    if (!this.isLoading) {
      this.isLoading = true;
      try {
        if(refresh) this.announcementRegistry.clear();
        const resp: AnnouncementModel[] = await api.getAnnouncements();
        runInAction(() => {      
          resp.forEach((a: AnnouncementModel) => {           
            this.announcementRegistry.set(a.announcementId, new AnnouncementEntity(a));
          });
        })
      }
      catch (err) {
        toast.error('Error loading announcements...');
        console.error(err);
      }
      finally {
        this.isLoading = false;
      }
    }
  };

  @action
  hideAnnouncement = async (id: number) => {    
    const announcement = this.announcementRegistry.get(id);
    runInAction(() => {
      if(announcement)        
        this.announcementRegistry.delete(announcement.announcementId);
    });
  };
}

export const announcementStore = new AnnouncementStore();

export const announcementStoreContext = createContext<AnnouncementStore | null>(null);

export const useAnnouncementStore = () => {
  const store = useContext(announcementStoreContext);
  if (!store) {
    throw new Error('You forgot to use AnnouncementStore!');
  }
  return store;
};
