using adapthub_api.Models;
using adapthub_api.ViewModels.User;
using System.Xml.Linq;

namespace adapthub_api.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        public CustomerViewModel Find(int id);

        public Customer FindWithoutRelations(int id);

        public CustomerViewModel Update(UpdateCustomerViewModel data);
    }
}
