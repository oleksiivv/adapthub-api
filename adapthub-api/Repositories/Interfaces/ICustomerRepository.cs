using adapthub_api.Models;
using adapthub_api.ViewModels.User;
using System.Xml.Linq;

namespace adapthub_api.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        public Customer Find(int id);

        public Customer FindWithoutRelations(int id);

        public Customer Update(UpdateCustomerViewModel data);
    }
}
