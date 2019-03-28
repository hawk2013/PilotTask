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
        static string firstPlayerName;
        static string secondPlayerName;
        static string MainWord;
        static string CurrentWord;
        static int MainWordLength;

        /// <summary>
        /// Input names of players
        /// </summary>
        static void PrintWelcomeText()
        {
            Console.WriteLine("Игра в слова.");
            Console.WriteLine("Правила: В начале игры пользователь вводит слово определенной длины: суть игры заключается в том,\n" + 
                    "чтобы 2 пользователя поочередно вводили слова, состоящие из букв первоначально указанного слова.\n" +
                    "Проигрывает тот, кто в свою очередь не вводит слово.\n");
            Console.WriteLine("Введите имя первого игрока:");
            firstPlayerName = Console.ReadLine();
            Console.WriteLine("Введите имя второго игрока:");
            secondPlayerName = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Играют {0} и {1}", firstPlayerName, secondPlayerName);
        }

        /// <summary>
        /// Input the main word
        /// </summary>
        static void InputMainWord()
        {
            Console.WriteLine("Введите начальное слово от 8 до 30 букв:");
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
                            Console.WriteLine("В слове есть число или символ повторите ввод.");
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
                    Console.WriteLine("Недостаточно букв в слове.");
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
            Console.WriteLine("Главное слово {0} {1}:{2} {3}:{4}", MainWord, firstPlayerName, pointOne, secondPlayerName, pointTwo);
            if ((count % 2) == 0)
                Console.WriteLine("{0} введите слово:", firstPlayerName);
            else
                Console.WriteLine("{0} введите слово:", secondPlayerName);
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
                CurrentWord = Console.ReadLine();
                int playWordLength = CurrentWord.Length;

                // If there isn't word then exit the cycle and the player lose
                if (string.IsNullOrEmpty(CurrentWord))
                    break;

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
                if (string.Equals(playWordLength, countWord))
                {
                    using (StreamReader sr = new StreamReader("Task1.txt", Encoding.UTF8)) 
                    {
                        while (true)
                        {
                            // Read string from file to temporary variable
                            string pastWords = sr.ReadLine();
                            // Word comparisons
                            if (string.Equals(CurrentWord, pastWords))
                            {
                                Console.WriteLine("Это слово уже было.");
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
                                    Console.Clear();
                                    break;
                                }
                            }
                        }
                    }
                    File.AppendAllText("Task1.txt", Environment.NewLine + CurrentWord);
                }
                else
                    Console.WriteLine("Такое слово нельзя составить.");
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
            if(pointOne == 0 && pointTwo == 0)
                winner = "не определился";
            if (pointOne > pointTwo)
                winner = firstPlayerName;
            if(pointOne == pointTwo && pointOne != 0)
                winner = secondPlayerName;
            Console.WriteLine("Победитель " + winner);
        }

        /// <summary>
        /// Main function program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            PrintWelcomeText();
            ChekMainWord();
            Game();

            File.Delete("Task1.txt");
            Console.ReadKey();
        }
    }
}
