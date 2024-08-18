namespace WeladSanad.DataAccessLayer.Models
{
    public class Group
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [ForeignKey("Teacher")]
        public string? TeacherId { get; set; }
        public virtual ApplicationUser? Teacher { get; set; }
    }
}
