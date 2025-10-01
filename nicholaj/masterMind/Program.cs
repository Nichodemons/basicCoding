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
            Console.WriteLine("The Computer will generate 4 random numbers between 1 - 2");
            Console.WriteLine("Numbers can repeat. An example: 2-2-1-2");
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
            Console.WriteLine("Make a guess, or type q to quit");

                while (true)
                {
                string playerGuess = Console.ReadLine();

                int result = 0;

                    //Look if what player typed is invalid
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
                }
                else
                {
                        Console.WriteLine("Guess Again, or Type Q to quit");
                        result = 0;
                }

                if (playerGuess == "q")
                {
                        isPlaying = false;
                        break;
                    }
                }
            }
            }
        }
    }
