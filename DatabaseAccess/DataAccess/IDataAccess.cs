using MongoDB.Driver;

namespace DatabaseAccess.DataAccess
{
    public interface IDataAccess
    {
        string? DatabaseName { get; }
        string? UserCollectionName { get; }
        string? EventCollectionName { get; }

        IMongoCollection<T> GetCollection<T>(string? databaseName, in string? collectionName);
    }
}