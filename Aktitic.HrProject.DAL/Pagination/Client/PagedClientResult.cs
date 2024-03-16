namespace Aktitic.HrProject.DAL.Pagination.Client;

public class PagedClientResult
{
    
    public int TotalCount { get; set; }
    public IEnumerable<ClientDto> Clients { get; set; }
    public int TotalPages { get; set; }
    
}