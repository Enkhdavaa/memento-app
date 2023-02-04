using DatabaseAccess.DataAccess;
using DatabaseAccess.Models;
using MongoDB.Driver;

namespace DatabaseAccess.Data;

public class UserData : IUserData
{
    private readonly IDataAccess _dataAccess;
    private readonly string? _databaseName;
    private readonly string? _collectionName;


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

    public async Task<UserModel> ReadUser(string id)
    {
        var collection = _dataAccess.GetCollection<UserModel>(_databaseName, _collectionName);
        var foundUser = await collection.FindAsync(c => c.Id == id);
        return foundUser.FirstOrDefault();
    }

    public async Task UpdateUser(string id, string? email)
    {
        var collection = _dataAccess.GetCollection<UserModel>(_databaseName, _collectionName);
        var foundUsers = await collection.FindAsync(c => c.Id == id);
        var user = foundUsers.FirstOrDefault();

        var updatedUser = new UserModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = email
        };

        var filter = Builders<UserModel>.Filter.Eq("Id", id);
        var result = collection.ReplaceOneAsync(filter, updatedUser, new ReplaceOptions { IsUpsert = true });
        if(result.IsFaulted)
        {
            throw new Exception("Update User data failed");
        }
    }

    public Task DeleteUser(string id)
    {
        var collection = _dataAccess.GetCollection<UserModel>(_databaseName, _collectionName);
        return collection.DeleteOneAsync(c => c.Id == id);
    }

    private async Task<List<UserModel>> GetAllUsers()
    {
        var collection = _dataAccess.GetCollection<UserModel>(_databaseName, _collectionName);
        var results = await collection.FindAsync(_ => true);
        return results.ToList();
    }
}

