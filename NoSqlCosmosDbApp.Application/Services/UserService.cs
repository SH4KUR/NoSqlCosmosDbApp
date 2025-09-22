using NoSqlCosmosDbApp.Domain.Interfaces;
using NoSqlCosmosDbApp.Domain.Models;

namespace NoSqlCosmosDbApp.Application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<User?> GetByIdAsync(Guid id) => await userRepository.GetByIdAsync(id);

    public async Task AddAsync(User user) => await userRepository.AddAsync(user);

    public async Task UpdateAsync(User user) => await userRepository.UpdateAsync(user);

    public async Task DeleteAsync(Guid id) => await userRepository.DeleteAsync(id);
}