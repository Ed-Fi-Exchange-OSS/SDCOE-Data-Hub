interface AnnouncementModel {
    announcementId: number;
    localOrganizationID: string;
    message: string;
    displayUntilDate?: Date;
    status: number;
}

export default AnnouncementModel;
