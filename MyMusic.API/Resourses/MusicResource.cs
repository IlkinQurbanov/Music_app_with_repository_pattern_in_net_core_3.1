namespace MyMusic.API.Resourses
{


    public class MusicResource
    {
        public int id { get; set; }
        public string name { get; set; }
        public ArtistResource Artist { get; set; }

    }
}
