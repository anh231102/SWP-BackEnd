using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SWP_Final.Models;

namespace SWP_Final.Repositories
{
    public interface IAgencyRepositories
    {
        Task<List<AgencyModel>> GetAllAgenciesAsync();
        Task<AgencyModel> GetAgencyByIdAsync(string id);
        Task AddAgencyAsync(AgencyModel agency);
        Task UpdateAgencyAsync(string id, AgencyModel agency);
        Task DeleteAgencyAsync(string id);
        Task<string> UploadImageAsync(string agencyId, IFormFile file);
        Task<string> GetImageAsync(string agencyId);
        Task DeleteImageAsync(string agencyId);
    }
}
