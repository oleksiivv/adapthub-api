using adapthub_api.Models;
using adapthub_api.Providers;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels.JobRequest;
using adapthub_api.ViewModels.Organization;
using adapthub_api.ViewModels.Vacancy;
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

            return organization;
        }

        public ListOrganizations List(FilterOrganizationViewModel filter, string sort,  string direction, int from, int to)
        {
            var organizations = _data.Organizations.Where(x => (x.Name == filter.Name || filter.Name == null));

            switch (sort.ToLower())
            {
                case "name":
                    organizations = direction.ToLower().Equals("asc") ? organizations.OrderBy(x => x.Name) : organizations.OrderByDescending(x => x.Name);
                    break;
                case "sitelink":
                    organizations = direction.ToLower().Equals("asc") ? organizations.OrderBy(x => x.SiteLink) : organizations.OrderByDescending(x => x.SiteLink);
                    break;
                default:
                    organizations = direction.ToLower().Equals("asc") ? organizations.OrderBy(x => x.Id) : organizations.OrderByDescending(x => x.Id);
                    break;
            }
            organizations = organizations.Skip(from).Take(to - from);

            var organizationViewModels = new List<Organization>();

            foreach (Organization organization in organizations)
            {
                organizationViewModels.Add(organization);
            }

            return new ListOrganizations
            {
                Data = organizationViewModels,
                TotalCount = _data.Organizations.Count(),
            };
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
                PasswordHash = SHA1Provider.Hash(data.Password),
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

            if (data.Password != null)
            {
                organization.PasswordHash = SHA1Provider.Hash(data.Password);
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

        public Organization? FindByEmail(string email)
        {
            return _data.Organizations.Where(x => x.Email.ToLower().Equals(email)).Count() > 0 ? _data.Organizations.Where(x => x.Email.ToLower().Equals(email)).First() : null;
        }

        public bool CheckPassword(string email, string password)
        {
            return _data.Organizations.Where(x => x.Email.ToLower().Equals(email)).First().PasswordHash.ToLower().Equals(SHA1Provider.Hash(password).ToLower());
        }
    }
}
