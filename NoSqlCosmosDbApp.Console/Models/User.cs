using Newtonsoft.Json;

namespace NoSqlCosmosDbApp.Console.Models;

public class User
{
    [JsonProperty(PropertyName = "id")]
    public Guid Id { get; set; }

    [JsonProperty(PropertyName = "firstName")]
    public string FirstName { get; set; } = null!;

    [JsonProperty(PropertyName = "lastName")]
    public string LastName { get; set; } = null!;
    
    [JsonProperty(PropertyName = "email")]
    public string Email { get; set; } = null!;
}