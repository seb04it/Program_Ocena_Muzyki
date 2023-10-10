using static Program_Ocena_Muzyki.SongBase;

namespace Program_Ocena_Muzyki
{
    public interface ISong
    {
        event OperationSuccesfulDelegate Succesful;
        void AddSong();
        void DeleteSong();
        void RateSongs();
        void GetSongsStatistics();
    }
}