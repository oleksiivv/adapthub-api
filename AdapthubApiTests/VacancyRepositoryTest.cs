using adapthub_api;
using adapthub_api.Models;
using adapthub_api.Repositories;
using adapthub_api.ViewModels.Organization;
using adapthub_api.ViewModels.Vacancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Errors.Model;
using System.Data.Entity;
using System.Data.SqlClient;

namespace AdapthubApiTests
{
    public class VacancyRepositoryTest
    {
        private VacancyRepository _vacancyRepository;

        private DataContext _dataContext;

        [SetUp]
        public void Setup()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>().Options;
            
            _dataContext = new DataContext(dbContextOptions);

            _vacancyRepository = new VacancyRepository(_dataContext);

            Seed();
        }

        [Test]
        public void TestCreateWorksCorreclty()
        {
            var organization = _dataContext.Organizations.First();

            var vacancyViewModel = new CreateVacancyViewModel
            {
                Title = "Test vacancy",
                OrganizationId = organization.Id,
                Data = "",
            };

            var newVacancy = _vacancyRepository.Create(vacancyViewModel);

            Assert.NotNull(_dataContext.Vacancies.Find(newVacancy.Id));

            Assert.AreEqual(vacancyViewModel.Title, newVacancy.Title);
            Assert.AreEqual("DRAFT", newVacancy.Status);
            Assert.AreEqual(vacancyViewModel.OrganizationId, newVacancy.Organization.Id);
        }

        [Test]
        public void TestCreateThrowsException()
        {
            var exception = Assert.Throws<DbUpdateException>(
                delegate
                {
                    var vacancyViewModel = new CreateVacancyViewModel
                    {
                        Title = "Test vacancy",
                        OrganizationId = 3,
                    };

                    _vacancyRepository.Create(vacancyViewModel);
                }
            );

            Assert.NotNull(exception);
        }

        [Test]
        public void TestFindWorksCorrectly()
        {
            var vacancy = new Vacancy
            {
                Title = "Test vacancy",
                Organization = _dataContext.Organizations.First(),
                Data = "",
                Status = "DRAFT",
            };

            _dataContext.Vacancies.Add(vacancy);
            _dataContext.SaveChanges();

            var originalVacancy = _dataContext.Vacancies.First();

            var foundVacancy = _vacancyRepository.Find(originalVacancy.Id);

            Assert.NotNull(foundVacancy);
            Assert.AreEqual(originalVacancy.Id, foundVacancy.Id);
            Assert.AreEqual(originalVacancy.Title, foundVacancy.Title);
            Assert.AreEqual(originalVacancy.Status, foundVacancy.Status);
            Assert.AreEqual(originalVacancy.Organization, foundVacancy.Organization);
        }

        [Test]
        public void TestFindReturnsNullWhenModelNotFound()
        {
            var foundVacancy = _vacancyRepository.Find(1);

            Assert.IsNull(foundVacancy);
        }

        [Test]
        public void TestUpdateWorksCorreclty()
        {
            var vacancy = new Vacancy
            {
                Title = "Test vacancy",
                Organization = _dataContext.Organizations.First(),
                Data = "",
                Status = "DRAFT",
            };

            _dataContext.Vacancies.Add(vacancy);
            _dataContext.SaveChanges();

            vacancy = _dataContext.Vacancies.Where(x => x.Title == vacancy.Title).First();

            var updateVacancyViewModel = new UpdateVacancyViewModel
            {
                Id = vacancy.Id,
                Status = "APPROVED",
            };

            var updatedVacancy = _vacancyRepository.Update(updateVacancyViewModel);

            Assert.NotNull(updatedVacancy);
            Assert.AreEqual(updatedVacancy.Id, vacancy.Id);
            Assert.AreEqual(updatedVacancy.Status, updateVacancyViewModel.Status);
        }

        [Test]
        public void TestUpdateThrowsExceptionWhenModelNotFound()
        {
            var exception = Assert.Throws<NotFoundException>(
                delegate
                {
                    var updateVacancyViewModel = new UpdateVacancyViewModel
                    {
                        Id = 345,
                        Status = "APPROVED",
                    };

                    _vacancyRepository.Update(updateVacancyViewModel);
                }
            );

            Assert.NotNull(exception);
        }

