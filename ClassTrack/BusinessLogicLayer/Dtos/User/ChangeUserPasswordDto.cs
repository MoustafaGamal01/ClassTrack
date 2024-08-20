namespace ClassTrack.BusinessLogicLayer.Dtos.User
{
    public class ChangeUserPasswordDto
    {
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }

    }
}
