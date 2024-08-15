using System.ComponentModel.DataAnnotations.Schema;

namespace WeladSanad.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Group")]
        public int? GroupId { get; set; }
        public virtual Group? Group { get; set; }
    }
}
