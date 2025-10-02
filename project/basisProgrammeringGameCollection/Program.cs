namespace basisProgrammeringGameCollection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Title
            bool isPlaying = true;
            
            while (isPlaying)
            {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  ________                        _________        .__  .__                 __  .__               \r\n /  _____/_____    _____   ____   \\_   ___ \\  ____ |  | |  |   ____   _____/  |_|__| ____   ____  \r\n/   \\  ___\\__  \\  /     \\_/ __ \\  /    \\  \\/ /  _ \\|  | |  | _/ __ \\_/ ___\\   __\\  |/  _ \\ /    \\ \r\n\\    \\_\\  \\/ __ \\|  Y Y  \\  ___/  \\     \\___(  <_> )  |_|  |_\\  ___/\\  \\___|  | |  (  <_> )   |  \\\r\n \\______  (____  /__|_|  /\\___  >  \\______  /\\____/|____/____/\\___  >\\___  >__| |__|\\____/|___|  /\r\n        \\/     \\/      \\/     \\/          \\/                      \\/     \\/                    \\/ ");

            Console.WriteLine();
            
            Console.WriteLine("(1) MasterMind");
            Console.WriteLine("(2) Sænke Slagskib");
            Console.WriteLine("(3) Mine Sweeper");
            Console.WriteLine("(4) Quit");

            Console.ForegroundColor = ConsoleColor.White;

            string pickedGame = Console.ReadLine();

            switch (pickedGame)
                {
                case "1":
                        Console.WriteLine("1");
                    break;

                case "2":
                        Console.WriteLine("2");
                    break;

                case "3":
                        Console.WriteLine("3");
                    break;

                case "4":
                        Console.WriteLine("Error");
                    return;
                        
                }

            }
                
        }
    }
}
