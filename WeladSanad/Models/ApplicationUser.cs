namespace WeladSanad.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }

        [ForeignKey("Group")]
        public int GroupId { get; set; }    
        public virtual Group Group { get; set; }
    }
}
