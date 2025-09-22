using NoSqlCosmosDbApp.Domain.Models;

namespace NoSqlCosmosDbApp.Domain.Interfaces;

public interface IUserService
{
    Task<User?> GetByIdAsync(Guid id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(Guid id);
}