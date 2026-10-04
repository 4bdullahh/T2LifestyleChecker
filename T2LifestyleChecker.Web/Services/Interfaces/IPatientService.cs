using T2LifestyleChecker.Web.Models;

namespace T2LifestyleChecker.Web.Services.Interfaces
{
    public interface IPatientService
    {
        Task<Patient?> GetPatientAsync(string NHSNumber);
    }
}
