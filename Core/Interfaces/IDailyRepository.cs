
using Core.Models;

namespace Core.Interfaces
{
    public interface IDailyRepository : IGenericRepository<Daily>
    {
        Task<List<Form>> GetDailyBoexByDailyId(int dailyId);
        // decimal GetSumDevelopmentByDailyId(int dailyId);
    }
}