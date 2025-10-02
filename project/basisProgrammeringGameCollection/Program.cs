namespace basisProgrammeringGameCollection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Title
            bool playerIsPlaying = true;
            
            while (playerIsPlaying)
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
                    //MasterMind Game
                    case "1":
                        Console.WriteLine("Hello and welcome to MasterMind");
                        Console.WriteLine("The Computer will generate 4 random numbers between 1 - 3");
                        Console.WriteLine("Numbers can repeat. An example: 2312");
                        bool isPlaying = true;

                        while (isPlaying)
                        {
                            int rndNumber1 = new Random().Next(1, 4);
                            int rndNumber2 = new Random().Next(1, 4);
                            int rndNumber3 = new Random().Next(1, 4);
                            int rndNumber4 = new Random().Next(1, 4);

                            int[] arrayGenerated = new int[] { rndNumber1, rndNumber2, rndNumber3, rndNumber4 };

                            //convert to string
                            string arrayConvertToString = String.Join("", arrayGenerated.Select(p => p.ToString()).ToArray());

                            //write out array for testing purposes
                            //for (int i = 0; i < arrayGenerated.Length; i++)
                            //{
                            //    Console.Write(arrayGenerated[i]);
                            //}

                            Console.WriteLine();
                            Console.WriteLine("Make a guess, or type quit to quit");

                            int playerTries = 0;
                            while (true)
                            {
                                string playerGuess = Console.ReadLine();
                                playerTries = playerTries + 1;
                                int result = 0;

                                //Look if what player typed is invalid
                                if (playerGuess.Length == 4)
                                {
                                    for (int i = 0; i < arrayGenerated.Length; i++)
                                    {
                                        if ((int)playerGuess[i] == arrayConvertToString[i])
                                        {
                                            result = result + 1;
                                        }
                                        else
                                        {
                                            result = result;
                                        }
                                    }
                                    Console.WriteLine("you have " + result + " correct");
                                    if (result == 4)
                                    {
                                        Console.WriteLine("You win!");
                                        Console.WriteLine("You used " + playerTries + " tries");
                                        playerTries = 0;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Guess Again, or Type quit to quit");
                                        result = 0;
                                    }

                                }
                                else
                                {
                                    Console.WriteLine("Invalid Answer, type again");
                                    continue;
                                }

                                if (playerGuess == "quit")
                                {
                                    isPlaying = false;
                                    break;
                                }
                            }
                        }
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
