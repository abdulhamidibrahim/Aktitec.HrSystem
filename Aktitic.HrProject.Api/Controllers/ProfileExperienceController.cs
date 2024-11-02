using Aktitic.HrProject.BL.Dtos.ProfileExperience;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileExperienceController(IProfileExperienceManager profileExperienceManager) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Add([FromBody] ProfileExperienceAddDto profileExperienceAddDto)
        {
            var result = await profileExperienceManager.Add(profileExperienceAddDto);
            if (result > 0)
                return Ok(result);
            return BadRequest("Failed to add profile experience.");
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromBody] ProfileExperienceUpdateDto profileExperienceUpdateDto,int id)
        {
            var result = await profileExperienceManager.Update(profileExperienceUpdateDto, id);
            if (result > 0)
                return Ok(result);
            return BadRequest("Failed to update profile experience.");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await profileExperienceManager.Delete(id);
            if (result > 0)
                return Ok(result);
            return BadRequest("Failed to delete profile experience.");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var result = await profileExperienceManager.GetByUserId(userId);
            if (result != null)
                return Ok(result);
            return NotFound("No profile experience found.");
        }
    }
}