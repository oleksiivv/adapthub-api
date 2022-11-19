using adapthub_api.Repositories.Interfaces;
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

        public async Task AskForVacancy(int vacancyId, int jobRequestId)
        {
            var vacancy = _vacancyRepository.Find(vacancyId);
            var jobRequest = _jobRequestRepository.Find(jobRequestId);

            await _mailService.SendEmailAsync(vacancy.Organization.Email, "Заява від кандидата на вашу вакансію", $"<h1>Заявка на вакансію {vacancy.Speciality}-{vacancyId} </h1>" +
                    $"<a href='#'>Переглянути заявку</a>");
        }

        public async Task ChooseJobRequestForVacancy(int vacancyId, int jobRequestId)
        {
            var vacancy = _vacancyRepository.Find(vacancyId);
            var jobRequest = _jobRequestRepository.Find(jobRequestId);

            await _mailService.SendEmailAsync(jobRequest.Customer.Email, "Нова вакансія для вас", $"<h1>Вашу заявку на вакансію за спеціальністю {vacancy.Speciality} від {vacancy.Organization} було схваленою.</h1>" +
                    $"<a href='#'>Переглянути вакансію</a>");
        }
    }
}
