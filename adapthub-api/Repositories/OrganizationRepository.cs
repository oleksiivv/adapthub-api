using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels.Organization;
using SendGrid.Helpers.Errors.Model;
using System.Net;

namespace adapthub_api.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private DataContext _data;
        public OrganizationRepository(DataContext data)
        {
            _data = data;
        }
        public Organization Find(int id)
        {
            var organization = _data.Organizations.Find(id);

            _data.Entry(organization).Reference("User").Load();

            return organization;
        }

        public IEnumerable<Organization> List(FilterOrganizationViewModel filter, string sort, int from, int to)
        {
            var organizations = _data.Organizations.Where(x => (x.Name == filter.Name || filter.Name == null)).Skip(from).Take(to - from);

            _data.Entry(organizations).Reference("User").Load();

            switch (sort)
            {
                case "Name":
                    organizations = organizations.OrderBy(x => x.Name);
                    break;
                case "SiteLink":
                    organizations = organizations.OrderBy(x => x.SiteLink);
                    break;
                default:
                    organizations = organizations.OrderBy(x => x.Id);
                    break;
            }
            return organizations;
        }

        public Organization Create(CreateOrganizationViewModel data)
        {
            var organization = new Organization
            {
                User = _data.Users.Find(data.UserId),
                Name = data.Name,
                SiteLink = data.SiteLink,
            };

            _data.Organizations.Add(organization);
            _data.SaveChanges();

            return organization;
        }

        public Organization Update(UpdateOrganizationViewModel data)
        {
            var organization = _data.Organizations.Find(data.Id);

            if (organization == null)
            {
                throw new NotFoundException();
            }

            //TODO: refactor this logic
            if (data.UserId != null)
            {
                organization.User = _data.Users.Find(data.UserId);
            }

            if (data.Name != null)
            {
                organization.Name = data.Name;
            }

            if (data.SiteLink != null)
            {
                organization.SiteLink = data.SiteLink;
            }
            _data.Update(organization);

            _data.SaveChanges();

            return organization;
        }

        public Organization Delete(int id)
        {
            var organization = _data.Organizations.Find(id);

            if (organization == null)
            {
                throw new NotFoundException();
            }

            _data.Organizations.Remove(organization);

            _data.SaveChanges();

            return organization;
        }
    }
}
