
using System.Linq;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Data
{
    public class DailyRepository : GenericRepository<Daily>, IDailyRepository
    {
        private readonly AppContext2 _context;

        public DailyRepository(AppContext2 context) : base(context)
        {
            this._context = context;
        }



        public async Task<List<Form>> GetDailyBoexByDailyId(int dailyId)
        {
            var dailyBoxes = this._context.DailyBoxes.Include(x => x.Forms).Where(t => t.DailyId == dailyId);



            var result = await dailyBoxes.SelectMany(t => t.Forms).ToListAsync();
            // foreach (var dailyBox in dailyBoxes)
            // {

            //     if (dailyBox.Forms != null)
            //         result += dailyBox.Forms.Sum(t => t.SumTax);
            // }
            return result;
        }
        // public decimal GetSumDevelopmentByDailyId(int dailyId)
        // {
        //     var dailyBoxes = this._context.DailyBoxes.Include(x => x.Forms).Where(t => t.DailyId == dailyId);

        //     decimal? result = 0;
        //     foreach (var dailyBox in dailyBoxes)
        //     {

        //         if (dailyBox.Forms != null)
        //             result += dailyBox.Forms.Sum(t => t.TaxDevelopment);
        //     }
        //     return result ?? 0;
        // }
    }
}