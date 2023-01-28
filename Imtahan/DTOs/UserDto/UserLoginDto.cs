using System.ComponentModel.DataAnnotations;

namespace Imtahan.DTOs.UserDto
{
    public class UserLoginDto
    {
        [Required, MaxLength(30)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
