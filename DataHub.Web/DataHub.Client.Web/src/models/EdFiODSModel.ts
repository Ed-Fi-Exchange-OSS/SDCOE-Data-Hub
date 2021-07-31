interface EdFiODSModel {
  extractId: number;  
  localOrganizationID: string;
  orgInternalAbbreviation: string;
  extractJobName: string;
  extractFrequency: string;
  extractLastStatus: string;
  extractLastDate: Date;
}

export default EdFiODSModel;
