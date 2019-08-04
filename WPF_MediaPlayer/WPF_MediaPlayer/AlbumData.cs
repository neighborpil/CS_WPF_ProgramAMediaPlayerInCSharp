namespace WPF_MediaPlayer
{
    public class AlbumData
    {
        public string Artist { get; set; }
        public string Album { get; set; }

        public AlbumData(string artist, string album)
        {
            Artist = artist;
            Album = album;
        }
    }
}