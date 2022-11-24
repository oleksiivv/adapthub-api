using adapthub_api.Models;
using adapthub_api.Repositories.Interfaces;
using adapthub_api.ViewModels;
using adapthub_api.ViewModels.JobRequest;
using adapthub_api.ViewModels.Vacancy;
using Microsoft.AspNetCore.Identity;
using System.Configuration;
using System.Security.Policy;

namespace adapthub_api.Services
{
    public class VacancyProcessService : IVacancyProcessService
    {
        private readonly IMailService _mailService;
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IJobRequestRepository _jobRequestRepository;
        private IConfiguration _configuration;

        public VacancyProcessService(IMailService mailService, IVacancyRepository vacancyRepository, IJobRequestRepository jobRequestRepository, IConfiguration configuration)
        {
            _configuration = configuration;
            _mailService = mailService;
            _vacancyRepository = vacancyRepository;
            _jobRequestRepository = jobRequestRepository;
        }

        public async Task AskForJobRequest(int vacancyId, int jobRequestId)
        {
            var vacancy = _vacancyRepository.Find(vacancyId);
            var jobRequest = _jobRequestRepository.Find(jobRequestId);

            await _mailService.SendEmailAsync(jobRequest.Customer.Email, "Пропозиція з роботи", $"Ви отримали пропозицію на вакансію за спеціальністю {vacancy.Speciality} від {vacancy.Organization.Name}" +
                    $"<a href='http://localhost:3000/vacancies?id={vacancyId}'>Переглянути вакансію</a>");
        }

        public async Task AskForVacancy(int vacancyId, int jobRequestId)
        {
            var vacancy = _vacancyRepository.Find(vacancyId);
            var jobRequest = _jobRequestRepository.Find(jobRequestId);

            string url = $"http://localhost:3000/accept-request?vacancyId={vacancyId}&requestId={jobRequestId}";


            await _mailService.SendEmailAsync(vacancy.Organization.Email, "Заява від кандидата на вашу вакансію", $"<h1>Заявка на вакансію {vacancy.Speciality} </h1>" +
                    $"<a href='{url}'>Переглянути заявку</a>");
        }

        public async Task CancelJobRequestForVacancy(int vacancyId, int jobRequestId)
        {
            var vacancy = _vacancyRepository.Find(vacancyId);

            var jobRequest = _jobRequestRepository.Find(jobRequestId);

            await _mailService.SendEmailAsync(jobRequest.Customer.Email, "Вашу заявку відхилено", $"<h1>Вашу заявку на вакансію за спеціальністю {vacancy.Speciality} від {vacancy.Organization} було відхилено. Продовжуйте пошук.</h1>");
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

            await _mailService.SendEmailAsync(jobRequest.Customer.Email, "Нова вакансія для вас", $"<h1>Вашу заявку на вакансію за спеціальністю {vacancy.Speciality} від {vacancy.Organization} було схваленою.</h1>" +
                    $"<a href='http://localhost:3000/vacancies?id={vacancyId}'>Переглянути вакансію</a>");
        }
    }
}
