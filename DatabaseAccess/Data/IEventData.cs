using DatabaseAccess.Models;

namespace DatabaseAccess.Data;

public interface IEventData
{
    Task CreateEvent(EventModel eventModel);
    Task DeleteEvent(EventModel eventModel);
    Task<List<EventModel>> GetAllEventsForAUser(UserModel user);
    Task<List<EventModel>> ReadAllEvents(EventModel eventModel);
    Task<EventModel> ReadEvent(EventModel eventModel);
    Task UpdateEvent(EventModel eventModel);
}