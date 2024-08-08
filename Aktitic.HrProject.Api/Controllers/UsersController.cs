using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using Aktitic.HrProject.Api.Configuration;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using IEmailService = User.Management.Services.Services.IEmailService;
using Message = User.Management.Services.Models.Message;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(
    IApplicationUserManager applicationUserManager,
    UserManager<ApplicationUser> userManager,
    IMapper mapper,
    IConfiguration configuration,
    IEmailService emailService,
    IOptions<JwtOptions> jwtOptions,
    IUnitOfWork unitOfWork)
    : ControllerBase
{

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(ApplicationUserAddDto userAddDto)
    {
        if (!ModelState.IsValid) return BadRequest("Enter Valid Data");
        var result = applicationUserManager.Add(userAddDto);
        var user = await userManager.CreateAsync(result,userAddDto.Password);
        if (!user.Succeeded) return BadRequest(user.Errors);
        
        var token = userManager.GenerateEmailConfirmationTokenAsync(result).Result;
        var confirmationLink = Url.Action
            (nameof(ConfirmEmail), 
                "Users", 
                new { token, email = userAddDto.Email },
                Request.Scheme);
        
        Message message = new Message(new [] {userAddDto.Email},"Confirm Email", confirmationLink);
        emailService.SendEmail(message);
        
        return Ok("Confirmation link sent Successfully, Please check your email");

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid) return BadRequest("Enter valid Data");
        var user = await userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return BadRequest("User Not Found");
        var correct= await userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!correct) return BadRequest("Wrong Password");
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(nameof(Company),user.TenantId.ToString() ?? string.Empty)
            }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"],
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SigningKey"]))
                    ,SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        var permissions = unitOfWork.Permission.GetByApplicationUserId(user.Id);
        var mappedPermissions = mapper.Map<List<Permission>, List<PermissionsDto>>(permissions);
        var userData = new ApplicationUserReadDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role,
            Email = user.Email,
            CompanyId = user.TenantId,
            Image = user.Image,
            EmployeeId = user.EmployeeId,
            UserName = user.UserName,
            Phone = user.PhoneNumber,
            Permissions = mappedPermissions,
            Date = user.Date,
           
        };
        
        // return data in the header 
        
        // HttpContext.Response.Headers.Add("X-User-Data", JsonConvert.SerializeObject(userData));
        
        return Ok(new 
            { 
                Token = tokenString , 
                Expiration = tokenDescriptor.Expires,
                userData 
            });
    }

    [HttpGet("confirmEmail")]

    public IActionResult ConfirmEmail(string token, string email)
    {
        var user = userManager.FindByEmailAsync(email).Result;
        if (user == null) return BadRequest("User Not Found");
        
        var result = userManager.ConfirmEmailAsync(user, token);
        if (!result.Result.Succeeded) return BadRequest("Failed to Confirm");
        
        return Ok("Confirmed Successfully, Now you can login");
     
    }

[HttpGet]
    public async Task<List<ApplicationUserReadDto>> GetAll()
    {
        return await applicationUserManager.GetAll();
    }
    
    [HttpGet("{id}")]
    public Task<ApplicationUserReadDto?> Get(int id)
    {
        var result = applicationUserManager.Get(id);
        if (result == null) return Task.FromResult<ApplicationUserReadDto?>(null);
        var hostUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}";
        result.Image = hostUrl + result.Image; 
        return Task.FromResult(result)!;
    }
    
    [HttpGet("getFilteredUsers")]
    public Task<FilteredApplicationUserDto> GetFilteredUsersAsync(int companyId, string? column, string? value1,string? @operator1,[Optional] string? value2, string? @operator2, int page, int pageSize)
    {
        return applicationUserManager.GetFilteredApplicationUsersAsync(companyId,column, value1, operator1 , value2,operator2,page,pageSize);
    }
    
    
    [HttpPost("create")]
    public  async Task<ActionResult> Create([FromForm] ApplicationUserAddDto userAddDto)
    {
        if (!ModelState.IsValid) return BadRequest("Enter Valid Data");
        var result = applicationUserManager.Create(userAddDto);
        var user = await userManager.CreateAsync(result,userAddDto.Password);
        if (!user.Succeeded) return BadRequest(user.Errors);

       return Ok("Created Successfully");
       
    }
    
    
    [HttpPut("update/{id}")]
    public ActionResult Update([FromForm] ApplicationUserUpdateDto userUpdateDto, int id)
    {
        
        var result = applicationUserManager.Update(userUpdateDto,id);
        if (result.Result == Task.FromResult(0)) return BadRequest("Failed to update");
        return Ok("updated successfully !");
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult Delete(int id)
    {
           var result =  applicationUserManager.Delete(id);
           if (result.Result > (0))
                return Ok(" deleted successfully!");
           
           return BadRequest("Failed to delete");
    }
    
    [HttpGet("GlobalSearch")]
    public async Task<IEnumerable<ApplicationUserDto>> GlobalSearch(int companyId, string search,string? column)
    {
        return await applicationUserManager.GlobalSearch(companyId, search,column);
    }
    
    
}