using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels.Moderator;
using adapthub_api.ViewModels.Organization;
using adapthub_api.ViewModels.Vacancy;
using SendGrid.Helpers.Errors.Model;

namespace adapthub_api.Repositories
{
    public class ModeratorRepository : IModeratorRepository
    {
        private DataContext _data;
        public ModeratorRepository(DataContext data)
        {
            _data = data; 
        }

        public Moderator Find(int id)
        {
            var moderator = _data.Moderators.Find(id);

            return moderator;
        }

        public Moderator Create(CreateModeratorViewModel data)
        {
            var moderator = new Moderator
            {
                Email = data.Email,
                PasswordHash = data.Password, //TODO: hash
            };

            _data.Moderators.Add(moderator);
            _data.SaveChanges();

            return moderator;
        }
    }
}
