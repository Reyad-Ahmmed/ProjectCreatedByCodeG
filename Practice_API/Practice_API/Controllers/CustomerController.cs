using Practice_Repository.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Practice_API.Controllers
{
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        private readonly CustomerRepo _customerRepo;
        public CustomerController()
        {
            _customerRepo = new CustomerRepo();
        }

        [HttpGet]
        [Route("GetCustomers")]
        public List<CustomerItem> GetCustomers()
        {
            var customers = _customerRepo.GetCustomers();
            return customers;
        }

        [HttpGet]
        [Route("GetCustomerById/{id}")]
        public CustomerItem GetCustomerById(int id)
        {
            var customer = _customerRepo.GetCustomerById(id);
            return customer;
        }

        [HttpPost]
        [Route("SaveOrUpdateCustomer")]

        public HttpResponseMessage SaveOrUpdateCustomer(CustomerItem customerItem)
        {
            try
            {
                var temp = _customerRepo.SaveCustomer(customerItem);
                return Request.CreateResponse(HttpStatusCode.OK, temp);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { message = "Something went wrong", error = ex.Message });
            }
        }

        [HttpDelete]
        [Route("DeleteCustomer/{id}")]
        public HttpResponseMessage DeleteCustomer(int id)
        {
            try
            {
                _customerRepo.DeleteCustomer(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { message = "Something went wrong", error = ex.Message });
            }
        }
    }
}
