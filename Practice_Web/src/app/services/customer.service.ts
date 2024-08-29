import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CustomerModel } from '../models/customer-model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  _baseUrl:string = "http://localhost:12514/api/Customer/";
  constructor(
    private _httpClient : HttpClient
  ) 
  { }

  getCustomers():Observable<CustomerModel[]>{
    return this._httpClient.get<CustomerModel[]>(`${this._baseUrl}GetCustomers`);
  }

  getCustomerById(id:number):Observable<CustomerModel>{
    return this._httpClient.get<CustomerModel>(`${this._baseUrl}GetCustomerById/${id}`);
  }

  DeleteCustomer(id:number):Observable<CustomerModel>{
    return this._httpClient.delete<CustomerModel>(`${this._baseUrl}DeleteCustomer/${id}`);
  }

  SaveOrUpdateCustomer(customer:CustomerModel):Observable<CustomerModel>{
    return this._httpClient.post<CustomerModel>(`${this._baseUrl}SaveOrUpdateCustomer`,customer);
  }
}
