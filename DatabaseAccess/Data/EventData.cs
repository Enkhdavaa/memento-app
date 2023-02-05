using DatabaseAccess.DataAccess;
using DatabaseAccess.Models;
using MongoDB.Driver;

namespace DatabaseAccess.Data;

public class EventData : IEventData
{
    private readonly IDataAccess _dataAccess;
    private readonly string? _databaseName;
    private readonly string? _collectionName;


    public EventData(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
        _databaseName = dataAccess.DatabaseName;
        _collectionName = dataAccess.EventCollectionName;
    }

    public Task CreateEvent(EventModel eventModel)
    {
        var eventCollection = _dataAccess.GetCollection<EventModel>(_databaseName, _collectionName);
        return eventCollection.InsertOneAsync(eventModel);
    }

    public async Task<EventModel> ReadEvent(string id)
    {
        var collection = _dataAccess.GetCollection<EventModel>(_databaseName, _collectionName);
        var foundEvents = await collection.FindAsync(c => c.Id == id);
        return foundEvents.FirstOrDefault();
    }

    public async Task UpdateEvent(string id, string? eventName, string? description, string? location, DateTime dateTime)
    {
        var collection = _dataAccess.GetCollection<EventModel>(_databaseName, _collectionName);
        var foundEvents = await collection.FindAsync(c => c.Id == id);
        var _event = foundEvents.FirstOrDefault();

        var updatedEvent = new EventModel
        {
            Id = id,
            AssignedTo = _event.AssignedTo,
            EventName = eventName,
            Description = description,
            Location = location,
            DateTime = dateTime
        };

        var filter = Builders<EventModel>.Filter.Eq("Id", id);
        var result = collection.ReplaceOneAsync(filter, updatedEvent, new ReplaceOptions { IsUpsert = true });

        if (result.IsFaulted)
        {
            throw new Exception("Update User data failed");
        }
    }

    public Task DeleteEvent(string id)
    {
        var eventCollection = _dataAccess.GetCollection<EventModel>(_databaseName, _collectionName);
        return eventCollection.DeleteOneAsync(c => c.Id == id);
    }

    public async Task<List<EventModel>> ReadAllEvents(EventModel eventModel)
    {
        var eventCollection = _dataAccess.GetCollection<EventModel>(_databaseName, _collectionName);
        var results = await eventCollection.FindAsync(_ => true);
        return results.ToList();
    }

    public async Task<List<EventModel>> GetAllEventsForAUser(UserModel user)
    {
        var eventCollection = _dataAccess.GetCollection<EventModel>(_databaseName, _collectionName);
        var results = await eventCollection.FindAsync(c => c.AssignedTo.Id == user.Id);
        return results.ToList();
    }
}