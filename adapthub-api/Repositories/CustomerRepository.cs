using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels;
using adapthub_api.ViewModels.JobRequest;
using adapthub_api.ViewModels.User;
using SendGrid.Helpers.Errors.Model;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using XSystem.Security.Cryptography;

namespace adapthub_api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private DataContext _data;
        public CustomerRepository(DataContext data)
        {
            _data = data;
        }
        public CustomerViewModel Find(int id)
        {
            var customer = _data.Customers.Find(id);
            _data.Entry(customer).Reference("Experience").Load();

            return PrepareResponse(customer);
        }

        public Customer FindWithoutRelations(int id)
        {
            var customer = _data.Customers.Find(id);

            return customer;
        }

        public CustomerViewModel Update(UpdateCustomerViewModel data)
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
            if(data.HelpOption != null)
            {
                HelpOption helpOption;
                switch (data.HelpOption.ToLower())
                {
                    case "соціальна допомога":
                        helpOption = HelpOption.SocialHelp;
                        break;
                    case "юридично-правова допомога":
                        helpOption = HelpOption.JuridicalHelp;
                        break;
                    case "медична допомога":
                        helpOption = HelpOption.MedicalHelp;
                        break;
                    default:
                        helpOption = HelpOption.SearchForJob;
                        break;
                }

                user.HelpOption = helpOption;
            }

            _data.Update(user);

            _data.SaveChanges();

            return PrepareResponse(user);
        }

        public Customer UpdateUserExperience(int customerId, CustomerExperienceViewModel expereience)
        {
            var customer = _data.Customers.Find(customerId);

            if(customer.Experience == null)
            {
                customer.Experience = new CustomerExperience();
            }
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

        private CustomerViewModel PrepareResponse(Customer customer)
        {
            string helpOption = null;

            switch (customer.HelpOption)
            {
                case HelpOption.SocialHelp:
                    helpOption = "Соціальна допомога";
                    break;
                case HelpOption.JuridicalHelp:
                    helpOption = "Юридично-Правова допомога";
                    break;
                case HelpOption.MedicalHelp:
                    helpOption = "Медична допомога";
                    break;
                case HelpOption.SearchForJob:
                    helpOption = "Пошук роботи";
                    break;
            }
            return new CustomerViewModel
            {
                Id = customer.Id,
                Gender = customer.Gender.ToString(),
                UserName = customer.UserName,
                NormalizedUserName = customer.NormalizedUserName,
                Email = customer.Email,
                NormalizedEmail = customer.NormalizedEmail,
                EmailConfirmed = customer.EmailConfirmed,
                PasswordHash = customer.PasswordHash,
                PassportNumber = customer.PassportNumber,
                IDCode = customer.IDCode,
                CurrentAddress = customer.CurrentAddress,
                PhoneNumber = customer.PhoneNumber,
                HelpOption = helpOption,
            };
        }
    }
}
