namespace backend.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string? ProfilePicPath { get; set; } 
       public string? PhoneNumber { get; set; }
    }
}