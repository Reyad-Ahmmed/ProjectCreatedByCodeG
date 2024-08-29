export class CustomerModel {
    
    CustomerId:number = 0;
    CustomerName?:string;
    Phone?:string;
    Address?:string;
    Deleted?:string;
    CreatedById?:number;
    CreatedDate?:Date;
    UpdatedById?:number;
    UpdatedDate?:Date;
    
    constructor (){}
}
