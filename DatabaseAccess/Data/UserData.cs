using DatabaseAccess.DataAccess;
using DatabaseAccess.Models;
using MongoDB.Driver;

namespace DatabaseAccess.Data;

public class UserData : IUserData
{
    private readonly IDataAccess _dataAccess;
    private readonly string _databaseName;
    private readonly string _collectionName;


    public UserData(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
        _databaseName = _dataAccess.DatabaseName;
        _collectionName = _dataAccess.UserCollectionName;

    }

    public Task CreateUser(UserModel user)
    {
        var collection = _dataAccess.GetCollection<UserModel>(_databaseName, _collectionName);
        return collection.InsertOneAsync(user);
    }

    public async Task<UserModel> ReadUser(UserModel user)
    {
        var collection = _dataAccess.GetCollection<UserModel>(_databaseName, _collectionName);
        var foundUser = await collection.FindAsync(c => c.Id == user.Id);
        return foundUser.FirstOrDefault();
    }

    public Task UpdateUser(UserModel user)
    {
        var collection = _dataAccess.GetCollection<UserModel>(_databaseName, _collectionName);
        var filter = Builders<UserModel>.Filter.Eq("Id", user.Id);
        return collection.ReplaceOneAsync(filter, user, new ReplaceOptions { IsUpsert = true });
    }

    public Task DeleteEvent(UserModel user)
    {
        var collection = _dataAccess.GetCollection<UserModel>(_databaseName, _collectionName);
        return collection.DeleteOneAsync(c => c.Id == user.Id);
    }

    private async Task<List<UserModel>> GetAllUsers()
    {
        var collection = _dataAccess.GetCollection<UserModel>(_databaseName, _collectionName);
        var results = await collection.FindAsync(_ => true);
        return results.ToList();
    }
}

