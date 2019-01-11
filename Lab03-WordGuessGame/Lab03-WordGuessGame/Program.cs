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
                try
                {
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            Console.Clear();
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
                                Console.Clear();
                                run = true;
                            }
                            break;

                        case "2":
                            Console.Clear();
                            Admin();
                            string input3 = Console.ReadLine();
                            if (input3 == "1")
                            {
                                string[] lines = File.ReadAllLines(path);
                                Console.Clear();
                                for (int i = 0; i < lines.Length; i++)
                                {
                                    Console.WriteLine(lines[i]);
                                }
                                Console.WriteLine("*************");
                                Console.WriteLine("press any key to go back");
                                Console.WriteLine("*************");
                                Console.ReadLine();
                                goto case "2";

                            }
                            else if (input3 == "2")
                            {
                                Console.Clear();
                                Console.WriteLine("what word you want to add?");
                                string word = Console.ReadLine();
                                using (StreamWriter streamWriter = File.AppendText(path))
                                {
                                    streamWriter.WriteLine(word);
                                }
                                Console.WriteLine("*************");
                                Console.WriteLine("You have successfully added that");
                                Console.WriteLine("press any key to go back");
                                Console.WriteLine("*************");
                                Console.ReadLine();
                                goto case "2";
                            }
                            else if (input3 == "3")
                            {
                                Console.Clear();
                                Console.WriteLine("what word you want to delete?");
                                string word = Console.ReadLine();

                                string tempFile = Path.GetTempFileName();

                                using (var sr = new StreamReader(path))
                                {
                                    using (var sw = new StreamWriter(tempFile))
                                    {
                                        string line;

                                        while ((line = sr.ReadLine()) != null)
                                        {
                                            if (line != word)
                                                sw.WriteLine(line);
                                        }
                                    }
                                }

                                File.Delete(path);
                                File.Move(tempFile, path);
                                Console.WriteLine("*************");
                                Console.WriteLine("You have successfully deleted that");
                                Console.WriteLine("press any key to go back");
                                Console.WriteLine("*************");
                                Console.ReadLine();
                                goto case "2";
                            }
                            else if (input3 == "4")
                            {
                                Console.Clear();
                                run = true;
                            }
                            break;

                        case "3":
                            Environment.Exit(0);
                            break;

                        default:
                           // Console.Clear();
                            throw new Exception("please choose one from here");

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
              
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

        //create home page

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
                if (input.Length != 1)
                {
                    Console.WriteLine("please press one letter");
                }

       
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
        //create Admin page with 4 choices
        public static void Admin()
        {
            Console.WriteLine("Press a number:");
            Console.WriteLine("1:View the Words");
            Console.WriteLine("2:Add a New Word");
            Console.WriteLine("3:Delete a Word");
            Console.WriteLine("4:Go back to Home page");
        }
 
    }
}
