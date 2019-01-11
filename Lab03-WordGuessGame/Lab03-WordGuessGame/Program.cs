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

            //boolean to check want run the home page and all the other operations
            bool run = true;
            while (run)
            {
                HomePage();
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":

                        string randomWord = RandomlyGetOneWord(path);
                        Console.WriteLine(randomWord);
                        UserGuessWord(randomWord);

                        //chooose start a new game or back to home page

                        Console.WriteLine("Do you want to start a new game? y/n");
                        string input2 = Console.ReadLine();
                        String ans = input2.ToLower();
                        if (ans == "y")
                        {
                            goto case "1";
                        }
                        else
                        {
                            run = true;
                        }
                        break;

                    case "2":

                    case "3":
                        Environment.Exit(0);
                        break;


                }

                //View words in the external file, 
                //add a word to the external file, 
                //Remove words from a text file, 
                //exit the game, 
                //start a new game

            }

          
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

        public static void HomePage()
        {
            Console.WriteLine("*******************************");
            Console.WriteLine("*******************************");
            Console.WriteLine("*******************************");
            Console.WriteLine("******Guess**A***Word**********");
            Console.WriteLine("*******************************");
            Console.WriteLine("*******************************");
            Console.WriteLine("*******************************");

            Console.WriteLine(" ");
            Console.WriteLine("1:Start a Game");
            Console.WriteLine("2:Admin");
            Console.WriteLine("3:Exit");
            Console.WriteLine("*******************************");

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
