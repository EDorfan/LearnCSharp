using System.ComponentModel.DataAnnotations;

public class LoginViewModel 
{
    // Require Email
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    // Require Password
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    // Remember me
    public bool RememberMe { get; set; }
}