using Aktitic.HrProject.BL.Dtos.EducationInformation;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyInformationController(IFamilyInformationManager familyInformationManager) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Add( FamilyInformationAddDto familyInformationAddDto)
        {
            var result = await familyInformationManager.Add(familyInformationAddDto);
            if (result > 0)
                return Ok(result);
            return BadRequest("Failed to add family information.");
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FamilyInformationAddDto familyInformationDto)
        {
            var result = await familyInformationManager.Update(familyInformationDto, id);
            if (result > 0)
                return Ok(result);
            return BadRequest("Failed to update family information.");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await familyInformationManager.Delete(id);
            if (result > 0)
                return Ok(result);
            return BadRequest("Failed to delete family information.");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAll(int userId)
        {
            var result = await familyInformationManager.GetAll(userId);
            if (result != null)
                return Ok(result);
            return NotFound("No family information found.");
        }
    }
}