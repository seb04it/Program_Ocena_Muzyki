namespace Program_Ocena_Muzyki
{
    public class SongInFile : SongBase
    {
        public const string fileSong = "songs.txt";
        public override void AddSong()
        {
            Console.Write("Podaj tytuł piosenki: ");
            string title = Console.ReadLine();
            Console.Write("Podaj artystę: ");
            string author = Console.ReadLine();
            int rating = 0;
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(author))
            {
                using (var writer = File.AppendText(fileSong))
                {
                    writer.WriteLine($"{title};{author};{rating}");
                    Console.WriteLine("Utwór dodano pomyślnie.");
                }
            }
            else
            {
                Console.WriteLine("Piosenka musi posiadać tytuł oraz artystę.");
            }
        }

        public override void DeleteSong()
        {
            List<Song> songs = ReadSongFromFile();

            if (songs.Count == 0)
            {
                Console.WriteLine("Brak dostępnych piosenek do usunięcia.");
                return;
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
                Console.WriteLine("Utwór został usunięty pomyślnie.");
            }
            else
            {
                Console.WriteLine("Niepoprawny numer piosenki do usunięcia.");
            }
        }

        public override void GetSongsStatistics()
        {
            List<Song> songs = ReadSongFromFile();

            if (songs.Count == 0)
            {
                Console.WriteLine("Brak dostępnych piosenek.");
                return;
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
                Console.WriteLine("Brak dostępnych piosenek do oceny.");
                return;
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
                        Console.WriteLine("Ocena została dodana pomyślnie.");

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
                        Console.WriteLine("Niepoprawna ocena. Ocena musi być w zakresie 1-10.");
                    }
                }
                else
                {
                    Console.WriteLine("Nieporawny wybór, spróbuj ponownie.");
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
                        if (parts.Length >= 3)
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