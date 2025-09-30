using System;

class Battleship
{
    const int Size = 10;
    const char Water = '~', Ship = 'S', Hit = 'X', Miss = 'O';
    char[,] playerBoard = new char[Size, Size];
    char[,] computerBoard = new char[Size, Size];
    bool[,] computerShips = new bool[Size, Size];

    string[] shipNames = { "Carrier", "Battleship", "Cruiser", "Submarine", "Destroyer" };
    int[] shipSizes = { 5, 4, 3, 3, 2 };

    int totalShipCells = 0;
    int playerHits = 0;
    int computerHits = 0;
    Random rand = new Random();

    static void Main() => new Battleship().Start();

    void Start()
    {
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
    }

    void InitBoard(char[,] board)
    {
        // Fill board with water
        for (int i = 0; i < Size; i++)
            for (int j = 0; j < Size; j++)
                board[i, j] = Water;
    }

    void PrintBoard(char[,] board, bool showShips)
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

    void PrintColoredCell(char cell)
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

    void DrawBanner(string title)
    {
        // Highlighted banner for turns and results
        string line = new string('=', title.Length + 8);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n" + line);
        Console.WriteLine("   " + title);
        Console.WriteLine(line);
        Console.ResetColor();
    }

    void PlaceShips(char[,] board, bool manual)
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

    bool CanPlace(char[,] board, int row, int col, int size, bool horiz)
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

    bool Attack(char[,] board, int row, int col, ref int hits)
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

    int Ask(string prompt)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(prompt + ": ");
        Console.ResetColor();
        return int.Parse(Console.ReadLine());
    }
}
