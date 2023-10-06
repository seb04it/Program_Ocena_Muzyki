namespace Program_Ocena_Muzyki
{
    public class Program
    {
        private static ISong program;

        private static void Main(string[] args)
        {
            PreMenu();
            Menu();
        }

        static void PreMenu()
        {
            while (true)
            {
                Console.Write("\nWybierz w jaki sposób chcesz korzystać z programu: \n" +
                   "1. InMemory\n" +
                   "2. InFile (Zalecane z uwagi na możliwość zapisu danych)\n" +
                   "Q. Wyjdź\n" +
                   "Wybrano: ");

                var choice = Console.ReadLine().ToUpper();
                switch (choice)
                {
                    case "1":
                        program = new SongInMemory();
                        return;
                    case "2":
                        program = new SongInFile();
                        return;
                    case "Q":
                        Console.WriteLine("Dziękuję za skorzystanie z programu!");
                        return;
                    default:
                        Console.WriteLine("Proszę wybrać jedną z powyższych opcji.\n" +
                            "Wybrano: ");
                        continue;
                }
            }
        }
        static void Menu()
        {
            Console.WriteLine("\n===========================================================================\n" +
                "Witaj użytkowniku w programie do zapisu i oceny twojej muzyki!\n"
               + "===========================================================================");
            while (true)
            {
                Console.Write("\nWybierz jedną opcję z poniższych: \n"
                       + "1. Dodaj nową piosenkę\n"
                       + "2. Usuń piosenkę\n"
                       + "3. Oceń piosenki\n"
                       + "4. Wyświetl statystyki\n"
                       + "Q. Wyjdź\n" +
                       "Wybrano: ");

                var choice = Console.ReadLine().ToUpper();
                switch (choice)
                {
                    case "1":
                        program.AddSong();
                        break;
                    case "2":
                        program.DeleteSong();
                        break;
                    case "3":
                        program.RateSongs();
                        break;
                    case "4":
                        program.GetSongsStatistics();
                        break;
                    case "Q":
                        Console.WriteLine("Dziękuję za skorzystanie z programu!");
                        return;
                    default:
                        Console.WriteLine("Proszę wybrać jedną z powyższych opcji.\n" +
                            "Wybrano: ");
                        continue;
                }
            }
        }
    }
}