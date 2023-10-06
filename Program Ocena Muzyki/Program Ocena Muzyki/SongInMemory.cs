namespace Program_Ocena_Muzyki
{
    public class SongInMemory : SongBase
    {
        public List<Song> songs = new List<Song>();

        public override void AddSong()
        {
            Console.Write("Podaj tytuł piosenki: ");
            string title = Console.ReadLine();
            Console.Write("Podaj artystę: ");
            string author = Console.ReadLine();
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(author))
            {
                songs.Add(new Song { Title = title, Author = author, Rating = 0 });
            }
            else
            {
                Console.WriteLine("Piosenka musi posiadać tytuł oraz artystę");
            }
        }

        public override void DeleteSong()
        {
            if (songs.Count == 0)
            {
                Console.WriteLine("Nie ma żadnych dostępnych piosenek do usunięcia.");
                return;
            }

            Console.WriteLine("\nLista piosenek:");

            for (int i = 0; i < songs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Tytuł: {songs[i].Title} Artysta: {songs[i].Author} (Ocena: {songs[i].Rating})");
            }

            Console.Write("Wybierz numer piosenki do usunięcia (lub 'Q' aby zakończyć): ");
            var input = Console.ReadLine();

            if (input == "Q")
            {
                Console.WriteLine("Usuwanie zakończone.");
                return;
            }
            else if (int.TryParse(input, out int songInput) && songInput >= 1 && songInput <= songs.Count)
            {
                string deletedSongTitle = songs[songInput - 1].Title;
                songs.RemoveAt(songInput - 1);
                Console.WriteLine($"Piosenka '{deletedSongTitle}' została usunięta.");
            }
            else
            {
                Console.WriteLine("Niepoprawny wybór piosenki.");
            }
        }

        public override void GetSongsStatistics()
        {
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
            foreach (var song in songs)
            {
                if (song.Rating == rating)
                    return song.Title;
            }
            return "Nie znaleziono";
        }

        public override void RateSongs()
        {
            if (songs.Count == 0)
            {
                Console.WriteLine("Nie ma żadnych dostępnych piosenek.");
                return;
            }
            while (true)
            {
                Console.WriteLine("\nLista piosenek:");

                for (int i = 0; i < songs.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. Tytuł: {songs[i].Title} Artysta: {songs[i].Author} (Ocena: {songs[i].Rating})");
                }

                Console.Write("Wybierz numer piosenki do oceny (lub 'Q' aby zakończyć): ");

                var input = Console.ReadLine().ToUpper();

                if (input == "Q")
                {
                    Console.WriteLine("Ocenianie zakończone.");
                    return;
                }
                else if (int.TryParse(input, out int songInput) && songInput >= 1 && songInput <= songs.Count)
                {
                    Console.Write($"Ocena utwór '{songs[songInput - 1].Title}' (1-10): ");
                    if (int.TryParse(Console.ReadLine(), out int rating) && rating >= 1 && rating <= 10)
                    {
                        songs[songInput - 1].Rating = rating;
                        Console.WriteLine("Ocena została dodana pomyślnie.");

                    }
                    else
                    {
                        Console.Write("Niepoprawna ocena. Wprowadź liczbę od 1 do 10: ");
                    }

                }
                else
                {
                    Console.WriteLine("Niepoprawny wybór piosenki.");
                }
            }
        }
    }
}

