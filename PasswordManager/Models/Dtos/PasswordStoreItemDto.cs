using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models.Dtos
{
    public class PasswordStoreItemDto
    {
        public int Id { get; set; }
        public string Category { get; set; }

        [Required]
        public string App { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
