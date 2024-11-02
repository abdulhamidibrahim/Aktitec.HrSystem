using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Microsoft.AspNetCore.Http;

namespace Aktitic.HrProject.BL;

public class NotificationSettingsAddDto
{
   public bool Active { get; set; }
   
   public int CompanyId { get; set; }
   
   public string PageCode { get; set; }

}