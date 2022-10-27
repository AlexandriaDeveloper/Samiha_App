
using Core.Interfaces;
using Core.Models;

namespace InfraStructure.Data
{
    public class CollageRepository : GenericRepository<Collage>, ICollageRepository
    {
        public CollageRepository(AppContext2 context) : base(context)
        {
        }
    }
}