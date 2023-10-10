namespace Program_Ocena_Muzyki.Tests
{
    public class SongTests
    {
        [Test]
        public void AddSongMethodCheckSongInMemory()
        {
            var songInMemory = new SongInMemory();
            var songCount = songInMemory.songs.Count + 1;

            songInMemory.songs.Add(new Song { Title = "Test Song", Author = "Test Author" });

            Assert.AreEqual(songCount, songInMemory.songs.Count);
        }

        [Test]
        public void DeleteSongMethodCheckSongInMemory()
        {
            var songInMemory = new SongInMemory();
            songInMemory.songs.Add(new Song { Title = "Test Song", Author = "Test Author" });
            var songCount = songInMemory.songs.Count;

            songInMemory.songs.RemoveAt(0);

            Assert.AreEqual(songCount - 1, songInMemory.songs.Count);
        }

        [Test]
        public void RateSongsMethodCheckSongInMemory()
        {
            // Arrange
            var songInMemory = new SongInMemory();
            var song = new Song { Title = "Test Song", Author = "Test Author", Rating = 8 };
            songInMemory.songs.Add(song);


            var rating = songInMemory.FindSongByRating(8);

            // Assert
            Assert.AreEqual("Test Song", rating);
        }
    }
}