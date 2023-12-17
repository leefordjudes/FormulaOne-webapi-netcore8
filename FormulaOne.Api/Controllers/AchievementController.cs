using AutoMapper;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using FormulaOne.Entities.Dtos.Requests;
using FormulaOne.Entities.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOne.Api.Controllers;

public class AchievementController(IUnitOfWork unitOfWork, IMapper mapper) : BaseController(unitOfWork, mapper)
{
    [HttpGet]
    [Route("{driverId:guid}")]
    public async Task<IActionResult> GetDriverAchievement(Guid driverId)
    {
        var driverAchievements = await _unitOfWork.Achievements.GetDriverFirstAchievementAsync(driverId);
        if (driverAchievements == null)
            return NotFound("Achievements not found");

        var result = _mapper.Map<DriverAchievementResponse>(driverAchievements);

        return Ok(result);
    }
    
    [HttpGet]
    [Route("{driverId:guid}/all")]
    public async Task<IActionResult> GetDriverAchievements(Guid driverId)
    {
        var driverAchievements = await _unitOfWork.Achievements.GetDriverAchievementsAsync(driverId);

        var result = _mapper.Map<IEnumerable<DriverAchievementResponse>>(driverAchievements);

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<IActionResult> AddAchievement([FromBody] CreateDriverAchievementRequest achievement)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = _mapper.Map<Achievement>(achievement);
        
        await _unitOfWork.Achievements.Add(result);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(GetDriverAchievements), new { driverId = result.DriverId }, result);
    }
    
    [HttpPut("")]
    public async Task<IActionResult> UpdateAchievement([FromBody] UpdateDriverAchievementRequest achievement)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var result = _mapper.Map<Achievement>(achievement);
        
        await _unitOfWork.Achievements.Update(result);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}