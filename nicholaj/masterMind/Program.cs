using System.ComponentModel.Design;
using System.Security.Cryptography;

namespace masterMind
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello and welcome to MasterMind");
            Console.WriteLine("The Computer will generate 4 random numbers between 1 - 2");
            Console.WriteLine("Numbers can repeat. An example: 2-2-1-2");
            bool isPlaying = true;
            int rndNumber1 = new Random().Next(1, 4);
            int rndNumber2 = new Random().Next(1, 4);
            int rndNumber3 = new Random().Next(1, 4);
            int rndNumber4 = new Random().Next(1, 4);

            int[] arrayGenerated = new int[] {rndNumber1, rndNumber2, rndNumber3, rndNumber4};

            //write out array for testing purposes
            for (int i = 0; i < arrayGenerated.Length; i++)
            {
                Console.Write(arrayGenerated[i]);
            }
            Console.WriteLine();

            while (isPlaying)
            {
                string playerGuess = Console.ReadLine();
                //int[] playerConvertedArray[] = int.Parse(playerGuess);

                int result = 0;

                //Problem ligger i at playerGuess er en string a chars som alle har et nummer bag sig som den kigger på. Bliver nødt til at splitte array og lave til int
                for (int i = 0; i < arrayGenerated.Length; i++)
                {
                    Console.WriteLine((int)playerGuess[i]);
                    if ( (int)playerGuess[i] == arrayGenerated[i])
                    {
                        result = +1;
                    }
                    else
                    {
                        result = result;
                    }
                        }
                    Console.WriteLine("you have " + result + " correct");
                    result = 0;
                    Console.WriteLine("type q to quit or anything else to continue");

                    if (Console.ReadLine() == "q")
                    {
                        isPlaying = false;
                    }
                    else
                    {
                        Console.WriteLine("Guess again");
                    }

                }
                Console.ReadLine();
            }
        }
    }
