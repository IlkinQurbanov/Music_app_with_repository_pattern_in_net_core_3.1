namespace MyMusicMVC.Models
{
    public class Music
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ArtistId;
        public Artist Artist { get; set; }
    }
}
