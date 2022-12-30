using ESHOP.Models;

namespace ESHOP.Services.Interfaces;

public interface IUserService
{
    List<User> GetAllUsers();
    void AddUser(User user);
    void RemoveUser(User user);
    bool IsExistByEmail(string email);
    bool IsUserExistById(int id);
    User GetUserForLogin(string email, string password);
    User GetUserById(int id);
    void Save();
    void UpdateUser(User user);
}