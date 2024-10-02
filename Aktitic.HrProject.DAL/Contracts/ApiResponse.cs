namespace Aktitic.HrProject.Api.Configuration;

public class ApiRespone<T>
{
    public ApiRespone(T? data)
    {
        Data = data;
        Errors = null;
        Message = "Opearation is successful";
        Success = true;
    }
    
    public ApiRespone(string message, List<string>? errors = null)
    {
        Data = default;
        Errors = errors;
        Message = message;
        Success = false;
    }

    public T? Data { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
    public List<string>? Errors { get; set; }
    
}