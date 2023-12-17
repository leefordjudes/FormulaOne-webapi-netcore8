using FormulaOne.DataService.Data;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FormulaOne.DataService.Repositories;

public class AchievementRepository(AppDbContext context, ILogger logger)
    : GenericRepository<Achievement>(context, logger), IAchievementRepository
{
    public async Task<Achievement?> GetDriverFirstAchievementAsync(Guid driverId)
    {
        try
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.DriverId == driverId);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} GetDriverAchievementAsync function error", typeof(AchievementRepository));
            throw;
        }
    }
    
    public async Task<IEnumerable<Achievement>> GetDriverAchievementsAsync(Guid driverId)
    {
        try
        {
            return await _dbSet.Where(x => x.DriverId == driverId)
                .AsNoTracking()
                .AsSplitQuery()
                .OrderBy(x => x.AddedDate)
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} Get Driver Achievements function error", typeof(AchievementRepository));
            throw;
        }
    }
    
    public override async Task<IEnumerable<Achievement>> All()
    {
        try
        {
            return await _dbSet.Where(x => x.Status == 1)
                .AsNoTracking()
                .AsSplitQuery()
                .OrderBy(x => x.AddedDate)
                .ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} All function error", typeof(AchievementRepository));
            throw;
        }
    }
    
    public override async Task<bool> Delete(Guid id)
    {
        try
        {
            // get my entity
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
                return false;

            result.Status = 0;
            result.UpdatedDate = DateTime.Now;

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} Delete function error", typeof(AchievementRepository));
            throw;
        }
    }
    
    public override async Task<bool> Update(Achievement achievement)
    {
        try
        {
            // get my entity
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == achievement.Id);

            if (result == null)
                return false;

            result.FastestLap = achievement.FastestLap;
            result.PolePosition = achievement.PolePosition;
            result.RaceWins = achievement.RaceWins;
            result.WorldChampionship = achievement.WorldChampionship;
            
            result.UpdatedDate = DateTime.Now;

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Repo} Update function error", typeof(AchievementRepository));
            throw;
        }
    }
}