using System;
using System.Threading.Tasks;
using System.Windows;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Model.Models;

namespace Model.Services;

public interface IUserService
{
    public User? CurrentUser { get; set; }
    User? GetCurrentUser();
    Task<User?> TryLogin(string login, string password);
    Task<bool> TrySignUp(User newUser);
}

public class UserService : IUserService
{
    public User? CurrentUser { get; set; }
    private readonly IDbContextFactory<QuizDb> _dbContextFactory;

    public UserService(IDbContextFactory<QuizDb> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
    }

    public User? GetCurrentUser() => CurrentUser;

    public async Task<User?> TryLogin(string login, string password)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();

        var user = await db.Users
            .FirstOrDefaultAsync(x => x.Login == login);

        if (user == null)
        {
            throw new IncorrectLoginException();
        }

        if (user.Password != password)
        {
            throw new IncorrectPasswordException();
        }

        CurrentUser = new User()
        {
            Login = user.Login,
            BirthDate = user.BirthDate,
        };

        return CurrentUser;
    }

    public async Task<bool> TrySignUp(User newUser)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();

        try
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Login == newUser.Login);
            if (user == null)
            {
                user = new UserEntity()
                {
                    Id = Guid.NewGuid(),
                    BirthDate = newUser.BirthDate,
                    Login = newUser.Login,
                    Password = newUser.Password
                };
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new LoginAlreadyExistException();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return false;
        }
    }
}