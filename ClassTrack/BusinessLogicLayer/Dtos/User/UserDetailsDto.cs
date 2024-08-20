namespace ClassTrack.BusinessLogicLayer.Dtos.User
{
    public class UserDetailsDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }
        
        public string Role { get; set; }

        public List<string>? Groups { get; set; } = new List<string>();
    }
}
