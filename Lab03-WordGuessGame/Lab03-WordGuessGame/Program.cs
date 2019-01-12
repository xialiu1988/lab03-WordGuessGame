using System;
using System.IO;
using System.Linq;


namespace Lab03_WordGuessGame
{
    public class Program
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
                            //call function to get a random word from file
                            string randomWord = RandomlyGetOneWord(path);
                           // Console.WriteLine(randomWord);
                            //call game
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
                            //display the admin page
                            Admin();
                            string input3 = Console.ReadLine();
                            if (input3 == "1")
                            {
                                bool read=ViewFile(path);
                                if (read)
                                {
                                    Console.WriteLine("*************");
                                    Console.WriteLine("press any key to go back");
                                    Console.WriteLine("*************");
                                    Console.ReadLine();
                                    goto case "2";
                                }
                            }
                            else if (input3 == "2")
                            {
                             bool word= AddWord(path);
                                if (word==true)
                                {
                                    Console.WriteLine("*************");
                                    Console.WriteLine($"You have successfully added it");
                                    Console.WriteLine("press any key to go back");
                                    Console.WriteLine("*************");
                                    Console.ReadLine();
                                    goto case "2";
                                }
                                else
                                {
                                    Console.WriteLine("*************");
                                    Console.WriteLine("Nothing hase been added");
                                    Console.WriteLine("press any key to go back");
                                    Console.WriteLine("*************");
                                    Console.ReadLine();
                                    goto case "2";
                                }
                            }
                            else if (input3 == "3")
                            {
                                string word=DeleteWord(path);
                                Console.WriteLine("*************");
                                Console.WriteLine($"{word} is gone now.");
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
                            else
                            {      
                                goto case "2";
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
        /// <summary>
        /// create a file withe words in it
        /// </summary>
        /// <param name="path"></param>

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

        //create home page,output at the very beginning

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



        /// <summary>
        /// randomly choose one word from the file 
        /// </summary>
        /// <param name="path"></param>
        /// <returns>the random word</returns>

        public static string RandomlyGetOneWord(string path)
        {
            Random randomeWord = new Random();
            string[] lines = File.ReadAllLines(path);
            int wIndex = randomeWord.Next(lines.Length);
            return lines[wIndex];
        }
        /// <summary>
        /// game processing
        /// </summary>
        /// <param name="randomWord"></param>
        public static void UserGuessWord(string randomWord)
        {
            String pathTwo = "../../../guesslog.txt";
            File.Create(pathTwo).Dispose();
            string[] emp = new string[randomWord.Length];
         
            for (int i = 0; i < randomWord.Length; i++)
            {
                emp[i] = "_ ";
                Console.Write($"{emp[i]}");
            }
           
         //user starts guessing the letter

            Console.WriteLine("please enter a letter");

            //if it still has "_ " continue the game,if not show complete the game
            while (emp.Contains("_ "))
            {
                string input = Console.ReadLine();
                if (input.Length != 1)
                {
                    Console.WriteLine("please press one letter");
                }

       //go through the random word compare eaach letter with the input,if they match ,replace "_ " with the letter
                for (int i = 0; i < randomWord.Length; i++)
                {
                    if (randomWord[i].ToString() == input)
                    {
                        int index = randomWord.IndexOf(input);

                        emp[i] = randomWord[i].ToString();

                    }
                }

                using (StreamWriter streamWriter = File.AppendText(pathTwo))
                {
                    streamWriter.WriteLine(input);
                }
                //now output the updated string array should has some letther with some "_ "

                for (int i = 0; i < emp.Length; i++)
                {
                    Console.Write($"{emp[i]}");
                }
                Console.Write("    ");
                //read the guess log
                string[] lines = File.ReadAllLines(pathTwo);
                Console.Write("Your have guessed these letters:");
                for (int i = 0; i < lines.Length; i++)
                {
                    Console.Write(lines[i]);
                }

            }

            Console.WriteLine("       ");
            Console.WriteLine("       ");
            Console.WriteLine("       ");
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
       

        /// <summary>
        /// Add a word to the file
        /// </summary>
        /// <param name="path"></param>
        /// <returns>true if successfully added</returns>
        public static bool AddWord(string path)
        {
            Console.Clear();
            Console.WriteLine("what word you want to add?");
            string word = Console.ReadLine();
        
            bool isDigitPresent = word.Any(c => char.IsDigit(c));
            if (isDigitPresent==true||word.Length==0)
            {
                Console.WriteLine("This is not a word.");
                return false;
            }
            else
            {
                using (StreamWriter streamWriter = File.AppendText(path))
                {
                    streamWriter.WriteLine(word);
                }
                return true;
            }
        }
        /// <summary>
        /// pass in the file path and delete the word user type in the console return the word user wants to delete
        /// </summary>
        /// <param name="path"></param>
        /// <returns>word</returns>
        public static string DeleteWord(string path)
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
            return word;
        }
        /// <summary>
        /// pass in the filepath and read the file output the content on the console
        /// </summary>
        /// <param name="path"></param>
        /// <returns>true</returns>

        public static bool ViewFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            Console.Clear();
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }
            return true;
        }

      
    }
}
