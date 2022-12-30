using ESHOP.Data;
using ESHOP.Models;
using ESHOP.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ESHOP.Services;

public class UserService : IUserService
{
    private readonly EShopContext _context;

    public UserService(EShopContext context)
    {
        _context = context;
    }
    
    public void AddUser(User user) => _context.Users.Add(user);
    
    public bool IsExistByEmail(string email) => _context.Users.Any(u => u.Email == email);
    
    public User GetUserForLogin(string email, string password) => _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
    
    public void Save() => _context.SaveChanges();
    
    public User GetUserById(int id) => _context.Users.FirstOrDefault(m => m.UserId == id);
    
    public void RemoveUser(User user) => _context.Users.Remove(user);
    
    public void UpdateUser(User user) => _context.Attach(user).State = EntityState.Modified;
    
    public bool IsUserExistById(int id) => _context.Users.Any(e => e.UserId == id);

    public List<User> GetAllUsers() => _context.Users.ToList();
    
}