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
                Name = data.Name,
                SiteLink = data.SiteLink,
                Description = data.Description,
                EDRPOU = data.EDRPOU,
                Email = data.Email,
                PasswordHash = data.PasswordHash, //TODO: hash
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
            if (data.Name != null)
            {
                organization.Name = data.Name;
            }

            if (data.SiteLink != null)
            {
                organization.SiteLink = data.SiteLink;
            }

            if (data.Description != null)
            {
                organization.Description = data.Description;
            }

            if (data.EDRPOU != null)
            {
                organization.EDRPOU = data.EDRPOU;
            }

            if (data.Email != null)
            {
                organization.Email = data.Email;
            }

            if (data.PasswordHash != null)
            {
                organization.PasswordHash = data.PasswordHash;
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
