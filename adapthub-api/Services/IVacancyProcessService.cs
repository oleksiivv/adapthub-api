namespace adapthub_api.Services
{
    public interface IVacancyProcessService
    {
        Task AskForVacancy(int vacancyId, int jobRequestId);

        Task AskForJobRequest(int vacancyId, int jobRequestId);

        Task ChooseJobRequestForVacancy(int vacancyId, int jobRequestId);

        Task CancelJobRequestForVacancy(int vacancyId, int jobRequestId);
    }
}
