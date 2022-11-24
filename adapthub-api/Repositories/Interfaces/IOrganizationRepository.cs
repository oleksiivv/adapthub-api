﻿using adapthub_api.Models;
using adapthub_api.ViewModels.Organization;
using SendGrid.Helpers.Errors.Model;
using System.Xml.Linq;

namespace adapthub_api.Repositories.Interfaces
{
    public interface IOrganizationRepository
    {
        public Organization Find(int id);
        public IEnumerable<Organization> List(FilterOrganizationViewModel filter, string sort, int from, int to);

        public Organization Create(CreateOrganizationViewModel data);

        public Organization Update(UpdateOrganizationViewModel data);

        public Organization Delete(int id);

        public Organization? FindByEmail(string email);

        public bool CheckPassword(string email, string password);
    }
}
