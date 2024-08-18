using System.ComponentModel.DataAnnotations.Schema;
using WeladSanad.DataAccessLayer.Models;

namespace WeladSanad.DataAccessLayer.Models.Attendence
{
    public class StudentAttend
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        [ForeignKey("Attend")]
        public int? AttendId { get; set; }
        public virtual Attend? Attend { get; set; }
    }
}
