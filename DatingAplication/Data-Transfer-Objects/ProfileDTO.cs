namespace DatingAplication.Data_Transfer_Objects
{
    public class ProfileDTO
    {
        public int ProfileId { get; set; }
        public int UserId { get; set; }
        public string Bio { get; set; }
        public string Location { get; set; }
        public string Interests { get; set; }
        public string ProfilePictureURL { get; set; }
    }

}
