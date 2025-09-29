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
            int[] arrayToGuess = new int[4];

            while (isPlaying)
            {

            for (int i = 0; i < arrayToGuess.Length; i++)
            {
                int rndNumber = new Random().Next(1, 4);
                arrayToGuess[i] = rndNumber;
                Console.Write(rndNumber);
            }
            

            
            }
        }
    }
}
