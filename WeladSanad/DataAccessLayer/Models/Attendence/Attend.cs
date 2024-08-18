namespace WeladSanad.DataAccessLayer.Models.Attendence
{
    public class Attend
    {
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
