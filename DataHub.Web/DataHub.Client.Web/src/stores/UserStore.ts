import { action, computed, makeObservable, observable, runInAction } from 'mobx';
import { createContext, useContext } from 'react';
import { toast } from 'react-toastify';

import { api } from '../api';
import UserModel from '../models/UserModel';
import { msalInstance } from '../utilities';

export class UserEntity {
    @observable localOrganizationID: string;
    @observable firstName: string;
    @observable lastName: string;
    @observable emailAddress: string;
    @observable role: string;
    @observable permissions: string[];

    constructor(user: UserModel) {
        this.localOrganizationID = user.localOrganizationId;
        this.firstName = user.firstName;
        this.lastName = user.lastName;
        this.emailAddress = user.emailAddress;
        this.role = user.role;
        this.permissions = user.permissions;
    }
}

export class UserStore {
    constructor() {
        makeObservable(this);
    }

    @observable me: UserEntity | null = null;
    @observable hasIssuedRequest: boolean = false;

    @computed get displayName() {
        if (this.me === null) return '(Unknown User)';
        return `${this.me.firstName} ${this.me.lastName}`;
    }

    @computed get userRole(){
      if (this.me === null) return '';
      return this.me.role;
    }
    
    // async stuff
    @action
    getMe = async (forceRefresh: boolean = false) => {
        try {
            if (!this.hasIssuedRequest || forceRefresh) {
                this.hasIssuedRequest = true;
                const resp = await api.getMe();
                runInAction(() => {
                    this.me = new UserEntity(resp);
                    let localOrgId = sessionStorage.getItem("LocalOrganizationID")||"";
                    if(localOrgId==="" && this.me?.localOrganizationID)
                      sessionStorage.setItem("LocalOrganizationID", JSON.stringify(`${this.me?.localOrganizationID}`));
                });
            }
        }
        catch (err) {
            if (msalInstance.getActiveAccount()) {
                toast.error('Error getting user details... Please try signing in again.');
                msalInstance.logout({
                    onRedirectNavigate: () => false
                });
            }
            console.error(err);
        }
    };

}

export const userStore = new UserStore();

export const userStoreContext = createContext<UserStore | null>(null);

export const useUserStore = () => {
    const store = useContext(userStoreContext);
    if (!store) {
        throw new Error('You forgot to use UserStore!');
    }
    return store;
};
