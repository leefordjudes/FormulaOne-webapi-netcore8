using FormulaOne.Entities.DbSet;
namespace FormulaOne.DataService.Repositories.Interfaces;

public interface IAchievementRepository : IGenericRepository<Achievement>
{
    Task<Achievement?> GetDriverFirstAchievementAsync(Guid driverId);
    Task<IEnumerable<Achievement>> GetDriverAchievementsAsync(Guid driverId);
}