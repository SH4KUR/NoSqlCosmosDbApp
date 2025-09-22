namespace NoSqlCosmosDbApp.Domain.Models;

public class PersonalInfo
{
    public required DateOnly BirthDate { get; set; }
    public required string Address { get; set; }
    public required string PhoneNumber { get; set; }
}