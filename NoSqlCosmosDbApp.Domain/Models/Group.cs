namespace NoSqlCosmosDbApp.Domain.Models;

public class Group
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}