using NoSqlCosmosDbApp.Domain.Interfaces;
using NoSqlCosmosDbApp.Domain.Models;

namespace NoSqlCosmosDbApp.Infrastructure.Repositories;

public class UserRepository(IdentityDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(Guid id)
    {
        var user = await context.Users.FindAsync(id);
        if (user is null)
            return null;
        
        var entry = context.Users.Entry(user);
        
        await entry.Collection(u => u.UserGroups).LoadAsync();
        await entry.Reference(u => u.WorkPlace).LoadAsync();
        
        return user;
    }

    public async Task AddAsync(User user)
    {
        await context.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        var existedUser = await GetByIdAsync(user.Id)
                          ?? throw new KeyNotFoundException($"User with Id '{user.Id}' was not found.");

        existedUser.FirstName = user.FirstName;
        existedUser.LastName = user.LastName;
        existedUser.Email = user.Email;
        existedUser.PersonalInfo = user.PersonalInfo;
        existedUser.UserGroups = user.UserGroups;
        existedUser.WorkPlace = user.WorkPlace;

        context.Update(existedUser);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var existedUser = await GetByIdAsync(id)
                          ?? throw new KeyNotFoundException($"User with Id '{id}' was not found.");

        context.Users.Remove(existedUser);
        await context.SaveChangesAsync();
    }
}