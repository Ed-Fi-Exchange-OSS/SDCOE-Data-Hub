interface EdFiRequestModel {
  edFiRequestId: number;
  description: string;
  requestDate: Date;
  requestStatus: number;
  isArchived: boolean;
}

export default EdFiRequestModel;
