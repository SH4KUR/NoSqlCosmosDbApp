using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using NoSqlCosmosDbApp.Console.Repositories;

Console.WriteLine("NoSqlCosmosDbApp.Console\n\n---\n");

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true)
    .AddUserSecrets<Program>();

IConfiguration configuration = builder.Build();

var repository = new UserRepository(configuration);

// Get All
var users = repository.GetAllAsync();
await foreach (var user in users)
{
    Console.WriteLine($"{user.FirstName} {user.LastName} {user.Email}");   
}

// GetByFirstName
var query = new QueryDefinition("SELECT * FROM Users WHERE Users.firstName = @firstName")
    .WithParameter("@firstName", "Frodo");
var usersWithFirstName = repository.GetAllAsync(query);
await foreach (var user in usersWithFirstName)
{
    Console.WriteLine($"{user.FirstName} {user.LastName} {user.Email}");   
}

Console.Write("\n---\n\nPress ENTER to exit: ");
Console.ReadLine();