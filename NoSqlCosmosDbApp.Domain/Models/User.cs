namespace NoSqlCosmosDbApp.Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required PersonalInfo PersonalInfo { get; set; }

    public ICollection<Group> UserGroups { get; set; } = [];
    public required WorkPlace WorkPlace { get; set; }
}