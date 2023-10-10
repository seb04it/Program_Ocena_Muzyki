namespace Program_Ocena_Muzyki
{
    public abstract class SongBase : ISong
    {
        public delegate void OperationSuccesfulDelegate(object sender, EventArgs args);
        public abstract event OperationSuccesfulDelegate Succesful;
        public abstract void AddSong();
        public abstract void DeleteSong();
        public abstract void GetSongsStatistics();
        public abstract void RateSongs();
    }
}
