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
        static void Main(string[] args)
        {
            string playerOne;
            string playerTwo;
            string word;
            int num = 0;
            int index;
            // Input names players
            Console.WriteLine("Введите имя первого игрока:");
            playerOne = Console.ReadLine();
            Console.WriteLine("Введите имя второго игрока:");
            playerTwo = Console.ReadLine();
            Console.WriteLine("Играют {0} и {1}", playerOne, playerTwo);
            // Enter start word, condition check and write to file.
            while (num < 1)
            {
                Console.WriteLine("Введите начальное слово с маленькой буквы:");
                word = Console.ReadLine();
                Console.WriteLine(word.Length);
                if (word.Length >= 8 && word.Length <= 30)
                {
                    for (index = 0; index < word.Length; index++)
                    {
                        if (!(char.IsLetter(word, index)))
                        {
                            Console.WriteLine("В слове есть число или символ повторите ввод.");
                            num = 0;
                            break;
                        }
                        else
                        {
                            File.WriteAllText("Task1.txt", word);
                            num++;
                        }
                    }
                }
            }
            word = File.ReadAllText("Task1.txt");
            Game(playerOne, playerTwo, word);

            File.Delete("Task1.txt");

            Console.ReadKey();
        }

        // Game class creation
        public static void Game(string playerOne, string playerTwo, string word)
        {
            string playWord;
            int pointOne = 0, pointTwo = 0, count = 0;
            // Cycle game
            while (true)
            {
                int countword = 0;
                Console.WriteLine("Главное слово {0} {1}:{2} {3}:{4}", word, playerOne, pointOne, playerTwo, pointTwo);
                if ((count % 2) == 0)
                    Console.WriteLine("{0} введите слово:", playerOne);
                else
                    Console.WriteLine("{0} введите слово:", playerTwo);
                playWord = Console.ReadLine();
                //There is a comparison of letters
                for (int a = 0; a < playWord.Length; a++)
                {
                    for (int j = 0; j < word.Length; j++)
                    {
                        if (playWord[a] == word[j])
                        {
                            countword++;
                            break;
                        }
                    }

                }
                // If there isn't word then exit the cycle and the player lose.
                if (playWord == "")
                    break;
                // If the word is normal comparison with the words entered earlier.
                if (playWord.Length == countword)
                {
                    using (StreamReader sr = new StreamReader("Task1.txt", Encoding.UTF8))
                    {
                        while (true)
                        {
                            string temp = sr.ReadLine();
                            if (playWord == temp)
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
            DeterminationOfWin(playerOne, playerTwo, pointOne, pointTwo);
        }
        //Determination of winner.
        public static void DeterminationOfWin(string playerOne, string playerTwo, int pointOne, int pointTwo)
        {
            string result;
            string winner;
            if (pointOne > pointTwo)
                winner = playerOne;
            else
                winner = playerTwo;
            result = "Победил " + winner;
            Console.WriteLine(result);
        }
    }
}

/*if (playWord == "")         //If player don't know word, he will be lose
                   break;
               if (playWord.Length == countword)       //If player input word makes a comparison/
               {
                   if ((count % 2) == 0)
                   {
                       count++;
                       pointOne++;                          // Counter the first player
                   }
                   else
                   {
                       count++;
                       pointTwo++;                         // Counter the second player
                   }
                  // File.AppendAllText(@"C:\Users\lenovo\Desktop\Task1.txt", Environment.NewLine + playWord);
                  using (StreamWriter sw = new StreamWriter(@"C:\Users\lenovo\Desktop\Task1.txt", true, Encoding.UTF8))
                   {
                       sw.Write(Environment.NewLine + playWord);
                   }
               }*/
// string line;
/* while ((line = sr.ReadLine()) != null)
 {
     if (playWord == line)
     {
         Console.WriteLine("Это слово уже было.");
     }
     else
     {
         if ((count % 2) == 0)
         {
             count++;
             pointOne++;                          // Counter the first player
         }
         else
         {
             count++;
             pointTwo++;                         // Counter the second player
         }
     }
 }*/

