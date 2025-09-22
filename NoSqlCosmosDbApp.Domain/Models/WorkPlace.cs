namespace NoSqlCosmosDbApp.Domain.Models;

public class WorkPlace
{
    public Guid Id { get; set; }
    public required int Room { get; set; }

    public required User User { get; set; }
}