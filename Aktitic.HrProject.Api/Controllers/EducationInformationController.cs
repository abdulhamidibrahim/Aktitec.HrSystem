using Microsoft.AspNetCore.Mvc;
using Aktitic.HrProject.BL.Dtos.EducationInformation;
using Aktitic.HrTaskList.BL;

namespace Aktitic.HrProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationInformationController(IEducationInformationManager educationInformationManager)
        : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Add([FromBody] EducationInformationAddDto educationInformationAddDto)
        {
            var result = await educationInformationManager.Add(educationInformationAddDto);
            if (result > 0)
                return Ok(result);
            return BadRequest("Failed to add education information.");
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EducationInformationAddDto educationInformationDto)
        {
            var result = await educationInformationManager.Update(educationInformationDto, id);
            if (result > 0)
                return Ok(result);
            return BadRequest("Failed to update education information.");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await educationInformationManager.Delete(id);
            if (result > 0)
                return Ok(result);
            return BadRequest("Failed to delete education information.");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAll(int userId)
        {
            var result = await educationInformationManager.GetAll(userId);
            if (result != null)
                return Ok(result);
            return NotFound("No education information found.");
        }
    }
}