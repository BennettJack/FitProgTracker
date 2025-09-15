using System.ComponentModel.DataAnnotations;

namespace fpt_backend.Data.DTO.UserDTOs;

public class UserCreationRequestDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Username { get; set; }
}