using adapthub_api.Models;
using adapthub_api.ViewModels.User;
using System.Xml.Linq;

namespace adapthub_api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public User Find(string id);

        public User Update(UpdateUserViewModel data);
    }
}
