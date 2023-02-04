using DatabaseAccess.Models;

namespace DatabaseAccess.Data;

public interface IEventData
{
    Task CreateEvent(EventModel eventModel);
    Task DeleteEvent(string id);
    Task<List<EventModel>> GetAllEventsForAUser(UserModel user);
    Task<List<EventModel>> ReadAllEvents(EventModel eventModel);
    Task<EventModel> ReadEvent(string id);
    Task UpdateEvent(string id, string? eventName, string? description, string? location, DateTime dateTime);
}