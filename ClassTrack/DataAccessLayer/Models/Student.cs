using System.ComponentModel.DataAnnotations.Schema;

namespace ClassTrack.DataAccessLayer.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string? PhoneNumber { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public virtual Group? Group { get; set; }
    }
}
