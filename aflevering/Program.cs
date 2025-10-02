namespace basisProgrammeringGameCollection
{
    internal class Program
    {
        const int Size = 10;
        const char Water = '~', Ship = 'S', Hit = 'X', Miss = 'O';
        static char[,] playerBoard = new char[Size, Size];
        static char[,] computerBoard = new char[Size, Size];
        static bool[,] computerShips = new bool[Size, Size];

        static string[] shipNames = { "Carrier", "Battleship", "Cruiser", "Submarine", "Destroyer" };
        static int[] shipSizes = { 5, 4, 3, 3, 2 };

        static int totalShipCells = 0;
        static int playerHits = 0;
        static int computerHits = 0;
        static Random rand = new Random();
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

                        //Main Loop
                        while (isPlaying)
                        {
                            //Generate numbers for array
                            int rndNumber1 = new Random().Next(1, 4);
                            int rndNumber2 = new Random().Next(1, 4);
                            int rndNumber3 = new Random().Next(1, 4);
                            int rndNumber4 = new Random().Next(1, 4);

                            int[] arrayGenerated = new int[] { rndNumber1, rndNumber2, rndNumber3, rndNumber4 };

                            //convert to string
                            string arrayConvertToString = String.Join("", arrayGenerated.Select(p => p.ToString()).ToArray());

                            Console.WriteLine();
                            Console.WriteLine("Make a guess, or type quit to quit");

                            int playerTries = 0;

                            //Loop for guessing
                            while (true)
                            {
                                string playerGuess = Console.ReadLine();
                                playerTries = playerTries + 1;
                                int result = 0;

                                //Look if what player typed is invalid
                                if (playerGuess.Length == 4)
                                {
                                    //Check each number
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
                                    
                                    //If guessed correctly
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
                                
                                //If answer is invalid
                                else
                                {
                                    Console.WriteLine("Invalid Answer, type again");
                                    continue;
                                }

                                //Quit if player typed quit
                                if (playerGuess == "quit")
                                {
                                    isPlaying = false;
                                    break;
                                }
                            }
                        }
                        break;

                case "2":
                        
                            // Initialize boards
                            InitBoard(playerBoard);
                            InitBoard(computerBoard);

                            DrawBanner("WELCOME TO BATTLESHIP");
                            Console.WriteLine("Place your ships:");
                            PlaceShips(playerBoard, manual: true);   // Player places ships
                            Console.WriteLine("\nComputer is placing ships...");
                            PlaceShips(computerBoard, manual: false); // Computer places ships randomly

                            // Main game loop
                            while (playerHits < totalShipCells && computerHits < totalShipCells)
                            {
                                // Player's turn
                                DrawBanner("PLAYER'S TURN");
                                Console.WriteLine("\nYour Board:");
                                PrintBoard(playerBoard, showShips: true);
                                Console.WriteLine("\nEnemy Board:");
                                PrintBoard(computerBoard, showShips: false);

                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write("Enter attack coordinates (row col): ");
                                Console.ResetColor();
                                var input = Console.ReadLine().Split();
                                int row = int.Parse(input[0]);
                                int col = int.Parse(input[1]);

                                if (Attack(computerBoard, row, col, ref playerHits))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(">>> HIT! <<<");
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("Miss.");
                                }
                                Console.ResetColor();

                                // Computer's turn
                                DrawBanner("COMPUTER'S TURN");
                                int r, c;
                                do
                                {
                                    r = rand.Next(Size);
                                    c = rand.Next(Size);
                                } while (playerBoard[r, c] == Hit || playerBoard[r, c] == Miss);

                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"Computer attacks ({r},{c})");
                                Console.ResetColor();

                                if (Attack(playerBoard, r, c, ref computerHits))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(">>> COMPUTER HIT YOUR SHIP! <<<");
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("Computer missed.");
                                }
                                Console.ResetColor();
                            }

                            // End of game
                            DrawBanner(playerHits == totalShipCells ? "YOU WIN!" : "COMPUTER WINS!");
                            Console.WriteLine("\nFinal Boards:");
                            Console.WriteLine("\nYour Board:");
                            PrintBoard(playerBoard, showShips: true);
                            Console.WriteLine("\nEnemy Board:");
                            PrintBoard(computerBoard, showShips: true);

                        return;

                case "3":
                        Console.WriteLine("3");
                    break;

                case "4":
                        Console.WriteLine("Error");
                    return;
                        
                }

            }
                
        }
        static void InitBoard(char[,] board)
        {
            // Fill board with water
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    board[i, j] = Water;
        }

        static void PrintBoard(char[,] board, bool showShips)
        {
            // Print column headers
            Console.Write("   ");
            for (int i = 0; i < Size; i++)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(i + " ");
                Console.ResetColor();
            }
            Console.WriteLine("\n  +" + new string('-', Size * 2) + "+");

            // Print rows
            for (int i = 0; i < Size; i++)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(i + " ");
                Console.ResetColor();
                Console.Write("| ");
                for (int j = 0; j < Size; j++)
                {
                    char cell = board[i, j];
                    if (!showShips && cell == Ship) cell = Water; // Hide enemy ships
                    PrintColoredCell(cell);
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("  +" + new string('-', Size * 2) + "+");
        }

        static void PrintColoredCell(char cell)
        {
            // Color-coded cells
            switch (cell)
            {
                case Hit:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("X ");
                    break;
                case Miss:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("O ");
                    break;
                case Ship:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("S ");
                    break;
                case Water:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("~ ");
                    break;
                default:
                    Console.Write(cell + " ");
                    break;
            }
            Console.ResetColor();
        }

        static void DrawBanner(string title)
        {
            // Highlighted banner for turns and results
            string line = new string('=', title.Length + 8);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n" + line);
            Console.WriteLine("   " + title);
            Console.WriteLine(line);
            Console.ResetColor();
        }

        static void PlaceShips(char[,] board, bool manual)
        {
            if (manual)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nGrid size is {Size}×{Size}. Valid rows and columns are from 0 to {Size - 1}.");
                Console.WriteLine("You'll be asked to place each ship by choosing a starting coordinate and direction.");
                Console.ResetColor();
            }

            // Place each ship
            for (int s = 0; s < shipNames.Length; s++)
            {
                string name = shipNames[s];
                int size = shipSizes[s];
                bool placed = false;
                while (!placed)
                {
                    int row = manual ? Ask($"Row for {name} ({size} cells)") : rand.Next(Size);
                    int col = manual ? Ask($"Col for {name} ({size} cells)") : rand.Next(Size);
                    bool horiz = manual ? Ask("Horizontal? (1=yes, 0=no)") == 1 : rand.Next(2) == 0;

                    if (CanPlace(board, row, col, size, horiz))
                    {
                        for (int i = 0; i < size; i++)
                        {
                            int r = row + (horiz ? 0 : i);
                            int c = col + (horiz ? i : 0);
                            board[r, c] = Ship;
                            if (board == computerBoard) computerShips[r, c] = true;
                        }
                        totalShipCells += size;
                        placed = true;
                    }
                    else if (manual)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid placement. Ship would go out of bounds or overlap another. Try again.");
                        Console.ResetColor();
                    }
                }
            }
        }

        static bool CanPlace(char[,] board, int row, int col, int size, bool horiz)
        {
            // Check if ship fits and does not overlap
            if (horiz && col + size > Size) return false;
            if (!horiz && row + size > Size) return false;
            for (int i = 0; i < size; i++)
            {
                int r = row + (horiz ? 0 : i);
                int c = col + (horiz ? i : 0);
                if (board[r, c] != Water) return false;
            }
            return true;
        }

        static bool Attack(char[,] board, int row, int col, ref int hits)
        {
            // Handle attack result
            if (board[row, col] == Ship)
            {
                board[row, col] = Hit;
                hits++;
                return true;
            }
            else if (board[row, col] == Water)
            {
                board[row, col] = Miss;
            }
            return false;
        }

        static int Ask(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(prompt + ": ");
            Console.ResetColor();
            return int.Parse(Console.ReadLine());
        }
    }

}
