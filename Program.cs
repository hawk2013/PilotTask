using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace PilotTask1
{
    class Program
    {
        static string firstPlayerName;
        static string secondPlayerName;
        static string word;
        static string playWord;
        static int longWord;

        /// <summary>
        /// Input names players
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
        /// Input the main word.
        /// </summary>
        static void InputMainWord()
        {
            Console.WriteLine("Введите начальное слово от 8 до 30 букв:");
            word = Console.ReadLine();
        }

        /// <summary>
        /// Check and write the main word to the file.
        /// </summary>
        static void ChekMainWord()
        {
            bool wordCorrect = true;
            while (wordCorrect)
            {
                InputMainWord();
                longWord = word.Length;
                if (longWord >= 8 && longWord <= 30)
                {
                    for (int i = 0; i < longWord; i++)
                    {
                        if (!(char.IsLetter(word, i)))
                        {
                            Console.WriteLine("В слове есть число или символ повторите ввод.");
                            wordCorrect = true;
                            break;
                        }
                        else
                        {
                            File.WriteAllText("Task1.txt", word);
                            wordCorrect = false;
                        }
                    }
                }
                else
                    Console.WriteLine("Недостаточно букв в слове.");
            }
            word = File.ReadAllText("Task1.txt");
        }

        /// <summary>
        /// Determines the status of the game.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="pointOne"></param>
        /// <param name="pointTwo"></param>
        static void PrintGameStatus(int count, int pointOne, int pointTwo)
        {
            Console.WriteLine("Главное слово {0} {1}:{2} {3}:{4}", word, firstPlayerName, pointOne, secondPlayerName, pointTwo);
            if ((count % 2) == 0)
                Console.WriteLine("{0} введите слово:", firstPlayerName);
            else
                Console.WriteLine("{0} введите слово:", secondPlayerName);
        }

        /// <summary>
        /// Cycle game where happens the comparasion of words entered by users and start word.
        /// </summary>
        public static void Game()
        {
            int pointOne = 0, pointTwo = 0, count = 0;
          
            while (true)
            {
                int countWord = 0;

                PrintGameStatus(count, pointOne, pointTwo);

                playWord = Console.ReadLine();
                int longPlayWord = playWord.Length;

                // If there isn't word then exit the cycle and the player lose.
                if (playWord == "")
                    break;

                //There is a comparison of letters
                int[] massLetters = new int[longWord];
                foreach (char ch in playWord.ToLower())
                {
                  //  int i = 0;
                    for(int l = 0; l < longWord; l++)
                    {
                        if (ch == word.ToLower()[l])
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

                // If the word is normal comparison with the words entered earlier.
                if (longPlayWord == countWord)
                {
                    using (StreamReader sr = new StreamReader("Task1.txt", Encoding.UTF8))
                    {
                        while (true)
                        {
                            string temp = sr.ReadLine();
                            if (Equals(playWord, temp))
                            {
                                Console.WriteLine("Это слово уже было.");
                                break;
                            }
                            else
                            {
                                if (temp == null)
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
                    File.AppendAllText("Task1.txt", Environment.NewLine + playWord);
                }
                else
                    Console.WriteLine("Такое слово нельзя составить.");
            }
            DeterminationOfWin(pointOne, pointTwo);
        }

        /// <summary>
        /// Determination of winner.
        /// </summary>
        /// <param name="pointOne"></param>
        /// <param name="pointTwo"></param>
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
        /// Main function program.
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
