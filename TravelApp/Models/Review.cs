using System.ComponentModel.DataAnnotations;

public class Review
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Comment { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    public int HelpfulCount { get; set; } = 0; // default 0
}