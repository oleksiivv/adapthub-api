namespace adapthub_api.Services
{
    public interface IVacancyProcessService
    {
        Task AskForVacancy(int vacancyId, int jobRequestId);

        Task ChooseJobRequestForVacancy(int vacancyId, int jobRequestId);
    }
}
