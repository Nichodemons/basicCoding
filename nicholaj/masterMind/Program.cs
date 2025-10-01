using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace masterMind
{
    internal class Program
    {
        static void Main(string[] args)
        {
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

            int[] arrayGenerated = new int[] {rndNumber1, rndNumber2, rndNumber3, rndNumber4};

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
            }
        }
    }
