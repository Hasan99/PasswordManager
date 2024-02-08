using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models
{
    public class PasswordStoreItem
    {
        [Key]
        public int Id { get; set; }
        public string Category { get; set; }

        [Required]
        public string App { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string EncryptedPassword { get; set; }
    }
}
