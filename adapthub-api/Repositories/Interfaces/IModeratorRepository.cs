using adapthub_api.Models;
using adapthub_api.ViewModels.Moderator;
using adapthub_api.ViewModels.User;
using System.Xml.Linq;

namespace adapthub_api.Repositories.Interfaces
{
    public interface IModeratorRepository
    {
        public Moderator Find(int id);

        public Moderator Create(CreateModeratorViewModel data);
    }
}
