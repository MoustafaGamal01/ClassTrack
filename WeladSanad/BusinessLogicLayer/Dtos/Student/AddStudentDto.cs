namespace WeladSanad.BusinessLogicLayer.Dtos.Student
{
    public class AddStudentDto
    {
        [Required]
        public string Name { get; set; }
        public int GroupId { get; set; }
    }
}
