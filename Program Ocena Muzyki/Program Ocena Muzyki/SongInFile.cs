namespace Program_Ocena_Muzyki
{
    public class SongInFile : SongBase
    {
        public const string fileSong = "songs.txt";

        public override event OperationSuccesfulDelegate Succesful;
        public override void AddSong()
        {
            List<Song> songs = ReadSongFromFile();

            Console.Write("Podaj tytuł piosenki: ");
            string title = Console.ReadLine();
            Console.Write("Podaj artystę: ");
            string author = Console.ReadLine();
            int rating = 0;
            if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(author))
            {
                throw new Exception("Piosenka musi posiadać tytuł oraz artystę.");
            }

            bool songExists = songs.Any(song => song.Title.Equals(title, StringComparison.OrdinalIgnoreCase) && song.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
            if (!songExists)
            {
                using (var writer = File.AppendText(fileSong))
                {
                    writer.WriteLine($"{title};{author};{rating}");
                    Succesful(this, new EventArgs());
                    return;
                }
            }
            else
            {
                throw new Exception("Piosenka o podanym tytule i artyście już istnieje na liście.");
            }
        }

        public override void DeleteSong()
        {
            List<Song> songs = ReadSongFromFile();

            if (songs.Count == 0)
            {
                throw new Exception("Brak dostępnych piosenek do usunięcia.");
            }

            Console.WriteLine("\nLista piosenek: ");
            int songIndex = 1;
            foreach (var song in songs)
            {
                Console.WriteLine($"{songIndex}. {song.Title} - {song.Author}");
                songIndex++;
            }

            Console.Write("Podaj numer piosenki do usunięcia (lub 'Q' to zakończyć usuwanie: ");
            var input = Console.ReadLine().ToUpper();
            if (input == "Q")
            {
                Console.WriteLine("Usuwanie zakończone.");
            }
            else if (int.TryParse(input, out int songInput) && songInput >= 1 && songInput <= songs.Count)
            {
                songs.RemoveAt(songInput - 1);
                using (var writer = File.CreateText(fileSong))
                {
                    foreach (var song in songs)
                    {
                        writer.WriteLine($"{song.Title};{song.Author};{song.Rating}");
                    }
                }
                Succesful(this, new EventArgs());
            }
            else
            {
                throw new Exception("Niepoprawny numer piosenki do usunięcia.");
            }
        }

        public override void GetSongsStatistics()
        {
            List<Song> songs = ReadSongFromFile();

            if (songs.Count == 0)
            {
                throw new Exception("Brak dostępnych piosenek.");
            }

            Statistics songStatistics = new Statistics();

            foreach (var song in songs)
            {
                if (song.Rating >= 1 && song.Rating <= 10)
                {
                    songStatistics.AddRating(song.Rating);
                }
            }

            Console.WriteLine("Statystyki: ");
            Console.WriteLine($"\nNajwyżej oceniona piosenka: {FindSongByRating(songStatistics.Max)} (ocena: {songStatistics.Max})");
            Console.WriteLine($"Najniżej oceniona piosenka: {FindSongByRating(songStatistics.Min)} (ocena: {songStatistics.Min})");
            Console.WriteLine($"Średnia ocena wszystkich piosenek: {songStatistics.Average:F2}");
        }
        public string FindSongByRating(int rating)
        {
            List<Song> songs = ReadSongFromFile();

            foreach (var song in songs)
            {
                if (song.Rating == rating)
                    return song.Title;
            }
            return "Nie znaleziono";
        }

        public override void RateSongs()
        {
            List<Song> songs = ReadSongFromFile();

            if (songs.Count == 0)
            {
                throw new Exception("Brak dostępnych piosenek do oceny.");
            }
            while (true)
            {
                Console.WriteLine("\nLista piosenek: ");
                int songIndex = 1;
                foreach (var song in songs)
                {
                    Console.WriteLine($"{songIndex}. {song.Title} - {song.Author} Ocena: {song.Rating}");
                    songIndex++;
                }

                Console.Write("Wybierz numer utworu do oceny (lub 'Q' aby zakończyć): ");
                var input = Console.ReadLine().ToUpper();

                if (input == "Q")
                {
                    Console.WriteLine("Ocenianie zakończone.");
                    break;
                }
                else if (int.TryParse(input, out int songInput) && songInput >= 1 && songInput <= songs.Count)
                {
                    Console.Write($"Oceń utwór '{songs[songInput - 1].Title}' (1-10): ");
                    if (int.TryParse(Console.ReadLine(), out int rating) && rating >= 1 && rating <= 10)
                    {
                        Song selectedSong = songs[songInput - 1];
                        selectedSong.Rating = rating;
                        Succesful(this, new EventArgs());

                        using (var writer = File.CreateText(fileSong))
                        {
                            foreach (var song in songs)
                            {
                                writer.WriteLine($"{song.Title};{song.Author};{song.Rating}");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Niepoprawna ocena. Ocena musi być w zakresie 1-10.");
                    }
                }
                else
                {
                    throw new Exception("Nieporawny wybór, spróbuj ponownie.");
                }
            }
        }

        private List<Song> ReadSongFromFile()
        {
            var songs = new List<Song>();
            if (File.Exists(fileSong))
            {
                using (var reader = File.OpenText(fileSong))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(';');
                        if (parts.Length == 3)
                        {
                            string title = parts[0];
                            string author = parts[1];
                            int rating;
                            if (int.TryParse(parts[2], out rating))
                            {
                                songs.Add(new Song { Title = title, Author = author, Rating = rating });
                            }
                        }
                    }
                }
            }
            return songs;
        }
    }
}