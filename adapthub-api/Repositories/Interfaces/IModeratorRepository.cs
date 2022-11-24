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

        public Moderator Update(UpdateModeratorViewModel data);

        public Moderator? FindByEmail(string email);

        public bool CheckPassword(string email, string password);

        public void SeedDB();
    }
}
