using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using File = System.IO.File;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class ContactsManager:IContactsManager
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ContactsManager(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
    }
    
    public Task<int> Add(ContactAddDto contactAddDto)
    {
        var contact = new Contact()
        {
            Name = contactAddDto.Name,
            Email = contactAddDto.Email,
            Number = contactAddDto.Number,
            Role = contactAddDto.Role,
            Type = contactAddDto.Type,
            Status = contactAddDto.Status,
            // CreatedAt = DateTime.Now,
        };

        var unique = Guid.NewGuid();

        var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/contacts", unique.ToString());

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        using var fileStream = new FileStream(Path.Combine(path, contactAddDto.Image.FileName), FileMode.Create);
       
        contactAddDto.Image.CopyTo(fileStream);
        
        contact.Image = "uploads/contacts/"+ unique + "/" + contactAddDto.Image.FileName;
        
        _unitOfWork.Contacts.Add(contact);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Update(ContactUpdateDto contactUpdateDto, int id)
    {
        var contact = _unitOfWork.Contacts.GetById(id);
        
        
        if (contact == null) return Task.FromResult(0);
        
        if(contactUpdateDto.Name != null) contact.Name = contactUpdateDto.Name;
        if(contactUpdateDto.Email != null) contact.Email = contactUpdateDto.Email;
        if(contactUpdateDto.Number != null) contact.Number = contactUpdateDto.Number;
        if(contactUpdateDto.Role != null) contact.Role = contactUpdateDto.Role;
        if(contactUpdateDto.Type != null) contact.Type = contactUpdateDto.Type;
        if(contactUpdateDto.Status != contact.Status) contact.Status = contactUpdateDto.Status;
       
        //update image
        if (contactUpdateDto.Image != null)
        {
            // Construct the path for the current image
            if (contact.Image != null)
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, contact.Image);

                // Delete the old image file if it exists
                if (File.Exists(oldImagePath))
                {
                    try
                    {
                        File.Delete(oldImagePath);
                    }
                    catch (Exception ex)
                    {
                        // Log the exception (you might want to use a logging framework)
                        Console.WriteLine($"Failed to delete old image: {ex.Message}");
                    }
                }
            }

            // Use the same path for the new image
            var unique = Path.GetDirectoryName(contact.Image);
            var path = Path.Combine(_webHostEnvironment.WebRootPath, unique);

            if (!Directory.Exists(path))    
            {
                Directory.CreateDirectory(path);
            }

            var filePath = Path.Combine(path, contactUpdateDto.Image.FileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            contactUpdateDto.Image.CopyTo(fileStream);

            contact.Image = Path.Combine(unique, contactUpdateDto.Image.FileName);
        }

        // contact.UpdatedAt = DateTime.Now;
        _unitOfWork.Contacts.Update(contact);
        return _unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        _unitOfWork.Contacts.Delete(id);
        // delete image
            var contact = _unitOfWork.Contacts.GetById(id);

            if (contact == null) return Task.FromResult(0);

            contact.IsDeleted = true;
            // contact.DeletedAt = DateTime.Now;

            _unitOfWork.Contacts.Update(contact);
        
        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, contact.Image);
        
        if (Directory.Exists(imagePath))
        {
            Directory.Delete(imagePath,true);
        }
        
        return _unitOfWork.SaveChangesAsync();
    }

    public ContactReadDto? Get(int id)
    {
        var contact = _unitOfWork.Contacts.GetById(id);
        if (contact == null) return null;
        return new ContactReadDto()
        {
            Id = contact.Id,
            Name = contact.Name,
            Email = contact.Email,
            Number = contact.Number,
            Role = contact.Role,
            Type = contact.Type,
            Image = contact.Image,
            Status = contact.Status,
        };
    }

    public Task<List<ContactReadDto>> GetAll()
    {
        var contact = _unitOfWork.Contacts.GetAll();
        return Task.FromResult(contact.Result.Select(c => new ContactReadDto()
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email,
            Number = c.Number,
            Role = c.Role,
            Type = c.Type,
            Image = c.Image,
            Status = c.Status,

        }).ToList());
    }

    public async Task<List<ContactReadDto>> GetByType(string type)
    {
        var contacts = await _unitOfWork.Contacts.GetByType(type);
        return contacts.Select(c => new ContactReadDto()
        {
            Id = c.Id,
            Name = c.Name,
            Email = c.Email,
            Number = c.Number,
            Role = c.Role,
            Type = c.Type,
            Image = c.Image,
            Status = c.Status,

        }).ToList();
    }


    public Task<List<ContactDto>> GlobalSearch(string searchKey, string? column)
    {
        
        if(column!=null)
        {
            IEnumerable<Contact> contactDto;
            contactDto = _unitOfWork.Contacts.GetAll().Result.Where(e => e.GetPropertyValue(column).ToLower().Contains(searchKey,StringComparison.OrdinalIgnoreCase));
            var contact = _mapper.Map<IEnumerable<Contact>, IEnumerable<ContactDto>>(contactDto);
            return Task.FromResult(contact.ToList());
        }

        var  contactDtos = _unitOfWork.Contacts.GlobalSearch(searchKey);
        var contacts = _mapper.Map<IEnumerable<Contact>, IEnumerable<ContactDto>>(contactDtos);
        return Task.FromResult(contacts.ToList());
    }

}
