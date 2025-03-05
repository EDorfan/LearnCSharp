using System.ComponentModel.DataAnnotations;

public class RegisterViewModel {
    // Require Email
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    // Require Password
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    // Require ConfirmPassword
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The passwords do not match")]
    public string ConfirmPassword { get; set; }
}