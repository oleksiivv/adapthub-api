using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels.Organization;
using adapthub_api.ViewModels.User;
using SendGrid.Helpers.Errors.Model;

namespace adapthub_api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DataContext _data;
        public UserRepository(DataContext data)
        {
            _data = data;
        }
        public User Find(string id)
        {
            return _data.Users.Find(id);
        }

        public User Update(UpdateUserViewModel data)
        {
            var user = _data.Users.Find(data.Id);

            if (user == null)
            {
                throw new NotFoundException();
            }

            //TODO: refactor this logic
            if (data.Data != null)
            {
                user.Data = data.Data;
            }
            _data.Update(user);

            _data.SaveChanges();

            return user;
        }
    }
}
