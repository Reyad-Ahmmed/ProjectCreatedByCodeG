using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Repository.Repo
{
    public class CustomerRepo
    {
        private readonly CustomerItem customerItem;
        private readonly CustomerList customerList;

        public CustomerRepo()
        {
            customerItem = new CustomerItem();
            customerList = new CustomerList();
        }

        public List<CustomerItem> GetCustomers()
        {
            List<CustomerItem> customers = new List<CustomerItem>();
            customerList.Load(CustomerList.LoadOption.LoadAll);
            foreach (CustomerItem item in customerList)
            {
                customers.Add(item);
            }

            return customers;
        }

        public CustomerItem GetCustomerById(int customerId)
        {
            customerItem.CustomerId = customerId;
            customerItem.Load(CustomerItem.LoadOption.LoadById);
            return customerItem;
        }

        public CustomerItem SaveCustomer(CustomerItem customerItem)
        {
            customerItem.Save(CustomerItem.SaveOption.SaveRow);
            return customerItem;
        }

        public void DeleteCustomer(int customerId)
        {
            customerItem.CustomerId = customerId;
            customerItem.Save(CustomerItem.SaveOption.DeleteRow);
        }
    }
}
