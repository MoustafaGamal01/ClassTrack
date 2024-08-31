namespace ClassTrack.BusinessLogicLayer.Dtos.Student
{
    public class AddStudentDto
    {
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public int GroupId { get; set; }
    }
}
