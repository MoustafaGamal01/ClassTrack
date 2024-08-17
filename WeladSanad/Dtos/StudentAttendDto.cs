namespace WeladSanad.Dtos
{
    public class StudentAttendDto
    {
        public string? Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int AttendId { get; set; }
    }
}
