interface UserModel {
    localOrganizationId: string;
    firstName: string;
    lastName: string;
    emailAddress: string;
    role: string;
    permissions: string[];
}

export default UserModel;
