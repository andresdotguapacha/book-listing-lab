using System.ComponentModel.DataAnnotations;

public class AuthorDto
{
    public Guid Id { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }
}