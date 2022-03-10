interface OrganizationModel {
  organizationId: number;
  organizationName: string;
  localOrganizationID: string;
  sis: string;
  dominantDataSystem: string;
  analyticsSystem: string;
  interimAssessments: string;
}

export default OrganizationModel;
