using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class EmailsManager(
    IWebHostEnvironment webHostEnvironment,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IEmailsManager
{
    public async Task<int> Add(EmailsAddDto emailsAddDto)
    {
        var receiverId = await unitOfWork.ApplicationUser.GetUserIdByEmail(emailsAddDto.ReceiverEmail);
        var emails = new Email()
        {
            SenderId = emailsAddDto.SenderId,
            ReceiverId = receiverId,
            ReceiverEmail = emailsAddDto.ReceiverEmail,
            Cc = emailsAddDto.Cc,
            Bcc = emailsAddDto.Bcc,
            Subject = emailsAddDto.Subject,
            Date = emailsAddDto.Date,
            Label = emailsAddDto.Label,
            Read = emailsAddDto.Read,
            Archive = emailsAddDto.Archive,
            Starred = emailsAddDto.Starred,
            Trash = emailsAddDto.Trash,
            Spam = emailsAddDto.Spam,
            Draft = emailsAddDto.Draft,
            Selected = emailsAddDto.Selected,
            Description = emailsAddDto.Description,
            
        };
        
        // upload attatchments to the server wwwroot
        if (emailsAddDto.Attatchments is { Count: > 0 })
        {
            emails.Attachments = new List<MailAttachment>(); // Initialize the attachments collection

            foreach (var attachmentDto in emailsAddDto.Attatchments)
            {
                // Generate a unique filename for the attachment to avoid collisions
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + attachmentDto.FileName;

                // Set the path to the wwwroot folder (e.g., "wwwroot/uploads")
                var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads");

                // Ensure the directory exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Combine the uploads folder path with the unique filename
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save the file to the server
                await using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await attachmentDto.CopyToAsync(fileStream);
                }

                // Create the MailAttachment entity and add it to the email
                var attachment = new MailAttachment
                {
                    Name = uniqueFileName,
                    Path = filePath, // Optionally, you could store the file as bytes here
                    Email = emails // Link the attachment to the email
                };

                // Add the attachment to the email's attachment collection
                emails.Attachments.Add(attachment);
            }
        }
        
        unitOfWork.Emails.Add(emails);
        return await unitOfWork.SaveChangesAsync();
    }

    public async Task<int> Update(EmailsUpdateDto emailsUpdateDto, int id)
{
    // Retrieve the email entity by ID
    var email = unitOfWork.Emails.GetById(id);

    // Check if the email exists
    if (email == null) return 0;
    
    // LogNote fields based on the properties in the Add function
    email.ReceiverId = await unitOfWork.ApplicationUser.GetUserIdByEmail(emailsUpdateDto.ReceiverEmail);
    if (emailsUpdateDto.SenderId != 0 && emailsUpdateDto.SenderId is not null) 
        email.SenderId = (int)emailsUpdateDto.SenderId;
    if (!emailsUpdateDto.ReceiverEmail.IsNullOrEmpty()) 
        email.ReceiverEmail = emailsUpdateDto.ReceiverEmail;
    if (!emailsUpdateDto.Cc.IsNullOrEmpty()) 
        email.Cc = emailsUpdateDto.Cc;
    if (!emailsUpdateDto.Bcc.IsNullOrEmpty()) 
        email.Bcc = emailsUpdateDto.Bcc;
    if (!emailsUpdateDto.Subject.IsNullOrEmpty()) 
        email.Subject = emailsUpdateDto.Subject;
    if (emailsUpdateDto.Date != email.Date && emailsUpdateDto.Date != null) 
        email.Date = (DateTime)emailsUpdateDto.Date;
    if (!emailsUpdateDto.Label.IsNullOrEmpty()) 
        email.Label = emailsUpdateDto.Label;
    if (emailsUpdateDto.Read is not null)
        email.Read = (bool)emailsUpdateDto.Read;
    if (emailsUpdateDto.Archive is not null) 
        email.Archive = (bool)emailsUpdateDto.Archive;
    if (emailsUpdateDto.Starred is not null) 
        email.Starred = (bool)emailsUpdateDto.Starred;
    if (emailsUpdateDto.Trash is not null) 
        email.Trash = (bool)emailsUpdateDto.Trash;
    if (emailsUpdateDto.Spam is not null) 
        email.Spam = (bool)emailsUpdateDto.Spam;
    if (emailsUpdateDto.Draft is not null) 
        email.Draft = (bool)emailsUpdateDto.Draft;
    if (emailsUpdateDto.Selected is not null) 
        email.Selected = (bool)emailsUpdateDto.Selected;
    if (!emailsUpdateDto.Description.IsNullOrEmpty()) 
        email.Description = emailsUpdateDto.Description;

    // Handle attachments update if needed
    // if (emailsUpdateDto.Attatchments is { Count: > 0 })
    // {
    //     email.Attachments?.Clear();
    //
    //     foreach (var attachmentDto in emailsUpdateDto.Attatchments)
    //     {
    //         var uniqueFileName = Guid.NewGuid().ToString() + "_" + attachmentDto.FileName;
    //         var uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
    //
    //         if (!Directory.Exists(uploadsFolder))
    //         {
    //             Directory.CreateDirectory(uploadsFolder);
    //         }
    //
    //         var filePath = Path.Combine(uploadsFolder, uniqueFileName);
    //
    //         await using (var fileStream = new FileStream(filePath, FileMode.Create))
    //         {
    //             await attachmentDto.CopyToAsync(fileStream);
    //         }
    //
    //         var attachment = new MailAttachment
    //         {
    //             Name = uniqueFileName,
    //             Path = filePath, 
    //             Email = email
    //         };
    //
    //         email.Attachments?.Add(attachment);
    //     }
    // }

    // LogNote the email entity
    
    unitOfWork.Emails.Update(email);

    // Save changes asynchronously and return the result
    return await unitOfWork.SaveChangesAsync();
}

    public Task<int> Delete(int id)
    {
        var emails = unitOfWork.Emails.GetById(id);
        if (emails==null) return Task.FromResult(0);
        emails.IsDeleted = true;
        // emails.DeletedAt = DateTime.Now;
        // emails.DeletedBy = userUtility.GetUserId();
        unitOfWork.Emails.Update(emails);
        return unitOfWork.SaveChangesAsync();
    }

    public EmailsReadDto? Get(int id)
    {
        var emails =  unitOfWork.Emails.GetEmail(id);
        if (emails == null) return null;
        
        return new EmailsReadDto()
        {
            Id = emails.Id,
            SenderId = emails.SenderId,
            ReceiverId = emails.ReceiverId,
            ReceiverEmail = emails.ReceiverEmail,
            Cc = emails.Cc,
            Bcc = emails.Bcc,
            Subject = emails.Subject,
            Date = emails.Date,
            Label = emails.Label,
            Read = emails.Read,
            Archive = emails.Archive,
            Starred = emails.Starred,
            Trash = emails.Trash,
            Spam = emails.Spam,
            Draft = emails.Draft,
            Sender = mapper.Map<ApplicationUser,ApplicationUserDto>(emails.Sender),
            Receiver = mapper.Map<ApplicationUser,ApplicationUserDto>(emails.Receiver),
            Selected = emails.Selected,
            Description = emails.Description,
            Attachments = emails.Attachments?.Select(a => new AttachmentDto()
            {
                Id = a.Id,
                Name = a.Name,
                Path = a.Path,
                Size = a.Size,
                Type = a.Type,
                
            }).ToList(),
            CreatedAt = emails.CreatedAt,
            CreatedBy = emails.CreatedBy,
            UpdatedAt = emails.UpdatedAt,
            UpdatedBy = emails.UpdatedBy,    
        };
    }

    public async Task<List<EmailsReadDto>> GetAll(string email)
    {
        var emails = await unitOfWork.Emails.GetReceivedEmails( email);
        return emails.Select(e => new EmailsReadDto()
        {
            Id = e.Id,
            SenderId = e.SenderId,
            ReceiverEmail = e.ReceiverEmail,
            ReceiverId = e.ReceiverId,
            Cc = e.Cc,
            Bcc = e.Bcc,
            Subject = e.Subject,
            Date = e.Date,
            Label = e.Label,
            Read = e.Read,
            Archive = e.Archive,
            Starred = e.Starred,
            Trash = e.Trash,
            Spam = e.Spam,
            Draft = e.Draft,
            Selected = e.Selected,
            Description = e.Description,
            Attachments = e.Attachments?.Select(a => new AttachmentDto()
            {
                Id = a.Id,
                Name = a.Name,
                Path = a.Path,
                Size = a.Size,
                Type = a.Type,
            }).ToList(),
            CreatedAt = e.CreatedAt,
            CreatedBy = e.CreatedBy,
            UpdatedAt = e.UpdatedAt,
            UpdatedBy = e.UpdatedBy,    
        }).ToList();
    }

    public async Task<List<EmailsReadDto>> GetSentEmails(int id)
    {
        var emails = await unitOfWork.Emails.GetSentEmails( id);
        return emails.Select(e => new EmailsReadDto()
        {
            Id = e.Id,
            SenderId = e.SenderId,
            ReceiverEmail = e.ReceiverEmail,
            ReceiverId = e.ReceiverId,
            Cc = e.Cc,
            Bcc = e.Bcc,
            Subject = e.Subject,
            Date = e.Date,
            Label = e.Label,
            Read = e.Read,
            Archive = e.Archive,
            Starred = e.Starred,
            Trash = e.Trash,
            Spam = e.Spam,
            Draft = e.Draft,
            Selected = e.Selected,
            Description = e.Description,
            Attachments = e.Attachments?.Select(a => new AttachmentDto()
            {
                Id = a.Id,
                Name = a.Name,
                Path = a.Path,
                Size = a.Size,
                Type = a.Type,
            }).ToList(),
            CreatedAt = e.CreatedAt,
            CreatedBy = e.CreatedBy,
            UpdatedAt = e.UpdatedAt,
            UpdatedBy = e.UpdatedBy,    
        }).ToList();
    }


    public async Task<FilteredEmailsDto> GetFilteredEmailsAsync
        (string? column, string? value1, string? operator1, string? value2,
            string? operator2, int page, int pageSize,string email)
    {
        var emailsList = await unitOfWork.Emails.GetReceivedEmails(email);


        // Check if column, value1, and operator1 are all null or empty
        if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
        {
            var count = emailsList.Count();
            var pages = (int)Math.Ceiling((double)count / pageSize);

            // Use ToList() directly without checking Any() condition
            var list = emailsList.ToList();

            var paginatedResults = list.Skip((page - 1) * pageSize).Take(pageSize);
    
            var emailsDto = new List<EmailsDto>();
            foreach (var emails in paginatedResults)
            {
                emailsDto.Add(new EmailsDto()
                {
                    Id = emails.Id,
                    SenderId = emails.SenderId,
                    ReceiverId = emails.ReceiverId,
                    Cc = emails.Cc,
                    ReceiverEmail = emails.ReceiverEmail,
                    Bcc = emails.Bcc,
                    Subject = emails.Subject,
                    Date = emails.Date,
                    Label = emails.Label,
                    Read = emails.Read,
                    Archive = emails.Archive,
                    Starred = emails.Starred,
                    Trash = emails.Trash,
                    Spam = emails.Spam,
                    Sender = mapper.Map<ApplicationUserDto>(emails.Sender),
                    Draft = emails.Draft,
                    Selected = emails.Selected,
                    Description = emails.Description,
                    Attachments = emails.Attachments?.Select(a => new AttachmentDto()
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Path = a.Path,
                        Size = a.Size,
                        Type = a.Type,
                    }).ToList(),
                    CreatedAt = emails.CreatedAt,
                    CreatedBy = emails.CreatedBy,
                    UpdatedAt = emails.UpdatedAt,
                    UpdatedBy = emails.UpdatedBy,    
                });
            }
            FilteredEmailsDto filteredBudgetsExpensesDto = new()
            {
                EmailsDto = emailsDto,
                TotalCount = count,
                TotalPages = pages
            };
            return filteredBudgetsExpensesDto;
        }

        if (emailsList != null)
        {
            IEnumerable<Email> filteredResults;
        
            // Apply the first filter
            filteredResults = ApplyFilter(emailsList, column, value1, operator1);

            // Apply the second filter only if both value2 and operator2 are provided
            if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
            {
                filteredResults = filteredResults.Concat(ApplyFilter(emailsList, column, value2, operator2));
            }

            var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
            var totalCount = enumerable.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);

            // var emailss = paginatedResults.ToList();
            // var mappedBudgetsExpenses = _mapper.Map<IEnumerable<BudgetsExpenses>, IEnumerable<BudgetsExpensesDto>>(emailss);
            
            var mappedEmails = new List<EmailsDto>();

            foreach (var emails in paginatedResults)
            {
                
                mappedEmails.Add(new EmailsDto()
                {
                    Id = emails.Id,
                    SenderId = emails.SenderId,
                    ReceiverId = emails.ReceiverId,
                    ReceiverEmail = emails.ReceiverEmail,
                    Cc = emails.Cc,
                    Bcc = emails.Bcc,
                    Subject = emails.Subject,
                    Date = emails.Date,
                    Label = emails.Label,
                    Read = emails.Read,
                    Archive = emails.Archive,
                    Starred = emails.Starred,
                    Trash = emails.Trash,
                    Spam = emails.Spam,
                    Draft = emails.Draft,
                    Sender = mapper.Map<ApplicationUserDto>(emails.Sender),
                    Selected = emails.Selected,
                    Description = emails.Description,
                    Attachments = emails.Attachments?.Select(a => new AttachmentDto()
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Path = a.Path,
                        Size = a.Size,
                        Type = a.Type,
                    }).ToList(),
                    CreatedAt = emails.CreatedAt,
                    CreatedBy = emails.CreatedBy,
                    UpdatedAt = emails.UpdatedAt,
                    UpdatedBy = emails.UpdatedBy,    
                });
            }
            FilteredEmailsDto filteredEmailsDto = new()
            {
                EmailsDto = mappedEmails,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
            return filteredEmailsDto;
        }

        return new FilteredEmailsDto();
    }
    private IEnumerable<Email> ApplyFilter(IEnumerable<Email> emails, string? column, string? value, string? operatorType)
    {
        // value2 ??= value;

        return operatorType switch
        {
            "contains" => emails.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "doesnotcontain" => emails.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
            "startswith" => emails.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
            "endswith" => emails.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
            _ when decimal.TryParse(value, out var emailsValue) => ApplyNumericFilter(emails, column, emailsValue, operatorType),
            _ => emails
        };
    }

    private IEnumerable<Email> ApplyNumericFilter(IEnumerable<Email> policys, string? column, decimal? value, string? operatorType)
{
    return operatorType?.ToLower() switch
    {
        "eq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var emailsValue) && emailsValue == value),
        "neq" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var emailsValue) && emailsValue != value),
        "gte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var emailsValue) && emailsValue >= value),
        "gt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var emailsValue) && emailsValue > value),
        "lte" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var emailsValue) && emailsValue <= value),
        "lt" => policys.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var emailsValue) && emailsValue < value),
        _ => policys
    };
}


    public Task<List<EmailsDto>> GlobalSearch(string searchKey, string? column, string email)
    {
        
        if(column!=null)
        {
            IEnumerable<Email> enumerable = unitOfWork.Emails.GetReceivedEmails(email).Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var emails = enumerable.Select(emails => new EmailsDto()
            {
                Id = emails.Id,
                SenderId = emails.SenderId,
                ReceiverId = emails.ReceiverId,
                ReceiverEmail = emails.ReceiverEmail,
                Cc = emails.Cc,
                Bcc = emails.Bcc,
                Subject = emails.Subject,
                Date = emails.Date,
                Label = emails.Label,
                Read = emails.Read,
                Archive = emails.Archive,
                Starred = emails.Starred,
                Trash = emails.Trash,
                Spam = emails.Spam,
                Draft = emails.Draft,
                Selected = emails.Selected,
                Sender = mapper.Map<ApplicationUserDto>(emails.Sender),
                Description = emails.Description,
                Attachments = emails.Attachments?.Select(a => new AttachmentDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Path = a.Path,
                    Size = a.Size,
                    Type = a.Type,
                }).ToList(),
                CreatedAt = emails.CreatedAt,
                CreatedBy = emails.CreatedBy,
                UpdatedAt = emails.UpdatedAt,
                UpdatedBy = emails.UpdatedBy,    
            });
            return Task.FromResult(emails.ToList());
        }

        var  queryable = unitOfWork.Emails.GlobalSearch(searchKey,email);
        var emailss = queryable.Select(emails => new EmailsDto()
        {
            Id = emails.Id,
            SenderId = emails.SenderId,
            ReceiverId = emails.ReceiverId,
            ReceiverEmail = emails.ReceiverEmail,
            Cc = emails.Cc,
            Bcc = emails.Bcc,
            Subject = emails.Subject,
            Date = emails.Date,
            Label = emails.Label,
            Read = emails.Read,
            Archive = emails.Archive,
            Starred = emails.Starred,
            Trash = emails.Trash,
            Spam = emails.Spam,
            Draft = emails.Draft,
            Sender = mapper.Map<ApplicationUserDto>(emails.Sender),
            Selected = emails.Selected,
            Description = emails.Description,
            Attachments = emails.Attachments.Select(a => new AttachmentDto()
            {
                Id = a.Id,
                Name = a.Name,
                Path = a.Path,
                Size = a.Size,
                Type = a.Type,
            }).ToList(),
            CreatedAt = emails.CreatedAt,
            CreatedBy = emails.CreatedBy,
            UpdatedAt = emails.UpdatedAt,
            UpdatedBy = emails.UpdatedBy,    
        });
        return Task.FromResult(emailss.ToList());
    }

    public async Task<IEnumerable<EmailsDto>> GetStarredEmails(int? page, int? pageSize,string email)
    {
         var emails = await unitOfWork.Emails.GetStarredEmails(page, pageSize,email);
         return emails.Select(e => new EmailsDto()
         {
             Id = e.Id,
             SenderId = e.SenderId,
             ReceiverId = e.ReceiverId,
             ReceiverEmail = e.ReceiverEmail,
             Cc = e.Cc,
             Bcc = e.Bcc,
             Subject = e.Subject,
             Date = e.Date,
             Label = e.Label,
             Read = e.Read,
             Archive = e.Archive,
             Starred = e.Starred,
             Trash = e.Trash,
             Spam = e.Spam,
             Draft = e.Draft,
             Selected = e.Selected,
             Description = e.Description,
             Attachments = e.Attachments?.Select(a => new AttachmentDto()
             {
                 Id = a.Id,
                 Name = a.Name,
                 Path = a.Path,
                 Size = a.Size,
                 Type = a.Type,
             }).ToList(),
         });
    } 
    

    public async Task<IEnumerable<EmailsDto>> GetTrashedEmails(int? page, int? pageSize,string email)
    {
        var emails = await unitOfWork.Emails.GetTrashedEmails(page, pageSize,email);
        return emails.Select(e => new EmailsDto()
        {
            Id = e.Id,
            SenderId = e.SenderId,
            ReceiverId = e.ReceiverId,
            ReceiverEmail = e.ReceiverEmail,
            Cc = e.Cc,
            Bcc = e.Bcc,
            Subject = e.Subject,
            Date = e.Date,
            Label = e.Label,
            Read = e.Read,
            Archive = e.Archive,
            Starred = e.Starred,
            Trash = e.Trash,
            Spam = e.Spam,
            Draft = e.Draft,
            Selected = e.Selected,
            Description = e.Description,
            Attachments = e.Attachments?.Select(a => new AttachmentDto()
            {
                Id = a.Id,
                Name = a.Name,
                Path = a.Path,
                Size = a.Size,
                Type = a.Type,
            }).ToList(),
        });
    }

    public async Task<IEnumerable<EmailsDto>> GetArchivedEmails(int? page, int? pageSize, string email)
    {
        var emails = await unitOfWork.Emails.GetArchivedEmails(page, pageSize,email);
        return emails.Select(e => new EmailsDto()
        {
            Id = e.Id,
            SenderId = e.SenderId,
            ReceiverId = e.ReceiverId,
            ReceiverEmail = e.ReceiverEmail,
            Cc = e.Cc,
            Bcc = e.Bcc,
            Subject = e.Subject,
            Date = e.Date,
            Label = e.Label,
            Read = e.Read,
            Archive = e.Archive,
            Starred = e.Starred,
            Trash = e.Trash,
            Spam = e.Spam,
            Draft = e.Draft,
            Selected = e.Selected,
            Description = e.Description,
            Attachments = e.Attachments?.Select(a => new AttachmentDto()
            {
                Id = a.Id,
                Name = a.Name,
                Path = a.Path,
                Size = a.Size,
                Type = a.Type,
            }).ToList(),
        });
    }

    public async Task<IEnumerable<EmailsDto>> GetDraftEmails(int? page, int? pageSize,string email)
    {
        var emails = await unitOfWork.Emails.GetDraftedEmails(page, pageSize,email);
        return emails.Select(e => new EmailsDto()
        {
            Id = e.Id,
            SenderId = e.SenderId,
            ReceiverId = e.ReceiverId,
            ReceiverEmail = e.ReceiverEmail,
            Cc = e.Cc,
            Bcc = e.Bcc,
            Subject = e.Subject,
            Date = e.Date,
            Label = e.Label,
            Read = e.Read,
            Archive = e.Archive,
            Starred = e.Starred,
            Trash = e.Trash,
            Spam = e.Spam,
            Draft = e.Draft,
            Selected = e.Selected,
            Description = e.Description,
            Attachments = e.Attachments?.Select(a => new AttachmentDto()
            {
                Id = a.Id,
                Name = a.Name,
                Path = a.Path,
                Size = a.Size,
                Type = a.Type,
            }).ToList(),
        });
    }

    public async Task<AttachmentDto> GetAttachments(int id)
    {
        var e = await unitOfWork.Emails.GetAttachments(id);
        if (e == null) return null;
        return  new AttachmentDto()
        {
            Id = e.Id,
            Name = e.Name,
            Path = e.Path,    
            Size = e.Size,
            Type = e.Type,
        };
    }
}
