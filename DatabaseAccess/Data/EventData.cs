using DatabaseAccess.DataAccess;
using DatabaseAccess.Models;
using MongoDB.Driver;

namespace DatabaseAccess.Data;

public class EventData : IEventData
{
    private readonly IDataAccess _dataAccess;
    private readonly string _databaseName;
    private readonly string _collectionName;


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

    public async Task<EventModel> ReadEvent(EventModel eventModel)
    {
        var collection = _dataAccess.GetCollection<EventModel>(_databaseName, _collectionName);
        var foundEvent = await collection.FindAsync(c => c.Id == eventModel.Id);
        return foundEvent.FirstOrDefault();
    }

    public Task UpdateEvent(EventModel eventModel)
    {
        var eventCollection = _dataAccess.GetCollection<EventModel>(_databaseName, _collectionName);
        var filter = Builders<EventModel>.Filter.Eq("Id", eventModel.Id);
        return eventCollection.ReplaceOneAsync(filter, eventModel, new ReplaceOptions { IsUpsert = true });
    }

    public Task DeleteEvent(EventModel eventModel)
    {
        var eventCollection = _dataAccess.GetCollection<EventModel>(_databaseName, _collectionName);
        return eventCollection.DeleteOneAsync(c => c.Id == eventModel.Id);
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

