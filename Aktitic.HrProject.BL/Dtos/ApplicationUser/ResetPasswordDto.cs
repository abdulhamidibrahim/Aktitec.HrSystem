namespace Aktitic.HrProject.BL;

public class ResetPasswordDto
{
    public string Token { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}