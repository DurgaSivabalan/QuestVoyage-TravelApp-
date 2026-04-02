public class Contact
{
    public int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Email { get; set; }
    public string Phone { get; set; }

    public string EnquiryType { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}