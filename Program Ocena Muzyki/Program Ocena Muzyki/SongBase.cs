namespace Program_Ocena_Muzyki
{
    public abstract class SongBase : ISong
    {
        public abstract void AddSong();
        public abstract void DeleteSong();
        public abstract void GetSongsStatistics();
        public abstract void RateSongs();
    }
}
