using System.ComponentModel.DataAnnotations;

namespace MVCPro.BLL.ViewModels
{
    public class CreateUserVM
    {
        [Required(ErrorMessage = "First name is required")]
        public string FName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required")]
        public string LName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; } = string.Empty;

        public string FullName
        {
            get => $"{FName} {LName}";
            set
            {
                var parts = value.Split(' ');
                if (parts.Length >= 2) { FName = parts[0]; LName = parts[1]; }
                else if (parts.Length == 1) { FName = parts[0]; LName = string.Empty; }
            }
        }
    }

    public class UpdateUserVM : CreateUserVM
    {
        public int Id { get; set; }
    }

    public class UserVM
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime JoinedAt { get; set; }
    }
}
