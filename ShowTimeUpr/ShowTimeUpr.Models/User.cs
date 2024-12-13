using System.ComponentModel.DataAnnotations;

namespace ShowTimeUpr.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }
        public int Score { get; set; }
        [Required]
        public string PasswordHash { get; set; }
    }
}
