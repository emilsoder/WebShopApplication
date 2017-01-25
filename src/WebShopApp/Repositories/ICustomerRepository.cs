using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopApp.Models;

namespace WebShopApp.Repositories
{
    public interface ICustomerRepository 
    {
        Task<IEnumerable<Customers>> GetCustomers();
        Task<Customers> GetCustomerByID(int customerID);
        void InsertCustomer(Customers customer);
        void DeleteCustomer(int customerID);
        void UpdateCustomer(Customers customer);

        Task<string> GetUsersFirstName();
        int GetCustomerID(string userName);
        void EditCustomerDetails(Customers customer);
        bool CustomerExists(int customerId);
        Task<string> UserRole(string user);
        void SaveAsync();
    }
}
