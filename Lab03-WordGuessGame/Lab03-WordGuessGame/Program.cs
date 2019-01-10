using System;
using System.IO;
using System.Linq;


namespace Lab03_WordGuessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            String path = "../../../wordgame.txt";
            CreateFile(path);

            //Home Navigation

            //View words in the external file, 
            //add a word to the external file, 
            //Remove words from a text file, 
            //exit the game, 
            //start a new game


           string randomWord= RandomlyGetOneWord(path);
            Console.WriteLine(randomWord);

            UserGuessWord(randomWord);
        }


       public static void CreateFile(string path)
        {

            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                string[] wordsList = { "kitty", "flower", "sky", "hippo", "elephant", "lobster"};
                try
                {
                    for (int i= 0; i < wordsList.Length; i++) {
                        streamWriter.WriteLine($"{wordsList[i]}");
                    }
                }
                catch(Exception)
                {
                    throw;

                }
            }
            
        }

        public static string RandomlyGetOneWord(string path)
        {
            Random randomeWord = new Random();
            string[] lines = File.ReadAllLines(path);
            int wIndex = randomeWord.Next(lines.Length);
            return lines[wIndex];
        }

        public static void UserGuessWord(string randomWord)
        {
            string[] emp = new string[randomWord.Length];
            for (int i = 0; i < randomWord.Length; i++)
            {
                emp[i] = "_ ";
                Console.Write($"{emp[i]}");
            }
           
         //user starts guessing the letter

            Console.WriteLine("please enter a letter");
            
            while (emp.Contains("_ "))
            {
                string input = Console.ReadLine();
                for (int i = 0; i < randomWord.Length; i++)
                {
                    if (randomWord[i].ToString() == input)
                    {
                        int index = randomWord.IndexOf(input);

                        emp[i] = randomWord[i].ToString();

                    }
                }

                for (int i = 0; i < emp.Length; i++)
                {

                    Console.Write($"{emp[i]}");
                }

            }

            Console.WriteLine("");
            Console.WriteLine("Yeah!");
            

        }

    }
}
