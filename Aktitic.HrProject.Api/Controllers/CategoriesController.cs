using System.Runtime.InteropServices;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrTaskList.BL;
using Microsoft.AspNetCore.Mvc;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController: ControllerBase
{
    private readonly ICategoryManager _categoryManager;

    public CategoriesController(ICategoryManager categoryManager)
    {
        _categoryManager = categoryManager;
    }
    
    [HttpGet]
    public Task<List<CategoryReadDto>> GetAll()
    {
        return _categoryManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public ActionResult<CategoryReadDto?> Get(int id)
    {
        var user = _categoryManager.Get(id);
        if (user == null) return NotFound();
        return Ok(user);
    }
    
    [HttpPost("create")]
    public ActionResult<Task> Add(CategoryAddDto categoryAddDto)
    {
        var result = _categoryManager.Add(categoryAddDto);
        if (result.Result == 0) return BadRequest("Failed to create");
        return Ok("Added Successfully");
    }
    
    [HttpPut("update/{id}")]
    public ActionResult<Task> Update(CategoryUpdateDto categoryUpdateDto,int id)
    {
        var result= _categoryManager.Update(categoryUpdateDto,id);
        if (result.Result == 0) return BadRequest("Failed to update");
        return Ok("Updated successfully");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<Task> Delete(int id)
    {
        var result= _categoryManager.Delete(id);
        if (result.Result == 0) return BadRequest("Failed to delete");
        return Ok("Deleted successfully");
    }
    
     
    [HttpGet("getFilteredCategories")]
    public Task<FilteredCategoryDto> GetFilteredCategoriesAsync(string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        
        return _categoryManager.GetFilteredCategoriesAsync(column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<CategoryDto>> GlobalSearch(string search,string? column)
    {
        return await _categoryManager.GlobalSearch(search,column);
    }


}