        [Test]
        public void TestDeleteWorksCorreclty()
        {
            var vacancy = new Vacancy
            {
                Title = "Test vacancy",
                Organization = _dataContext.Organizations.First(),
                Data = "",
                Status = "DRAFT",
            };

            _dataContext.Vacancies.Add(vacancy);
            _dataContext.SaveChanges();

            vacancy = _dataContext.Vacancies.First();

            _vacancyRepository.Delete(vacancy.Id);

            var updatedVacancy = _dataContext.Vacancies.Find(vacancy.Id);

            Assert.IsNull(updatedVacancy);
        }

        [Test]
        public void TestDeleteThrowsExceptionWhenModelNotFound()
        {
            var exception = Assert.Throws<NotFoundException>(
                delegate
                {
                    _vacancyRepository.Delete(1990);
                }
            );

            Assert.NotNull(exception);
        }

        [Test]
        public void TestFilterAndPaginateWorksCorreclty()
        {
            var vacancies = SeedVacancies();

            var filter = new FilterVacancyViewModel
            {
                Status = "APPROVED"
            };

            int to = 4;
            int from = 1;

            string sort = "Title";

            var list = _vacancyRepository.List(filter, sort, from, to);

            Assert.AreEqual(list.Count(), 2);

            foreach (var vacancy in list)
            {
                Assert.AreEqual(vacancy.Status, filter.Status);
            }
        }

        [Test]
        public void TestSortWorksCorreclty()
        {
            var vacancies = SeedVacancies();

            var filter = new FilterVacancyViewModel
            {
                Status = "APPROVED"
            };

            int to = 4;
            int from = 1;

            string sort = "Title";

            var list = _vacancyRepository.List(filter, sort, from, to);

            var sortedCollection = vacancies.Where(x => x.Status == filter.Status).Skip(from).Take(to - from).OrderBy(x => x.Title);

            Assert.AreEqual(sortedCollection, list);
        }

        [Test]
        public void TestListThrowsExceptionIfPaginationIsWrong()
        {
            var exception = Assert.Throws<Microsoft.Data.SqlClient.SqlException>(
                delegate
                {
                    var filter = new FilterVacancyViewModel
                    {
                        Status = "APPROVED"
                    };

                    int to = 1;
                    int from = 1;

                    string sort = "Title";

                    var list = _vacancyRepository.List(filter, sort, from, to);

                    Assert.AreEqual(list.Count(), to - from);

                    foreach (var vacancy in list)
                    {
                        Assert.AreEqual(vacancy.Status, filter.Status);
                    }
                }
            );
        }

        private void Seed()
        {
            RefreshDB();

            var organization = new Organization
            {
                Name = "Test Name",
                SiteLink = "youtube.com",
                User = new User(),
            };

            _dataContext.Organizations.Add(organization);
            _dataContext.SaveChanges();
        }

        private void RefreshDB()
        {
            foreach (var entity in _dataContext.Vacancies)
                _dataContext.Vacancies.Remove(entity);

            foreach (var entity in _dataContext.Organizations)
                _dataContext.Organizations.Remove(entity);

            _dataContext.SaveChanges();
        }

        private IEnumerable<Vacancy> SeedVacancies()
        {
            var vacancies = new Vacancy[]
            {
                new Vacancy
                {
                    Title = "Devops",
                    Organization = _dataContext.Organizations.First(),
                    Data = "",
                    Status = "DRAFT",
                },
                new Vacancy
                {
                    Title = "QA",
                    Organization = _dataContext.Organizations.First(),
                    Data = "",
                    Status = "DRAFT",
                },
                new Vacancy
                {
                    Title = "Backend dev",
                    Organization = _dataContext.Organizations.First(),
                    Data = "",
                    Status = "APPROVED",
                },
                new Vacancy
                {
                    Title = "Frontend dev",
                    Organization = _dataContext.Organizations.First(),
                    Data = "",
                    Status = "APPROVED",
                },
                new Vacancy
                {
                    Title = "Designer",
                    Organization = _dataContext.Organizations.First(),
                    Data = "",
                    Status = "APPROVED",
                },
            };

            _dataContext.Vacancies.AddRange(vacancies);
            _dataContext.SaveChanges();

            return _dataContext.Vacancies;
        }
    }
}