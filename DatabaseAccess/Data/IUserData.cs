using DatabaseAccess.Models;

namespace DatabaseAccess.Data;

public interface IUserData
{
    Task CreateUser(UserModel user);
    Task DeleteEvent(UserModel user);
    Task<UserModel> ReadUser(UserModel user);
    Task UpdateUser(UserModel user);
}