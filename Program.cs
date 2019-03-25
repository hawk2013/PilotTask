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
        private static string firstPlayerName;
        private static string secondPlayerName;
        private static string word;

        // Input names players
        static void PrintWelcomeText()
        {
            Console.WriteLine("Введите имя первого игрока:");
            firstPlayerName = Console.ReadLine();
            Console.WriteLine("Введите имя второго игрока:");
            secondPlayerName = Console.ReadLine();
            Console.WriteLine("Играют {0} и {1}", firstPlayerName, secondPlayerName);
        }

        // Input main word.
        static void InputMainWord()
        {
            Console.WriteLine("Введите начальное слово от 8 до 30 букв:");
            word = Console.ReadLine();
        }

        //Check and write the main word to the file.
        static void ChekMainWord()
        {
            bool wordCorrect = true;
            while (wordCorrect)
            {
                InputMainWord();
                if (word.Length >= 8 && word.Length <= 30)
                {
                    for (int i = 0; i < word.Length; i++)
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

        // Game class creation
        public static void Game(string firstPlayerName, string secondPlayerName, string word)
        {
            string playWord;
            int pointOne = 0, pointTwo = 0, count = 0;
            // Cycle game
            while (true)
            {
                int countWord = 0;
                Console.WriteLine("Главное слово {0} {1}:{2} {3}:{4}", word, firstPlayerName, pointOne, secondPlayerName, pointTwo);
                if ((count % 2) == 0)
                    Console.WriteLine("{0} введите слово:", firstPlayerName);
                else
                    Console.WriteLine("{0} введите слово:", secondPlayerName);
                playWord = Console.ReadLine();

                // If there isn't word then exit the cycle and the player lose.
                if (playWord == "")
                    break;

                //There is a comparison of letters
                for (int a = 0; a < playWord.Length; a++)
                {
                    for (int j = 0; j < word.Length; j++)
                    {
                        if (playWord.ToLower()[a] == word.ToLower()[j])
                        {
                            countWord++;
                            break;
                        }
                    }

                }

                // If the word is normal comparison with the words entered earlier.
                if (playWord.Length == countWord)
                {
                    using (StreamReader sr = new StreamReader("Task1.txt", Encoding.UTF8))
                    {
                        while (true)
                        {
                            string temp = sr.ReadLine();
                            if (String.Equals(playWord, temp))
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
            DeterminationOfWin(firstPlayerName, secondPlayerName, pointOne, pointTwo);
        }

        //Determination of winner.
        public static void DeterminationOfWin(string firstPlayerName, string secondPlayerName, int pointOne, int pointTwo)
        {
            string winner;

            if(pointOne == 0 && pointTwo == 0)
                winner = "не определился";
            if (pointOne > pointTwo)
                winner = firstPlayerName;
            else
                winner = secondPlayerName;
            Console.WriteLine("Победитель " + winner);
        }

        static void Main(string[] args)
        {

            PrintWelcomeText();
            ChekMainWord();
            Game(firstPlayerName, secondPlayerName, word);

            File.Delete("Task1.txt");

            Console.ReadKey();
        }
    }
}



