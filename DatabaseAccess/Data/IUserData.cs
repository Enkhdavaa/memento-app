using DatabaseAccess.Models;

namespace DatabaseAccess.Data;

public interface IUserData
{
    Task CreateUser(UserModel user);
    Task DeleteUser(string id);
    Task<UserModel> ReadUser(string id);
    Task UpdateUser(string id, string? email);
}