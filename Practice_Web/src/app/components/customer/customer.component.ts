import { Component, OnInit, SimpleChange } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PageChangeEvent } from '@progress/kendo-angular-grid';
import { subscribeOn } from 'rxjs';
import { CustomerModel } from 'src/app/models/customer-model';
import { CustomerService } from 'src/app/services/customer.service';
declare var $: any;

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {

  constructor(
    private _customerService:CustomerService,
    private fb: FormBuilder
  ) 
  { }

  customerList:CustomerModel[]=[];
  customer:CustomerModel = new CustomerModel();
  deletedCustomerId:number = 0;
  customerForm!: FormGroup;

  public pageSize = 10;
  public skip = 0;
  
  public pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.getCustomers();
  }
  // show save modal when save new customer
  AddCustomerClick(){
    this.formInitialize();
    $('#saveOrUpdateModal').modal('show');
  }

  // save or update form submission
  SaveOrUpdateClick(){
    if(this.customerForm.value.CustomerId === 0){
      this.customer.CustomerId = -1,
      this.customer.CustomerName = this.customerForm.value.CustomerName,
      this.customer.Phone = this.customerForm.value.Phone,
      this.customer.Address = this.customerForm.value.Address,
      //this.customer.CreatedDate = new Date(this.getCurrentDateTime())
  
      console.log(this.customer);
  
      this._customerService.SaveOrUpdateCustomer(this.customer).subscribe(data=>{
        console.log(data);
  
        this.SaveOrUpdateCustomerCancelModal();
        this.getCustomers();
        this.blankFieldForCustomerForm();

      });
    }
    else{
      this.customer.CustomerId = this.customerForm.value.CustomerId,
      this.customer.CustomerName = this.customerForm.value.CustomerName,
      this.customer.Phone = this.customerForm.value.Phone,
      this.customer.Address = this.customerForm.value.Address,
      //this.customer.CreatedDate = new Date(this.getCurrentDateTime())
  
      console.log(this.customer);
  
      this._customerService.SaveOrUpdateCustomer(this.customer).subscribe(data=>{
        console.log(data);
  
        this.SaveOrUpdateCustomerCancelModal();
        this.getCustomers();
        this.blankFieldForCustomerForm();

      });
    }
  }

  // get current date time
  getCurrentDateTime(): string {
    const now = new Date();
    return now.toLocaleString('en-US', { timeZone: 'Asia/Dhaka' });
  }

  onEditClick(id:number){
    this._customerService.getCustomerById(id).subscribe(data=>{
      this.customer.CustomerId = id;
      this.customer.CustomerName = data.CustomerName;
      this.customer.Phone = data.Phone;
      this.customer.Address = data.Address;

      this.customerForm.patchValue(this.customer);
      $('#saveOrUpdateModal').modal('show');
    });
  }
  // show modal when click grid delete button
  onDeleteClick(id:number){
    this.deletedCustomerId = id;
    $('#confirmModal').modal('show');
  }

  // get all customers
  getCustomers(){
    this._customerService.getCustomers().subscribe(data=>{
      this.customerList = data;
    })
  }

  // delete confirm modal close
  ConfirmCancelModal(){
    $('#confirmModal').modal('hide');
  }

  SaveOrUpdateCustomerCancelModal(){
    $('#saveOrUpdateModal').modal('hide');
  }

  // delete pop up yes button click
  ConfirmYesClick(id:number){
    if(id > 0){
      this._customerService.DeleteCustomer(id).subscribe(r=>{
        console.log(r);
        this.ConfirmCancelModal();
        this.getCustomers();
      });
    }
    else{
      alert("Something went wrong.");
    }
  }

  // form initialize
  formInitialize(){
    this.customerForm = this.fb.group({
      CustomerId: [0],  // Hidden field
      CustomerName: ['', Validators.required],
      Phone: ['', [Validators.required, Validators.pattern('^\\+?[0-9]{10,15}$')]],
      Address: ['', Validators.required]
    });
  }

  // form fields reset
  blankFieldForCustomerForm(){
    this.customerForm.reset();
  }

  ngOnInit(): void {
    // Initialization logic here
    this.getCustomers();
    this.formInitialize();
    console.log('CustomerComponent initialized');
  }
}
