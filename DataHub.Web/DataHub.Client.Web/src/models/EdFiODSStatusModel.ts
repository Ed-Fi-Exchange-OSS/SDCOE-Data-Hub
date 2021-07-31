import EdFiODSClientModel from './EdFiODSClientModel';
import { default as EdFiODSResourceCountModel } from './EdFiODSResourceCountModel';

interface EdFiODSStatusModel{
  edFiOdsNo: number;
  odsName: string;
  odsVersion: string;
  odsUrl: string;
  odsPath: string;
  status: string;
  lastCheckedDate: string;
  edFiOdsClients: EdFiODSClientModel[];
  resourceCounts: EdFiODSResourceCountModel[];
}

export default EdFiODSStatusModel;