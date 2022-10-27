using System.Data;
using Core.Interfaces;
using InfraStructure;
using InfraStructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class UOW : IUOW
{
    private readonly AppContext2 _context;

    private IDailyBoxRepository _dailyBoxRepository;
    private IDailyRepository _dailyRepository;
    private IFormRepository _formRepository;
    private ICollageRepository _collageRepository;
    private readonly ILogger<UOW> _logger;

    private IBoxRepository _boxRepository;
    public UOW(AppContext2 context, ILogger<UOW> logger)
    {
        this._logger = logger;
        this._context = context;
    }
    public IDailyBoxRepository DailyBoxRepository => _dailyBoxRepository = _dailyBoxRepository ?? new DailyBoxRepository(_context);
    public IDailyRepository DailyRepository => _dailyRepository = _dailyRepository ?? new DailyRepository(_context);
    public IFormRepository FormRepository => _formRepository = _formRepository ?? new FormsRepository(_context);
    public ICollageRepository CollageRepository => _collageRepository = _collageRepository ?? new CollageRepository(_context);
    public IBoxRepository BoxRepository => _boxRepository = _boxRepository ?? new BoxRepository(_context);
    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<bool> SaveChangesAsync()
    {
        var result = string.Empty;
        try
        {
            var resultIndex = await _context.SaveChangesAsync();


            return true;

        }
        catch (DbUpdateException ex)
        {
            SqlException innerException = ex.InnerException.InnerException as SqlException;
            if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
            {
                // Swallow the exception, as the only thing we're updating is the `LastWrite`, which will be the current date/time so there's not point in doing additional queries;
            }
            else
            {
                throw;
            }
            return false;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);

            return false;
        }


    }
}