using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.BL.Managers;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogsController(ILogsManager logsManager,DatabaseSizeService databaseSizeService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<LogsReadDto>>> GetAll()
    {
        var logs = await logsManager.GetAll();
        return Ok(logs);
    }
    
    [HttpGet("{id}")]
    public ActionResult<LogsReadDto?> Get(int id)
    {
        var logs = logsManager.Get(id);
        if (logs == null) return NotFound("Logs not found");
        return Ok(logs);
    }
    
   
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    { 
        var result =logsManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
    // database size 
    [HttpGet("databaseSize")]
    public Task<string> DatabaseSize()
    {
        return databaseSizeService.GetDatabaseSizeAsync();
    } 
    
    // nonactive data size 
    [HttpGet("trash")]
    public async Task<object> NonActiveDataSize()
    {
        var totalSizeInByte = await databaseSizeService.GetDatabaseSizeAsync();
        var activeSizeInByte = await databaseSizeService.GetActiveDataSizeAsync();
        var activeData = (double)activeSizeInByte / (1024 * 1024);
        var nonActiveSizeInByte = await databaseSizeService.GetNonActiveDataSizeAsync();
        var tempData  = (double)nonActiveSizeInByte / (1024 * 1024);
        return new {totalSizeInByte =totalSizeInByte,activeData =activeData,tempData =tempData};
    }
    
    // active data size 
   
    // delete non active data
    [HttpDelete("deleteNonActiveData")]
    public void DeleteNonActiveData()
    {
        databaseSizeService.DeleteNonActiveData();
    }    
    
    // get department temp data
    // [HttpGet("getDepartmentTempData")]
    // public Task<long> GetDepartmentTempData()
    // {
    //     return databaseSizeService.GetQueryDataSizeAsync();
    // }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<LogsDto>> GlobalSearch(string search,string? column)
    {
        return await logsManager.GlobalSearch(search,column);
    }
    
    [HttpGet("getFilteredLogs")]
    public Task<FilteredLogsDto> GetFilteredLogsAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return logsManager.GetFilteredLogsAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
}