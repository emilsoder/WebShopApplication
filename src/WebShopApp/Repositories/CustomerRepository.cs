using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShopApp.Models;

namespace WebShopApp.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private ApplicationDbContext _context;
        public DbSet<Customers> customers { get; set; }

        public CustomerRepository(ApplicationDbContext context)
        {
            this._context = context;
            this.customers = _context.Set<Customers>();
        }

        public async Task<IEnumerable<Customers>> GetCustomers()
        {
            return await customers.ToListAsync();
        }

        public async Task<Customers> GetCustomerByID(int customerID)
        {
            return await customers.SingleOrDefaultAsync(x =>
                x.CustomerID == customerID);
        }

        public async void InsertCustomer(Customers customer)
        {
            try
            {
                var result = await customers.AddAsync(customer);
            }
            catch (Exception)
            {
                return;
            }
        }

        public async void DeleteCustomer(int customerID)
        {
            Customers customer = await customers.SingleOrDefaultAsync(x => x.CustomerID == customerID);

            customers.Remove(customer);
            _context.SaveChanges();
        }

        public void UpdateCustomer(Customers customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void EditCustomerDetails(Customers customer)
        {
            try
            {
                customers.Update(customer);
                SaveAsync();
            }
            catch (Exception)
            {
                return;
            }
        }

        public int GetCustomerID(string userName)
        {
            var result = customers.FirstOrDefault(x => x.Email == userName);
            return result.CustomerID;
        }

        public bool CustomerExists(int customerId)
        {
            if (customers.Any(x => x.CustomerID == customerId))
            {
                return true;
            }
            return false;
        }

        public async void SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<string> UserRole(string user)
        {
            var _user = await GetCustomerByID(GetCustomerID(user));
            return _user.Role;
        } 
        public Task<string> GetUsersFirstName()
        {
            throw new NotImplementedException();
        }
    }
}
