interface ExtractModel {
  extractId: number;  
  localOrganizationID: string;
  organizationAbbreviation: string;
  extractJobName: string;
  extractFrequency: string;
  extractLastStatus: string;
  extractLastDate: Date;
}

export default ExtractModel;
