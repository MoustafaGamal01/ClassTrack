using System.ComponentModel.DataAnnotations.Schema;

namespace WeladSanad.Models
{
    public class StudentAttend
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Student")]
        public int? StudentId { get; set; }
        public virtual Student? Student { get; set; }

        [ForeignKey("Attend")]
        public int? AttendId { get; set; }
        public virtual Attend? Attend { get; set; }
    }
}
