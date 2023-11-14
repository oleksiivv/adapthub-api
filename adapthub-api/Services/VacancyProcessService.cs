using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels.JobRequest;
using adapthub_api.ViewModels.Vacancy;
using adapthub_api.ViewModels;

namespace adapthub_api.Services
{
    public class VacancyProcessService : IVacancyProcessService
    {
        private readonly IMailService _mailService;
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IJobRequestRepository _jobRequestRepository;
        private readonly IConfiguration _configuration;

        public VacancyProcessService(IMailService mailService, IVacancyRepository vacancyRepository, IJobRequestRepository jobRequestRepository, IConfiguration configuration)
        {
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _vacancyRepository = vacancyRepository ?? throw new ArgumentNullException(nameof(vacancyRepository));
            _jobRequestRepository = jobRequestRepository ?? throw new ArgumentNullException(nameof(jobRequestRepository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task AskForJobRequest(int vacancyId, int jobRequestId)
        {
            var vacancy = _vacancyRepository.Find(vacancyId);
            var jobRequest = _jobRequestRepository.Find(jobRequestId);

            var emailContent = $"Ви отримали пропозицію на вакансію за спеціальністю {vacancy.Speciality} від {vacancy.Organization.Name}" +
                $"<a href='{_configuration["FrontendUrl"]}/vacancies?id={vacancyId}'>Переглянути вакансію</a>";

            await _mailService.SendEmailAsync(jobRequest.Customer.Email, "Пропозиція з роботи", emailContent);
        }

        public async Task AskForVacancy(int vacancyId, int jobRequestId)
        {
            var vacancy = _vacancyRepository.Find(vacancyId);
            var jobRequest = _jobRequestRepository.Find(jobRequestId);

            var url = $"{_configuration["FrontendUrl"]}/accept-request?vacancyId={vacancyId}&requestId={jobRequestId}";

            var emailContent = $"<h1>Заявка на вакансію {vacancy.Speciality} </h1>" +
                $"<a href='{url}'>Переглянути заявку</a>";

            await _mailService.SendEmailAsync(vacancy.Organization.Email, "Заява від кандидата на вашу вакансію", emailContent);
        }

        public async Task CancelJobRequestForVacancy(int vacancyId, int jobRequestId)
        {
            var vacancy = _vacancyRepository.Find(vacancyId);
            var jobRequest = _jobRequestRepository.Find(jobRequestId);

            var emailContent = $"<h1>Вашу заявку на вакансію за спеціальністю {vacancy.Speciality} від {vacancy.Organization.Name} було відхилено. Продовжуйте пошук.</h1>";

            await _mailService.SendEmailAsync(jobRequest.Customer.Email, "Вашу заявку відхилено", emailContent);
        }

        public async Task ChooseJobRequestForVacancy(int vacancyId, int jobRequestId)
        {
            var vacancy = _vacancyRepository.Update(new UpdateVacancyViewModel
            {
                Id = vacancyId,
                Status = StatusType.Past.ToString(),
                ChosenJobRequest = jobRequestId,
            });

            var jobRequest = _jobRequestRepository.Update(new UpdateJobRequestViewModel
            {
                Id = jobRequestId,
                Status = StatusType.Past.ToString(),
            });

            var emailContent = $"<h1>Вашу заявку на вакансію за спеціальністю {vacancy.Speciality} від {vacancy.Organization.Name} було схваленою.</h1>" +
                $"<a href='{_configuration["FrontendUrl"]}/vacancies?id={vacancyId}'>Переглянути вакансію</a>";

            await _mailService.SendEmailAsync(jobRequest.Customer.Email, "Нова вакансія для вас", emailContent);
        }
    }
}
