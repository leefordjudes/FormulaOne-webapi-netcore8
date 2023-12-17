using FormulaOne.DataService.Data;
using FormulaOne.DataService.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace FormulaOne.DataService.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;
    
    public IDriverRepository Drivers { get; }
    public IAchievementRepository Achievements { get; }

    public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        var logger = loggerFactory.CreateLogger("logs");

        Drivers = new DriverRepository(_context, logger);
        Achievements = new AchievementRepository(_context, logger);
    }

    public async Task<bool> CompleteAsync()
    {
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }


    public void Dispose()
    {
        // Dispose(true);
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
    //ref: https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca1816
    // protected virtual void Dispose(bool disposing)
    // {
    //     if (disposing)
    //     {
    //        _context.Dispose();
    //     }
    // }
}