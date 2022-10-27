
using Core.Interfaces;
using Core.Models;

namespace InfraStructure.Data
{
    public class FormsRepository : GenericRepository<Form>, IFormRepository
    {
        public FormsRepository(AppContext2 context) : base(context)
        {
        }
    }
}