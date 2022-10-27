
using Core.Interfaces;
using Core.Models;

namespace InfraStructure.Data
{
    public class DailyBoxRepository : GenericRepository<DailyBoxes>, IDailyBoxRepository
    {
        private readonly AppContext2 _context;
        public DailyBoxRepository(AppContext2 context) : base(context)
        {
            this._context = context;

        }
        public decimal GetSumTaxByDailyBoxId(int dailyBoxid)
        {

            return _context.Forms.Where(x => x.DailyBoxId == dailyBoxid).Sum(t => t.SumTax);
        }


        public decimal GetSumDevelopmentByDailyBoxId(int dailyBoxid)
        {

            return _context.Forms.Where(x => x.DailyBoxId == dailyBoxid).Sum(t => t.TaxDevelopment);
        }
    }
}