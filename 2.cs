using System.ComponentModel.DataAnnotations;

namespace UserRegistrationApp
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(256)]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
