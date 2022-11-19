using adapthub_api.Models;
using adapthub_api.ViewModels.User;
using System.Xml.Linq;

namespace adapthub_api.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        public Customer Find(string id);

        public Customer Update(UpdateCustomerViewModel data);
    }
}
