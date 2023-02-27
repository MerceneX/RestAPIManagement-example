using RestAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestAPI.Repository
{
    public interface IAccidentsRepository
    {
        Task DeleteAccident(string id);
        Task<List<Accident>> GetAccident();
        Task<Accident> GetAccidentByID(string id);
        Task InsertAccident(Accident newAccident);
        Task UpdateAccident(string id, Accident updateAccident);
    }
}