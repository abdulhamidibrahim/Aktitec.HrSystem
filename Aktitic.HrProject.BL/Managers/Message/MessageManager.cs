
using System.Collections;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class MessageManager:IMessageManager
{
    private readonly IMessageRepo _messageRepo;
    private readonly IItemRepo _itemRepo;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public MessageManager(IMessageRepo messageRepo, IUnitOfWork unitOfWork, IMapper mapper, IItemRepo itemRepo)
    {
        _messageRepo = messageRepo;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _itemRepo = itemRepo;
    }
    
    public Task<int> Add(MessageAddDto messageAddDto)
    {
        var message = new Message()
        {
            SenderName = messageAddDto.SenderName,
            SenderPhotoUrl = messageAddDto.SenderPhotoUrl,
            SenderId = messageAddDto.SenderId,
            Date = messageAddDto.Date,
            Text = messageAddDto.Text,
            TaskId = messageAddDto.TaskId
            
        }; 
        _messageRepo.Add(message);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(MessageUpdateDto messageUpdateDto, int id)
    {
        var message = _messageRepo.GetById(id);
        
        if(messageUpdateDto.SenderName != null) message.SenderName = messageUpdateDto.SenderName;
        if (message == null) return Task.FromResult(0);
        
        
        _messageRepo.Update(message);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        _messageRepo.Delete(id);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<MessageReadDto>? Get(int id)
    {
        var message = _messageRepo.GetById(id);
        if (message == null) return null;
        return Task.FromResult(new MessageReadDto()
        {
           
        });
    }

    public Task<List<MessageReadDto>> GetAll()
    {
        var message = _messageRepo.GetAll();
        return Task.FromResult(message.Result.Select(p => new MessageReadDto()
        {
           

        }).ToList());
    }
    
//      public async Task<FilteredMessageDto> GetFilteredMessagesAsync(string? column, string? value1, string? operator1, string? value2, string? operator2, int page, int pageSize)
//     {
//         var users = await _messageRepo.GetAllMessageWithClients();
//         
//
//         // Check if column, value1, and operator1 are all null or empty
//         if (string.IsNullOrEmpty(column) || string.IsNullOrEmpty(value1) || string.IsNullOrEmpty(operator1))
//         {
//             var count = users.Count();
//             var pages = (int)Math.Ceiling((double)count / pageSize);
//
//             // Use ToList() directly without checking Any() condition
//             var userList = users.ToList();
//
//             var paginatedResults = userList.Skip((page - 1) * pageSize).Take(pageSize);
//     
//             var mappedMessages = new List<MessageDto>();
//             foreach (var message in paginatedResults)
//             {
//                 mappedMessages.Add(new MessageDto()
//                 {
//                     Id = message.Id,
//                     InvoiceNumber = message.InvoiceNumber,
//                     PaidDate = message.PaidDate,
//                     PaidAmount = message.PaidAmount,
//                     TotalAmount = message.TotalAmount,
//                     BankName = message.BankName,
//                     MessageType = message.MessageType,
//                     Address = message.Address,
//                     Country = message.Country,
//                     City = message.City,
//                     Iban = message.Iban,
//                     SwiftCode = message.SwiftCode,
//                     Status = message.Status,
//                     ClientId = message.ClientId,
//                     InvoiceId = message.InvoiceId,
//                     Client = _mapper.Map<Client,ClientDto>(message.Client!).FullName,
//                 });
//             }
//             FilteredMessageDto filteredMessageDto = new()
//             {
//                 MessageDto = mappedMessages,
//                 TotalCount = count,
//                 TotalPages = pages
//             };
//             return filteredMessageDto;
//         }
//
//         if (users != null)
//         {
//             IEnumerable<Message> filteredResults;
//         
//             // Apply the first filter
//             filteredResults = ApplyFilter(users, column, value1, operator1);
//
//             // Apply the second filter only if both value2 and operator2 are provided
//             if (!string.IsNullOrEmpty(value2) && !string.IsNullOrEmpty(operator2))
//             {
//                 filteredResults = filteredResults.Concat(ApplyFilter(users, column, value2, operator2));
//             }
//
//             var enumerable = filteredResults.Distinct().ToList();  // Use Distinct to eliminate duplicates
//             var totalCount = enumerable.Count();
//             var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
//             var paginatedResults = enumerable.Skip((page - 1) * pageSize).Take(pageSize);
//
//             // var messages = paginatedResults.ToList();
//             // var mappedMessage = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDto>>(messages);
//             
//             var mappedMessages = new List<MessageDto>();
//
//             foreach (var message in paginatedResults)
//             {
//                 
//                 mappedMessages.Add(new MessageDto()
//                 {
//                     Id = message.Id,
//                     InvoiceNumber = message.InvoiceNumber,
//                     PaidDate = message.PaidDate,
//                     PaidAmount = message.PaidAmount,
//                     TotalAmount = message.TotalAmount,
//                     BankName = message.BankName,
//                     MessageType = message.MessageType,
//                     Address = message.Address,
//                     Country = message.Country,
//                     City = message.City,
//                     Iban = message.Iban,
//                     SwiftCode = message.SwiftCode,
//                     Status = message.Status,
//                     ClientId = message.ClientId,
//                     InvoiceId = message.InvoiceId,
//                     Client = _mapper.Map<Client,ClientDto>(message.Client!).FullName,
//                 });
//             }
//             FilteredMessageDto filteredMessageDto = new()
//             {
//                 MessageDto = mappedMessages,
//                 TotalCount = totalCount,
//                 TotalPages = totalPages
//             };
//             return filteredMessageDto;
//         }
//
//         return new FilteredMessageDto();
//     }
//     private IEnumerable<Message> ApplyFilter(IEnumerable<Message> users, string? column, string? value, string? operatorType)
//     {
//         // value2 ??= value;
//
//         return operatorType switch
//         {
//             "contains" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
//             "doesnotcontain" => users.SkipWhile(e => value != null && column != null && e.GetPropertyValue(column).Contains(value,StringComparison.OrdinalIgnoreCase)),
//             "startswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).StartsWith(value,StringComparison.OrdinalIgnoreCase)),
//             "endswith" => users.Where(e => value != null && column != null && e.GetPropertyValue(column).EndsWith(value,StringComparison.OrdinalIgnoreCase)),
//             _ when decimal.TryParse(value, out var messageValue) => ApplyNumericFilter(users, column, messageValue, operatorType),
//             _ => users
//         };
//     }
//
//     private IEnumerable<Message> ApplyNumericFilter(IEnumerable<Message> users, string? column, decimal? value, string? operatorType)
// {
//     return operatorType?.ToLower() switch
//     {
//         "eq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var messageValue) && messageValue == value),
//         "neq" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var messageValue) && messageValue != value),
//         "gte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var messageValue) && messageValue >= value),
//         "gt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var messageValue) && messageValue > value),
//         "lte" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var messageValue) && messageValue <= value),
//         "lt" => users.Where(e => column != null && decimal.TryParse(e.GetPropertyValue(column), out var messageValue) && messageValue < value),
//         _ => users
//     };
// }
//
//
//     public Task<List<MessageDto>> GlobalSearch(string searchKey, string? column)
//     {
//         
//         if(column!=null)
//         {
//             IEnumerable<Message> user;
//             user = _messageRepo.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
//             var message = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDto>>(user);
//             return Task.FromResult(message.ToList());
//         }
//
//         var  users = _messageRepo.GlobalSearch(searchKey);
//         var messages = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageDto>>(users);
//         return Task.FromResult(messages.ToList());
//     }

}
