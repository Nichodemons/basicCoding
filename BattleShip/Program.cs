using System;

class Battleship
{
    const int Size = 10;
    const char Water = '~', Ship = 'S', Hit = 'X', Miss = 'O';
    char[,] playerBoard = new char[Size, Size];
    char[,] computerBoard = new char[Size, Size];
    bool[,] computerShips = new bool[Size, Size];

    // Ship definitions (names and sizes)
    string[] shipNames = { "Carrier", "Battleship", "Cruiser", "Submarine", "Destroyer" };
    int[] shipSizes = { 5, 4, 3, 3, 2 };

    int totalShipCells = 0;
    int playerHits = 0;
    int computerHits = 0;
    Random rand = new Random();

    static void Main() => new Battleship().Start();

    void Start()
    {
        InitBoard(playerBoard);
        InitBoard(computerBoard);
        DrawBanner("WELCOME TO BATTLESHIP");
        Console.WriteLine("Place your ships:");
        PlaceShips(playerBoard, manual: true);
        Console.WriteLine("\nComputer is placing ships...");
        PlaceShips(computerBoard, manual: false);

        while (playerHits < totalShipCells && computerHits < totalShipCells)
        {
            DrawBanner("PLAYER'S TURN");
            Console.WriteLine("\nYour Board:");
            PrintBoard(playerBoard, showShips: true);
            Console.WriteLine("\nEnemy Board:");
            PrintBoard(computerBoard, showShips: false);

            Console.Write("\u001b[36mEnter attack coordinates (row col): \u001b[0m");
            var input = Console.ReadLine().Split();
            int row = int.Parse(input[0]);
            int col = int.Parse(input[1]);

            if (Attack(computerBoard, row, col, ref playerHits))
                Console.WriteLine("\u001b[5;31mHit!\u001b[0m");
            else
                Console.WriteLine("\u001b[34mMiss.\u001b[0m");

            DrawBanner("COMPUTER'S TURN");
            int r, c;
            do
            {
                r = rand.Next(Size);
                c = rand.Next(Size);
            } while (playerBoard[r, c] == Hit || playerBoard[r, c] == Miss);

            Console.WriteLine($"\u001b[36mComputer attacks ({r},{c})\u001b[0m");
            if (Attack(playerBoard, r, c, ref computerHits))
                Console.WriteLine("\u001b[5;31mComputer hit your ship!\u001b[0m");
            else
                Console.WriteLine("\u001b[34mComputer missed.\u001b[0m");
        }

        DrawBanner(playerHits == totalShipCells ? "🎉 YOU WIN! 🎉" : "💥 COMPUTER WINS 💥");
        Console.WriteLine("\nFinal Boards:");
        Console.WriteLine("\nYour Board:");
        PrintBoard(playerBoard, showShips: true);
        Console.WriteLine("\nEnemy Board:");
        PrintBoard(computerBoard, showShips: true);
    }

    void InitBoard(char[,] board)
    {
        for (int i = 0; i < Size; i++)
            for (int j = 0; j < Size; j++)
                board[i, j] = Water;
    }

    void PrintBoard(char[,] board, bool showShips)
    {
        Console.Write("   ");
        for (int i = 0; i < Size; i++) Console.Write($"\u001b[35m{i}\u001b[0m ");
        Console.WriteLine("\n  +" + new string('-', Size * 2) + "+");
        for (int i = 0; i < Size; i++)
        {
            Console.Write($"\u001b[35m{i}\u001b[0m | ");
            for (int j = 0; j < Size; j++)
            {
                char cell = board[i, j];
                if (!showShips && cell == Ship) cell = Water;
                Console.Write(Colorize(cell));
            }
            Console.WriteLine("|");
        }
        Console.WriteLine("  +" + new string('-', Size * 2) + "+");
    }

    string Colorize(char cell)
    {
        return cell switch
        {
            Hit => "\u001b[5;41;30mX\u001b[0m ",    // Flashing red background
            Miss => "\u001b[44;37mO\u001b[0m ",     // Blue background
            Ship => "\u001b[42;30mS\u001b[0m ",     // Green background
            Water => "\u001b[47;30m~\u001b[0m ",    // Light gray background
            _ => $" {cell} "
        };
    }

    void DrawBanner(string title)
    {
        string line = new string('=', title.Length + 8);
        Console.WriteLine($"\n\u001b[43;30m{line}\u001b[0m");
        Console.WriteLine($"\u001b[43;30m   {title}   \u001b[0m");
        Console.WriteLine($"\u001b[43;30m{line}\u001b[0m");
    }

    void PlaceShips(char[,] board, bool manual)
    {
        if (manual)
        {
            Console.WriteLine($"\n\u001b[33mGrid size is {Size}×{Size}. Valid rows and columns are from 0 to {Size - 1}.\u001b[0m");
            Console.WriteLine("\u001b[33mYou'll be asked to place each ship by choosing a starting coordinate and direction.\u001b[0m");
        }

        for (int s = 0; s < shipNames.Length; s++)
        {
            string name = shipNames[s];
            int size = shipSizes[s];
            bool placed = false;
            while (!placed)
            {
                int row = manual ? Ask($"\u001b[36mRow for {name} ({size} cells)\u001b[0m") : rand.Next(Size);
                int col = manual ? Ask($"\u001b[36mCol for {name} ({size} cells)\u001b[0m") : rand.Next(Size);
                bool horiz = manual ? Ask("\u001b[36mHorizontal? (1=yes, 0=no)\u001b[0m") == 1 : rand.Next(2) == 0;

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
                    Console.WriteLine("\u001b[31mInvalid placement. Ship would go out of bounds or overlap another. Try again.\u001b[0m");
            }
        }
    }

    bool CanPlace(char[,] board, int row, int col, int size, bool horiz)
    {
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
        Console.Write($"{prompt}: ");
        return int.Parse(Console.ReadLine());
    }
}
