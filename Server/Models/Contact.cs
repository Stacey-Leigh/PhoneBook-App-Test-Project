namespace PhonebookApp.Models;

public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? EmailAddress { get; set; }
}