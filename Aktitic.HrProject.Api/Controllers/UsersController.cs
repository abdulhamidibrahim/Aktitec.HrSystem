using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using Aktitic.HrProject.Api.Configuration;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrTaskList.BL;
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
    IAppModulesManager appModulesManager)
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
        
        var message = new Message(new [] {userAddDto.Email},"Confirm Email", confirmationLink);
        emailService.SendEmail(message);
        
        return Ok("Confirmation link sent Successfully, Please check your email");

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid) return BadRequest("Enter valid Data");
        var user = await applicationUserManager.FindByEmailAsync(loginDto.Email);
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
            // Expires = DateTime.UtcNow.AddHours(1),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"],
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SigningKey"]))
                    ,SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        // var permissions = unitOfWork.Permission.GetByApplicationUserId(user.Id);
        // var mappedPermissions = mapper.Map<List<Permission>, List<PermissionsDto>>(permissions);

        // var result = unitOfWork.ApplicationUser.GetUser(user.Id);
        
        var userData = new UserReadDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = appModulesManager.GetRole(user.RoleId),
            Email = user.Email,
            CompanyId = user.TenantId,
            Image = user.Image,
            EmployeeId = user.EmployeeId,
            UserName = user.UserName,
            Phone = user.PhoneNumber,
            Date = user.Date,
            IsAdmin = user.IsAdmin, 
            IsManager = user.IsManager,
            State = user.State,
            Country = user.Country,
            PinCode = user.PinCode,
            Birthday = user.Birthday,
            Address = user.Address,
            Gender = user.Gender,
            PassportNumber = user.PassportNumber,
            PassportExpDate = user.PassportExpDate,
            Tel = user.Tel,
            Nationality = user.Nationality,
            Religion = user.Religion,
            MatritalStatus = user.MatritalStatus,
            EmploymentSpouse = user.EmploymentSpouse,
            ChildrenNumber = user.ChildrenNumber,
            ReportsTo = user.ReportsToId
            
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

    [HttpPost("forgotPassword")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var user = await applicationUserManager.FindByEmailAsync(email);
        if (user is null) return BadRequest("user not found");
        var token = await userManager.GenerateUserTokenAsync(user, "Default", "ResetPassword");
        var resetLink = Url.Action(nameof(ResetPassword), "Users", new { token, email }, Request.Scheme);
        var message = new Message(new [] {email}, "Reset Password", $"Click here to reset your password :\n {resetLink}");
        emailService.SendEmail(message);
        return Ok("Reset Link sent successfully, Please check your email");
    }

    [HttpGet("resetPassword")]
    public async Task<IActionResult> ResetPassword(string email,string token)
    {
        var user = await applicationUserManager.FindByEmailAsync(email);
        if (user is null) return BadRequest("user not found");
        var result = await userManager.VerifyUserTokenAsync(user, "Default", "ResetPassword", token);
        var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
        if (!result) return BadRequest("Invalid Token");
        return Redirect($"http://localhost:4200/reset-password?token={resetToken}&email={email}");
    }
    
    [HttpPost("resetPassword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var user = await applicationUserManager.FindByEmailAsync(resetPasswordDto.Email);
        if (user is null) return BadRequest("user not found");
        var result = await userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
        if (!result.Succeeded) return BadRequest("Failed to reset password");
        return Ok("Password reset successfully");
    }
    
    [HttpPost("changePassword")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var user = await applicationUserManager.FindByEmailAsync(changePasswordDto.Email);
        if (user is null) return BadRequest("user not found");
        var result = await userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);
        if (!result.Succeeded) return BadRequest("Failed to change password");
        return Ok("Password changed successfully");
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
    public async Task<ActionResult> Update([FromForm] ApplicationUserUpdateDto userUpdateDto, int id)
    {
        
        var result = await applicationUserManager.Update(userUpdateDto,id);
        if (result == 0 ) return BadRequest("Failed to update");
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
