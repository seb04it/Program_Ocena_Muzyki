using Program_Ocena_Muzyki;

namespace MusicRatingApp
{
    class Program
    {
        public const string fileName = "songs.txt";

        static List<Song> songs = new List<Song>();

        private static void Main(string[] args)
        {
            LoadSongs();
            ShowMenu();
        }

        static void ShowMenu()
        {

            Console.WriteLine("Witaj użytkowniku w programie do zapisu i oceny twojej muzyki!\n"
                + "===========================================================================");

            while (true)
            {
                Console.Write("\nWybierz jedną opcję z poniższych\n"
                    + "1. Dodaj nową piosenkę\n"
                    + "2. Usuń piosenkę\n"
                    + "3. Oceń piosenki\n"
                    + "4. Wyświetl podsumowanie\n"
                    + "Q. Wyjdź (Zapisz plik\n");
                Console.Write("Wybrano: ");

                var choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case "1":
                        AddSong();
                        break;
                    case "2":
                        DeleteSong();
                        break;
                    case "3":
                        RateSongs();
                        break;
                    case "4":
                        DisplaySummary();
                        break;
                    case "Q":
                        SaveSongs();
                        return;
                    default:
                        Console.WriteLine("Niepoprawny wybór. Spróbuj ponownie\n");
                        continue;
                }
            }
        }

        static void AddSong()
        {
            Console.Write("Podaj tytuł piosenki: ");
            string title = Console.ReadLine();
            Console.Write("Podaj artystę: ");
            string artist = Console.ReadLine();
            if(!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(artist))
            {
                songs.Add(new Song { Title = title, Artist = artist, Rating = 0 });
            }
            else
            {
                Console.WriteLine("Piosenka musi posiadać tytuł oraz artystę");
            }
        }

        static void DeleteSong()
        {

            if (songs.Count == 0)
            {
                Console.WriteLine("Nie ma żadnych dostępnych piosenek do usunięcia.");
                return;
            }

            Console.WriteLine("\nLista piosenek:");

            for (int i = 0; i < songs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Tytuł: {songs[i].Title} Artysta: {songs[i].Artist} (Ocena: {songs[i].Rating})");
            }

            Console.Write("Wybierz numer piosenki do usunięcia (lub 0 aby zakończyć): ");
            var input = Convert.ToInt32(Console.ReadLine());

            if (input == 0)
            {
                Console.WriteLine("Usuwanie zakończone.");
                return;
            }
            else if (input >= 1 && input <= songs.Count)
            {
                string deletedSongTitle = songs[input - 1].Title;
                songs.RemoveAt(input - 1);
                Console.WriteLine($"Piosenka '{deletedSongTitle}' została usunięta.");
            }
            else
            {
                Console.WriteLine("Niepoprawny wybór piosenki.");
            }

        }

        static void RateSongs()
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
                    Console.WriteLine($"{i + 1}. Tytuł: {songs[i].Title} Artysta: {songs[i].Artist} (Ocena: {songs[i].Rating})");
                }

                Console.Write("Wybierz numer piosenki do oceny (lub 0 aby zakończyć): ");
                
                var input = Convert.ToInt32(Console.ReadLine());

                if (input == 0)
                {
                    Console.WriteLine("Ocenianie zakończone.");
                    return;
                }

                if (input >= 1 && input <= songs.Count)
                {
                    Console.Write($"Ocena dla '{songs[input - 1].Title}': ");
                    int rating;

                    while (true)
                    {
                        if (int.TryParse(Console.ReadLine(), out rating) && rating >= 1 && rating <= 10)
                        {
                            songs[input - 1].Rating = rating;
                            Console.WriteLine($"Ocena '{songs[input - 1].Title}' zaktualizowana.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Niepoprawna ocena. Wprowadź liczbę od 1 do 10.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Niepoprawny wybór piosenki.");
                }
            }
        }

        static void DisplaySummary()
        {
            if (songs.Count == 0)
            {
                Console.WriteLine("Nie ma żadnych dostępnych piosenek.");
                return;
            }

            int maxRating = -1;
            int minRating = 11;
            int sumRating = 0;

            foreach (var song in songs)
            {
                sumRating += song.Rating;
                maxRating = Math.Max(maxRating, song.Rating);
                minRating = Math.Min(minRating, song.Rating);
            }

            double averageRating = sumRating / songs.Count;

            Console.WriteLine($"\nNajwyżej oceniona piosenka: {FindSongByRating(maxRating)} (ocena: {maxRating})");
            Console.WriteLine($"Najniżej oceniona piosenka: {FindSongByRating(minRating)} (ocena: {minRating})");
            Console.WriteLine($"Średnia ocena wszystkich piosenek: {averageRating:F2}");
        }

        static string FindSongByRating(int rating)
        {
            foreach (var song in songs)
            {
                if (song.Rating == rating)
                    return song.Title;
            }
            return "Nie znaleziono";
        }

        static void SaveSongs()
        {
            using (var writer = File.CreateText(fileName))
            {
                foreach (var song in songs)
                {
                    writer.WriteLine($"{song.Title};{song.Artist};{song.Rating}");
                }
            }
        }

        static void LoadSongs()
        {
            if (File.Exists(fileName))
            {
                using (var reader = File.OpenText(fileName))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        string[] parts = line.Split(';');
                        songs.Add(new Song { Title = parts[0], Artist = parts[1], Rating = int.Parse(parts[2]) });
                        line = reader.ReadLine();
                    }
                }
            }
        }
    }
}
