using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using User = NoSqlCosmosDbApp.Console.Models.User;

namespace NoSqlCosmosDbApp.Console.Repositories;

public class UserRepository
{
    private readonly Container _container;

    public UserRepository(IConfiguration configuration)
    {
        var client = new CosmosClient(
            configuration.GetValue<string>("CosmosDb:Endpoint"),
            configuration.GetValue<string>("CosmosDb:PrimaryKey")
        );

        var dbId = configuration.GetValue<string>("CosmosDb:Database");
        Database database = client.CreateDatabaseIfNotExistsAsync(dbId).Result;
        
        _container = database.CreateContainerIfNotExistsAsync(
            new ContainerProperties
            {
                Id = "Users",
                PartitionKeyPath = "/id"
            }
        ).Result;
    }

    public async Task<User> GetByIdAsync(string id)
    {
        var response = await _container.ReadItemAsync<User>(id, new PartitionKey(id));
        System.Console.WriteLine($"{nameof(GetByIdAsync)}: {response.RequestCharge} RUs");
        return response.Resource;
    }

    public async IAsyncEnumerable<User> GetAllAsync(QueryDefinition? query = null)
    {
        FeedIterator<User> feedIterator = _container.GetItemQueryIterator<User>(query);

        while (feedIterator.HasMoreResults)
        {
            FeedResponse<User> response = await feedIterator.ReadNextAsync();
            
            System.Console.WriteLine($"{nameof(GetAllAsync)}: {response.RequestCharge} RUs");

            foreach (var user in response)
            {
                yield return user;
            }
        }
    }
    
    public async Task<User> AddAsync(User user)
    {
        ItemResponse<User> response = await _container.CreateItemAsync(user);
        System.Console.WriteLine($"{nameof(AddAsync)}: {response.RequestCharge} RUs");
        return response.Resource;
    }
    
    public async Task<User> UpdateAsync(User user)
    {
        var userId = user.Id.ToString();
        
        var response = await _container.ReadItemAsync<User>(userId, new PartitionKey(userId));
        System.Console.WriteLine($"{nameof(UpdateAsync)} - Get: {response.RequestCharge} RUs");

        var existedUser = response.Resource;
        if (existedUser is null)
        {
            throw new InvalidOperationException($"User {userId} is not exist");
        }

        existedUser.FirstName = user.FirstName;
        existedUser.LastName = user.LastName;
        existedUser.Email = user.Email;

        response = await _container.ReplaceItemAsync(existedUser, userId, new PartitionKey(userId));

        System.Console.WriteLine($"{nameof(UpdateAsync)} - Replace: {response.RequestCharge} RUs");
        
        return response.Resource;
    }
    
    public async Task DeleteAsync(User user)
    {
        var userId = user.Id.ToString();
        
        var response = await _container.ReadItemAsync<User>(userId, new PartitionKey(userId));
        System.Console.WriteLine($"{nameof(DeleteAsync)} - Get: {response.RequestCharge} RUs");

        var existedUser = response.Resource;
        if (existedUser is null)
        {
            throw new InvalidOperationException($"User {userId} is not exist");
        }

        existedUser.FirstName = user.FirstName;
        existedUser.LastName = user.LastName;
        existedUser.Email = user.Email;

        response = await _container.DeleteItemAsync<User>(userId, new PartitionKey(userId));

        System.Console.WriteLine($"{nameof(DeleteAsync)} - Delete: {response.RequestCharge} RUs");
    }
}