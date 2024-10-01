
using System.Collections;
using Aktitic.HrProject.BL;
using Aktitic.HrProject.DAL.Dtos;
using Aktitic.HrProject.DAL.Helpers;
using Aktitic.HrProject.DAL.Models;
using Aktitic.HrProject.DAL.Pagination.Client;
using Aktitic.HrProject.DAL.Repos;
using Aktitic.HrProject.DAL.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using Task = System.Threading.Tasks.Task;

namespace Aktitic.HrTaskList.BL;

public class EventManager(IUnitOfWork unitOfWork) : IEventManager
{
    public EventReadDto Add(EventAddDto eventAddDto)
    {
        var @event = new Event()
        {
            EventName = eventAddDto.Title,
            StarDate = eventAddDto.Start,
            EndDate = eventAddDto.End,
            EventCategory = eventAddDto.Color,
            CreatedAt = DateTime.Now,
        }; 
        var addEvent = unitOfWork.Events.AddEvent(@event);
        // return the added object 
        var eventReadDto = new EventReadDto()
        {
            Id = addEvent.Id,
            Title = addEvent.Result.EventName,
            Start = addEvent.Result.StarDate,
            End = addEvent.Result.EndDate,
            Color = addEvent.Result.EventCategory
        };
        return eventReadDto;
    }

    public Task<int> Update(EventUpdateDto eventUpdateDto, int id)
    {
        var @event = unitOfWork.Events.GetById(id);
        
        
        if (@event == null) return Task.FromResult(0);
        
        if(!eventUpdateDto.Title.IsNullOrEmpty()) @event.EventName = eventUpdateDto.Title;
        // if(eventUpdateDto.Start != null) @event.StarDate = eventUpdateDto.Start;
        // if(eventUpdateDto.End != null) @event.EndDate = eventUpdateDto.End;
        if(!eventUpdateDto.Color.IsNullOrEmpty()) @event.EventCategory = eventUpdateDto.Color;

        unitOfWork.Events.Update(@event);
        return unitOfWork.SaveChangesAsync();
    }

    public Task<int> Delete(int id)
    {
        var @event = unitOfWork.Events.GetById(id);
        if (@event == null) return Task.FromResult(0);
        @event.IsDeleted = true;
        @event.DeletedAt = DateTime.Now;
        return unitOfWork.SaveChangesAsync();
    }

    public EventReadDto? Get(int id)
    {
        var @event = unitOfWork.Events.GetById(id);
        if (@event == null) return null;
        return new EventReadDto()
        {
            Id = @event.Id,
            Title = @event.EventName,
            Start = @event.StarDate,
            End = @event.EndDate,
            Color = @event.EventCategory,
        };
    }

    public Task<List<EventReadDto>> GetAll()
    {
        var @event = unitOfWork.Events.GetAll();
        return Task.FromResult(@event.Result.Select(p => new EventReadDto()
        {
            Id = p.Id,
            Title = p.EventName,
            Color = p.EventCategory,
            Start = p.StarDate,
            End = p.EndDate,

        }).ToList());
    }

    public Task<List<EventReadDto>> GetByMonth(int month, int year)
    {
        
        var @event = unitOfWork.Events.GetByMonth(month, year);
        return Task.FromResult(@event.Result.Select(p => new EventReadDto()
        {
            Id = p.Id,
            Title = p.EventName,
            Color = p.EventCategory,
            Start = p.StarDate,
            End = p.EndDate,

        }).ToList());
    }
}
