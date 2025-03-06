using System.ComponentModel.DataAnnotations;

public class ProfileViewModel
{
    public string Email { get; set; }

    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }

    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
    public string ConfirmNewPassword { get; set; }
}
