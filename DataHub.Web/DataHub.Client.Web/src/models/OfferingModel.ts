interface OfferingModel {
  offeringId: number;
  itemNo: number;
  itemCategoryType: number;
  itemCategory: string;
  itemName: string; 
  itemDescription: string; 
  itemType: number; 
  associatedCost: string; 
  productUrl: string; 
  contactName: string; 
  contactPhone: string; 
  contactEmail: string; 
}

export default OfferingModel;
