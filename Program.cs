using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;

namespace PilotTask1
{
    class Program
    {
        static string FirstPlayerName;
        static string SecondPlayerName;
        static string MainWord;
        static string CurrentWord;
        static int MainWordLength;
        static string Choise;
        static Timer timer;
        static DateTime oneMinute = new DateTime(1, 1, 1, 0, 1, 0);
        static int Y;

        /// <summary>
        /// Timer function
        /// </summary>
        static void Timer()
        {
            long interval = 1000;
            
            timer = new Timer(interval);
            //Adds an event to a timer that runs every time the time ends
            timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
            timer.Start();
        }

        /// <summary>
        /// Сountdown and timer output to console
        /// </summary>
        static void TimerElapsed(object obj, ElapsedEventArgs e)
        {
            int x1 = Console.CursorLeft;
            int y1 = Console.CursorTop;
            Console.SetCursorPosition(60, Y);
            Console.Write(oneMinute.ToString("mm:ss"));
            if (oneMinute == new DateTime(1, 1, 1, 0, 0, 0))
            {
                Console.SetCursorPosition(60, Y);
                if (String.Equals(Choise, "Рус", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("Время вышло.");
                }
                else
                    Console.WriteLine("Time is over");
                timer.Stop();
            }
            else
                oneMinute = oneMinute.AddSeconds(-1);
            Console.SetCursorPosition(x1, y1);
        }

        /// <summary>
        /// Input names of players
        /// </summary>
        static void PrintWelcomeText()
        {
            if (String.Equals(Choise, "Рус", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Игра в слова.");
                Console.WriteLine("Правила: В начале игры пользователь вводит слово определенной длины: суть игры заключается в том,\n" +
                        "чтобы 2 пользователя поочередно вводили слова, состоящие из букв первоначально указанного слова.\n" +
                        "Проигрывает тот, кто в свою очередь не вводит слово. На ввод слова 1 минута.\n");
                Console.WriteLine("Введите имя первого игрока:");
                FirstPlayerName = Console.ReadLine();
                Console.WriteLine("Введите имя второго игрока:");
                SecondPlayerName = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Играют {0} и {1}", FirstPlayerName, SecondPlayerName);
            }
            else
            {
                Console.WriteLine("Word game.");
                Console.WriteLine("Rules: At the beginning of the game, the user enters a word of a certain length: the essence of the game is \n " +
 "so that 2 users alternately enter words consisting of the letters of the originally specified word. \n" +
 "The one who in turn does not enter a word loses. On input of a word 1 minute.\n");
                Console.WriteLine("Input name the first player:");
                FirstPlayerName = Console.ReadLine();
                Console.WriteLine("Input name the second player:");
                SecondPlayerName = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Play {0} и {1}", FirstPlayerName, SecondPlayerName);
            }
        }

        /// <summary>
        /// Input the main word
        /// </summary>
        static void InputMainWord()
        {
            if (String.Equals(Choise, "Рус", StringComparison.CurrentCultureIgnoreCase))
                Console.WriteLine("Введите начальное слово от 8 до 30 букв:");
            else
                Console.WriteLine("Enter a starting word from 8 to 30 letters:");
            MainWord = Console.ReadLine();
        }

        /// <summary>
        /// Check and write the main word to the file
        /// </summary>
        static void ChekMainWord()
        {
            bool errorFlag = true;
            while (errorFlag)
            {
                InputMainWord();
                MainWordLength = MainWord.Length;
                if (MainWordLength >= 8 && MainWordLength <= 30)
                {
                    for (int i = 0; i < MainWordLength; i++)
                    {
                        if (!(char.IsLetter(MainWord, i)))
                        {
                            Console.Clear();
                            if (String.Equals(Choise, "Рус", StringComparison.CurrentCultureIgnoreCase))
                                Console.WriteLine("В слове есть число или символ повторите ввод.");
                            else
                                Console.WriteLine("There is a number or character in the word, repeat input.");
                            errorFlag = true;
                            break;
                        }
                        else
                        {
                            File.WriteAllText("Task1.txt", MainWord);
                            errorFlag = false;
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    if (String.Equals(Choise, "Рус", StringComparison.CurrentCultureIgnoreCase))
                        Console.WriteLine("Недостаточно букв в слове.");
                    else
                        Console.WriteLine("Not enough letters in the word.");
                }
            }
            MainWord = File.ReadAllText("Task1.txt");
        }

        /// <summary>
        /// Determines the status of the game
        /// </summary>
        /// <param name="count">Int value counting totel words.</param>
        /// <param name="pointOne">Int value counting number of the first player words.</param>
        /// <param name="pointTwo">Int value counting number of the second player words.</param>
        static void PrintGameStatus(int count, int pointOne, int pointTwo)
        {
            if (String.Equals(Choise, "Рус", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Главное слово {0} {1}:{2} {3}:{4}", MainWord, FirstPlayerName, pointOne, SecondPlayerName, pointTwo);
                Y = Console.CursorTop;
                if ((count % 2) == 0)
                    Console.WriteLine("{0} введите слово :", FirstPlayerName);
                else
                    Console.WriteLine("{0} введите слово :", SecondPlayerName);
            }
            else
            {
                Console.WriteLine("Main word {0} {1}:{2} {3}:{4}", MainWord, FirstPlayerName, pointOne, SecondPlayerName, pointTwo);
                Y = Console.CursorTop;
                if ((count % 2) == 0)
                    Console.WriteLine("{0} input word :", FirstPlayerName);
                else
                    Console.WriteLine("{0} input word :", SecondPlayerName);
            }
        }

        /// <summary>
        /// Main game process
        /// </summary>
        public static void Game()
        {
            int pointOne = 0, pointTwo = 0, count = 0;
          
            while (true)
            {
                int countWord = 0;

                PrintGameStatus(count, pointOne, pointTwo);
                Timer();
                CurrentWord = Console.ReadLine();
                int playWordLength = CurrentWord.Length;

                // If there isn't word then exit the cycle and the player lose
                if (string.IsNullOrEmpty(CurrentWord) || oneMinute == new DateTime(1,1,1,0,0,0))
                {
                    break;
                }

                //There is a comparison of letters of the main word and the player
                int[] massLetters = new int[MainWordLength];
                foreach (char ch in CurrentWord.ToLower())
                {
                    for(int l = 0; l < MainWordLength; l++)
                    {
                        if (ch == MainWord.ToLower()[l])
                        {
                            if (massLetters[l] == ch)
                                l++;
                            else
                            {
                                massLetters[l] = ch;
                                countWord++;
                                break;
                            }    
                        }
                    }
                }

                // If the word is normal comparison with the words entered earlier
                if (Equals(playWordLength, countWord))
                {
                    using (StreamReader sr = new StreamReader("Task1.txt", Encoding.UTF8)) 
                    {
                        while (true)
                        {
                            // Read string from file to temporary variable
                            string pastWords = sr.ReadLine();
                            // Word comparisons
                            if (String.Equals(CurrentWord, pastWords, StringComparison.CurrentCultureIgnoreCase))
                            {
                                Console.Clear();
                                if (String.Equals(Choise, "Рус", StringComparison.CurrentCultureIgnoreCase))
                                    Console.WriteLine("Это слово уже было.");
                                else
                                    Console.WriteLine("This word has already been.");
                                timer.Stop();
                                break;
                            }
                            else
                            {
                                if (pastWords == null)
                                {
                                    if ((count % 2) == 0)
                                    {
                                        count++;
                                        pointOne++;
                                    }
                                    else
                                    {
                                        count++;
                                        pointTwo++;
                                    }
                                    timer.Stop();
                                    oneMinute = new DateTime(1, 1, 1, 0, 1, 0);
                                    Console.Clear();
                                    break;
                                }
                            }
                        }
                    }
                    File.AppendAllText("Task1.txt", Environment.NewLine + CurrentWord);
                }
                else
                {
                    Console.Clear();
                    if(String.Equals(Choise, "Рус", StringComparison.CurrentCultureIgnoreCase))
                        Console.WriteLine("Такое слово нельзя составить.");
                    else
                        Console.WriteLine("This word can't be compiled.");
                    timer.Stop();
                }
            }
            //If the game is over, the winner is determined
            DeterminationOfWin(pointOne, pointTwo);
        }

        /// <summary>
        /// Determination of winner
        /// </summary>
        /// <param name="pointOne">Int value counting number of the first player words.</param>
        /// <param name="pointTwo">Int value counting number of the second player words.</param>
        public static void DeterminationOfWin(int pointOne, int pointTwo)
        {
            string winner = "";
            if (pointOne == 0 && pointTwo == 0)
                if (String.Equals(Choise, "Рус", StringComparison.CurrentCultureIgnoreCase))
                    winner = "не определился";
                else
                    winner = "not determined";
            if (pointOne > pointTwo)
                winner = FirstPlayerName;
            if(pointOne == pointTwo && pointOne != 0)
                winner = SecondPlayerName;
            if(String.Equals(Choise, "Рус", StringComparison.CurrentCultureIgnoreCase))
                Console.WriteLine("Победитель " + winner);
            else
                Console.WriteLine("Winner " + winner);
        }

        /// <summary>
        /// Language selection
        /// </summary>
        static void LanguageSelection()
        {
            Console.WriteLine("Выберите язык: Рус/Англ?");
            Choise = Console.ReadLine();
        }

        /// <summary>
        /// Main function program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            LanguageSelection();
            PrintWelcomeText();
            ChekMainWord();
            Game();

            File.Delete("Task1.txt");
            Console.ReadKey();
        }
    }
}
