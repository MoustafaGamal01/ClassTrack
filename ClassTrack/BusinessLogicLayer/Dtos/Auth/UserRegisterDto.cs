namespace ClassTrack.BusinessLogicLayer.Dtos.Auth
{
    public class UserRegisterDto
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
