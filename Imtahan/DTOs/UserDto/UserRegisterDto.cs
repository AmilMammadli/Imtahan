using System.ComponentModel.DataAnnotations;

namespace Imtahan.DTOs.UserDto
{
    public class UserRegisterDto
    {

        [Required, MaxLength(30)]
        public string? Username { get; set; }

        [Required, MaxLength(30)]
        public string? Fullname { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string PasswordConfirmed { get; set; }

    }

}
