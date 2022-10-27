
using Core.Interfaces;
using Core.Models;

namespace InfraStructure.Data
{
    public class BoxRepository : GenericRepository<Box>, IBoxRepository
    {
        public BoxRepository(AppContext2 context) : base(context)
        {
        }
    }
}