using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUOW : IDisposable
    {

        IDailyRepository DailyRepository { get; }

        IFormRepository FormRepository { get; }
        IDailyBoxRepository DailyBoxRepository { get; }
        ICollageRepository CollageRepository { get; }
        IBoxRepository BoxRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}