using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels;
using adapthub_api.ViewModels.User;
using SendGrid.Helpers.Errors.Model;
using System.Net;

namespace adapthub_api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private DataContext _data;
        public CustomerRepository(DataContext data)
        {
            _data = data;
        }
        public Customer Find(string id)
        {
            var customer = _data.Customers.Find(id);
            _data.Entry(customer).Reference("Experience").Load();

            return customer;
        }

        public Customer FindWithoutRelations(string id)
        {
            var customer = _data.Customers.Find(id);

            return customer;
        }

        public Customer Update(UpdateCustomerViewModel data)
        {
            var user = _data.Customers.Find(data.Id);

            if (user == null)
            {
                throw new NotFoundException();
            }

            if (data.CurrentAddress != null)
            {
                user.CurrentAddress = data.CurrentAddress;
            }
            if(data.Gender != null)
            {
                GenderType gender;
                Enum.TryParse(data.Gender, out gender);

                user.Gender = gender;
            }
            if(data.PhoneNumber != null)
            {
                user.PhoneNumber = data.PhoneNumber;
            }
            if (data.IDCode != null)
            {
                user.IDCode = data.IDCode;
            }
            if (data.Experience != null)
            {
                user = UpdateUserExperience(user.Id, data.Experience);
            }

            _data.Update(user);

            _data.SaveChanges();

            return user;
        }

        public Customer UpdateUserExperience(string customerId, CustomerExperienceViewModel expereience)
        {
            var customer = _data.Customers.Find(customerId);

            if (expereience.Education != null)
            {
                customer.Experience.Education = expereience.Education;
            }
            if (expereience.PastJob != null)
            {
                customer.Experience.PastJob = expereience.PastJob;
            }
            if (expereience.Profession != null)
            {
                customer.Experience.Profession = expereience.Profession;
            }
            if (expereience.Experience != null)
            {
                customer.Experience.Experience = expereience.Experience;
            }

            _data.Update(customer);

            _data.SaveChanges();

            return customer;
        }
    }
}
