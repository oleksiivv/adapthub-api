using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels;
using adapthub_api.ViewModels.Moderator;
using adapthub_api.ViewModels.Organization;
using adapthub_api.ViewModels.Vacancy;
using Microsoft.EntityFrameworkCore;
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
                FullName = data.FullName,
                PhoneNumber = data.PhoneNumber,
            };

            _data.Moderators.Add(moderator);
            _data.SaveChanges();

            return moderator;
        }

        public Moderator FindByEmail(string email)
        {
            return _data.Moderators.Where(x => x.Email.ToLower().Equals(email)).Count() > 0 ? _data.Moderators.Where(x => x.Email.ToLower().Equals(email)).First() : null;
        }

        public bool CheckPassword(string email, string password)
        {
            return _data.Moderators.Where(x => x.Email.ToLower().Equals(email)).First().PasswordHash == password; //TODO: hash
        }

        public void SeedDB()
        {
            _data.Customers.Add(
                    new Customer
                    {
                        Gender = GenderType.male,
                        UserName = "Петро Петренко",
                        Email = "ppetrenko@gmail.com",
                        EmailConfirmed = true,
                        PasswordHash = "32af2719dfacf9f199795043b3032177518d544c",
                        PassportNumber = "0098876541",
                        IDCode = "12457293",
                        CurrentAddress = "Львів, вул. Скрипника 18",
                        PhoneNumber = "+380985463746",
                        Experience = new CustomerExperience
                        {
                            PastJob = "Freelance",
                            Education = "НУ ЛП, спеціальність - ІПЗ, бакалавр, рік закінчення - 2021",
                            Experience = "Freelance - рік",
                            Profession = "Інженер ПЗ, напрямок - Data Science"
                        }
                    });
            _data.Customers.Add(
                    new Customer
                    {
                        Gender = GenderType.male,
                        UserName = "Микола Андрієнко",
                        Email = "mm12d@gmail.com",
                        EmailConfirmed = true,
                        PasswordHash = "249ae5af1f3a0742fff7e16728d8b962d28d1d01",
                        PassportNumber = "0085076541",
                        IDCode = "78657293",
                        CurrentAddress = "Львів, вул. Коперника 18",
                        PhoneNumber = "+380967834526",
                        Experience = new CustomerExperience
                        {
                            PastJob = "Касир у MCDonalds",
                            Education = "Повна середня",
                            Experience = "2 роки",
                            Profession = "Різноробочий"
                        }
                    });

            _data.Customers.Add(
                    new Customer
                    {
                        Gender = GenderType.female,
                        UserName = "Софія Тополя",
                        Email = "ss1s67@gmail.com",
                        EmailConfirmed = true,
                        PasswordHash = "14d05512336757d9e18c12b3bb4d1baba4a184b8",
                        PassportNumber = "0085076541",
                        IDCode = "78657293",
                        CurrentAddress = "Львів, вул. Коперника 18",
                        PhoneNumber = "+380967834526",
                        Experience = new CustomerExperience
                        {
                            PastJob = "Epam Systems",
                            Education = "НУ ЛП, спеціальність - КН, бакалавр, рік закінчення - 2019",
                            Experience = "Epam Systems - 1.5 року, Freelance - 20 місяців",
                            Profession = "Спец. у галузі КН, напрямок - BE"
                        }
                    }
            );

            _data.Moderators.Add(
                    new Moderator
                    {
                        FullName = "Василь Сало",
                        PasswordHash = "14d05512336757d9e18c12b3bb4d1baba4a184b8",
                        Email = "admin@gmail.com",
                        PhoneNumber = "+380987847587"
                    }
            );

            _data.Vacancies.Add(new Vacancy
            {
                Organization = new Organization
                {
                    Name = "АТБ",
                    SiteLink = "atb.com",
                    Description = "Це слова і букви були змінені додаванням або видаленням, так навмисно роблять його зміст безглуздо, це не є справжньою, правильно чи зрозумілою Латинської більше. У той час як Lorem Ipsum все ще нагадує класичну латину, він насправді не має ніякого значення.",
                    EDRPOU = "43700200",
                    Email = "atb_worker@gmail.com",
                    PasswordHash = "12975910c3e6352b5b2bdee81fa2fc4653a5bd59",
                },
                Status = StatusType.Confirmed,
                Speciality = "Працівник на склад",
                Salary = 10000,
                MinExperience = 12
            });

            _data.Vacancies.Add(
                    new Vacancy
                    {
                        Organization = new Organization
                        {
                            Name = "Рукавичка",
                            SiteLink = "sitetest.com",
                            Description = "Це слова і букви були змінені додаванням або видаленням, так навмисно роблять його зміст безглуздо, це не є справжньою, правильно чи зрозумілою Латинської більше. У той час як Lorem Ipsum все ще нагадує класичну латину, він насправді не має ніякого значення.",
                            EDRPOU = "43700200",
                            Email = "rykavuchka@gmail.com",
                            PasswordHash = "12975910c3e6352b5b2bdee81fa2fc4653a5bd59",
                        },
                        Status = StatusType.Confirmed,
                        Speciality = "Касир",
                        Salary = 10000,
                        MinExperience = 0
                    });

            _data.Vacancies.Add(
                    new Vacancy
                    {
                        Organization = new Organization
                        {
                            Name = "Спортлайф",
                            SiteLink = "sportlige.com",
                            Description = "Це слова і букви були змінені додаванням або видаленням, так навмисно роблять його зміст безглуздо, це не є справжньою, правильно чи зрозумілою Латинської більше. У той час як Lorem Ipsum все ще нагадує класичну латину, він насправді не має ніякого значення.",
                            EDRPOU = "43700200",
                            Email = "rykavuchka@gmail.com",
                            PasswordHash = "12975910c3e6352b5b2bdee81fa2fc4653a5bd59",
                        },
                        Status = StatusType.InReview,
                        Speciality = "Прибиральниця",
                        Salary = 7000,
                        MinExperience = 0
                    }
            );

            _data.JobRequests.Add(new JobRequest
            {
                Customer = new Customer
                {
                    Gender = GenderType.male,
                    UserName = "Іван Миколайчук",
                    Email = "johnm@gmail.com",
                    EmailConfirmed = true,
                    PasswordHash = "32af2719dfacf9f199795043b3032177518d544c",
                    PassportNumber = "0098876541",
                    IDCode = "12457293",
                    CurrentAddress = "Львів, вул. Скрипника 18",
                    PhoneNumber = "+380985463746",
                    Experience = new CustomerExperience
                    {
                        PastJob = "Працівник на складі",
                        Education = "Повна вища",
                        Experience = "Працівник на складі - рік",
                        Profession = "Різноробочий"
                    }
                },
                Status = StatusType.Confirmed,
                Speciality = "Працівник на складі",
                ExpectedSalary = 10000
            });

            _data.JobRequests.Add(
                    new JobRequest
                    {
                        Customer = new Customer
                        {
                            Gender = GenderType.male,
                            UserName = "Ірина Середа",
                            Email = "seredai@gmail.com",
                            EmailConfirmed = true,
                            PasswordHash = "32af2719dfacf9f199795043b3032177518d544c",
                            PassportNumber = "0098876541",
                            IDCode = "12457293",
                            CurrentAddress = "Львів, вул. Сихівська 56",
                            PhoneNumber = "+380985555746",
                            Experience = new CustomerExperience
                            {
                                PastJob = "Касир",
                                Education = "Повна вища",
                                Experience = "Касир - рік",
                                Profession = "Бугалтер"
                            }
                        },
                        Status = StatusType.Confirmed,
                        Speciality = "Касир",
                        ExpectedSalary = 7000
                    });

                    _data.JobRequests.Add(new JobRequest
                    {
                        Customer = new Customer
                        {
                            Gender = GenderType.male,
                            UserName = "Данило Дуб",
                            Email = "dubbd@gmail.com",
                            EmailConfirmed = true,
                            PasswordHash = "32af2719dfacf9f199795043b3032177518d544c",
                            PassportNumber = "0098876841",
                            IDCode = "12457293",
                            CurrentAddress = "Львів, вул. Зелена 44",
                            PhoneNumber = "+380985555746",
                            Experience = new CustomerExperience
                            {
                                PastJob = "Відсутня",
                                Education = "Повна вища",
                                Experience = "Відсутній",
                                Profession = "Кухар-кондитер"
                            }
                        },
                        Status = StatusType.Confirmed,
                        Speciality = "Працівник на кухню",
                        ExpectedSalary = 7000
                    }
            );

            _data.SaveChanges();
        }
    }
}
