namespace NoSqlCosmosDbApp.Domain.Models;

public class UserGroup
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}