namespace SmileGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;
    using System.Security;
    using System.Threading;
    using System.Media;
    
    class Program
    {
        static List<Zombie> zombies = new List<Zombie>();
        static System.Media.SoundPlayer soundEffect = new System.Media.SoundPlayer();

        public static void LoadLevel(int level)
        {
            int startLevel = level;

            string[] allLevels =
                            {                                 
                                 @"..\..\levels\New.lvl",
                                 @"..\..\levels\GoGoGo.lvl",                                 
                                 @"..\..\levels\Labir.lvl",
                                 @"..\..\levels\BehindEnemyLines.lvl", 
                                 @"..\..\levels\BehindEnemyes2.lvl"
                             };
            
            StreamReader lvl;
            string ll;

            for (int i = level; i < allLevels.Length; i++)
            {
                PlaySound("../../sounds/reach-door.wav"); //Play sound when reach the exit block to the next level

                lvl = new StreamReader(allLevels[i],
                    Encoding.GetEncoding("Windows-1251"));
                using (lvl)
                {
                    ll = lvl.ReadToEnd();
                }

                if (i == 3)
                {
                    zombies.Add(new Zombie(new int[,] { { 23, 11 }, { 22, 11 }, { 21, 11 }, { 20, 11 }, { 19, 11 }, { 18, 11 }, { 17, 11 }, { 16, 11 }, { 15, 11 } }));
                    zombies.Add(new Zombie(new int[,] { { 15, 15 }, { 16, 15 }, { 17, 15 }, { 18, 15 }, { 19, 15 }, { 20, 15 }, { 21, 15 }, { 22, 15 }, { 23, 15 } }));
                    Engine.LoadNewLevel(new Map(ll), new List<Zombie>(zombies));
                }
                else if (i == 4)
                {
                    zombies.Add(new Zombie(new int[,] 
                    { 
                            { 18, 19 },{ 17, 19 }, { 16, 19 },{ 15, 19 }, { 14, 19 },{ 13, 19 },{ 12, 19 },{ 11, 19 },
                            { 11, 18 }, { 11, 17 }, { 11, 16 },{ 11, 15 },{ 11, 14 }, { 11, 13 }, { 11, 12 }, { 11, 11 }, { 11, 10 }, { 11, 9 }, { 11, 8 }, { 11, 7 },
                            { 12, 7 }, { 13, 7 }, { 14, 7 }, { 15, 7 }, { 16, 7 }, { 17, 7 }, { 18, 7 }, { 19, 7 }, { 20, 7 }, { 21, 7 }, { 22, 7 }, { 23, 7 }, { 24, 7 }, { 25, 7 }, { 26, 7 },{ 27, 7 },
                            { 27, 8 }, { 27, 9 }, { 27, 10 }, { 27, 11 }, { 27, 12 }, { 27, 13 }, { 27, 14 }, { 27, 15 }, { 27, 16 }, { 27, 17 }, { 27, 18 }, { 27, 19 },
                            { 26, 19 }, { 25, 19 }, { 24, 19 }, { 23, 19 }, { 22, 19 }, { 21, 19 }, { 20, 19 },{ 19, 19 },{ 18, 19 },{ 17, 19 }, { 16, 19 },{ 15, 19 }, { 14, 19 },{ 13, 19 },{ 12, 19 },{ 11, 19 },
                            { 11, 18 }, { 11, 17 }, { 11, 16 },{ 11, 15 },{ 11, 14 }, { 11, 13 }, { 11, 12 }, { 11, 11 }, { 11, 10 }, { 11, 9 }, { 11, 8 }, { 11, 7 },
                            { 12, 7 }, { 13, 7 }, { 14, 7 }, { 15, 7 }, { 16, 7 }, { 17, 7 }, { 18, 7 }, { 19, 7 }, { 20, 7 }, { 21, 7 }, { 22, 7 }, { 23, 7 }, { 24, 7 }, { 25, 7 }, { 26, 7 },{ 27, 7 },
                            { 27, 8 }, { 27, 9 }, { 27, 10 }, { 27, 11 }, { 27, 12 }, { 27, 13 }, { 27, 14 }, { 27, 15 }, { 27, 16 }, { 27, 17 }, { 27, 18 }, { 27, 19 },
                            { 26, 19 }, { 25, 19 }, { 24, 19 }, { 23, 19 }, { 22, 19 }, { 21, 19 }, { 20, 19 },{ 19, 19 },
                    }));

                    zombies.Add(new Zombie(new int[,] 
                    { 
                            { 18, 7 }, { 19, 7 }, { 20, 7 }, { 21, 7 }, { 22, 7 }, { 23, 7 }, { 24, 7 }, { 25, 7 }, { 26, 7 },{ 27, 7 },
                            { 27, 8 }, { 27, 9 }, { 27, 10 }, { 27, 11 }, { 27, 12 }, { 27, 13 }, { 27, 14 }, { 27, 15 }, { 27, 16 }, { 27, 17 }, { 27, 18 }, { 27, 19 },
                            { 26, 19 }, { 25, 19 }, { 24, 19 }, { 23, 19 }, { 22, 19 }, { 21, 19 }, { 20, 19 },{ 19, 19 },{ 18, 19 },{ 17, 19 }, { 16, 19 },{ 15, 19 }, { 14, 19 },{ 13, 19 },{ 12, 19 },{ 11, 19 },
                            { 11, 18 }, { 11, 17 }, { 11, 16 },{ 11, 15 },{ 11, 14 }, { 11, 13 }, { 11, 12 }, { 11, 11 }, { 11, 10 }, { 11, 9 }, { 11, 8 }, { 11, 7 },
                            { 12, 7 }, { 13, 7 }, { 14, 7 }, { 15, 7 }, { 16, 7 }, { 17, 7 }, { 18, 7 },{ 19, 7 }, { 20, 7 }, { 21, 7 }, { 22, 7 }, { 23, 7 }, { 24, 7 }, { 25, 7 }, { 26, 7 },{ 27, 7 },
                            { 27, 8 }, { 27, 9 }, { 27, 10 }, { 27, 11 }, { 27, 12 }, { 27, 13 }, { 27, 14 }, { 27, 15 }, { 27, 16 }, { 27, 17 }, { 27, 18 }, { 27, 19 },
                            { 26, 19 }, { 25, 19 }, { 24, 19 }, { 23, 19 }, { 22, 19 }, { 21, 19 }, { 20, 19 },{ 19, 19 },{ 18, 19 },{ 17, 19 }, { 16, 19 },{ 15, 19 }, { 14, 19 },{ 13, 19 },{ 12, 19 },{ 11, 19 },
                            { 11, 18 }, { 11, 17 }, { 11, 16 },{ 11, 15 },{ 11, 14 }, { 11, 13 }, { 11, 12 }, { 11, 11 }, { 11, 10 }, { 11, 9 }, { 11, 8 }, { 11, 7 },
                            { 12, 7 }, { 13, 7 }, { 14, 7 }, { 15, 7 }, { 16, 7 }, { 17, 7 }, { 18, 7 },
                    }));

                    Engine.LoadNewLevel(new Map(ll), new List<Zombie>(zombies));
                }
                else
                {
                    Engine.LoadNewLevel(new Map(ll));
                }

                zombies.Clear();
            }
        }

        public static void PlaySound(string filePath)
        {            
            soundEffect.SoundLocation = filePath;
            soundEffect.Play();
        }

        public void ConsoleDraw()
        {
            Console.SetWindowSize(40, 40);
            Console.Clear();
        }

        private static void GameSuccessfullyCompletedEnding()
        {
            Console.Clear();

            Program.PlaySound("../../sounds/game-completed.wav");

            try
            {
                StreamReader gameEnding = new StreamReader("../../LogoGame/GameEnding.txt", Encoding.GetEncoding("UTF-8"));

                using (gameEnding)
                {
                    string endingText = gameEnding.ReadToEnd();

                    Console.SetCursorPosition(0, 0);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(endingText);

                    GameMenu.EndingGame();
                }
            }
            catch (FileNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The file specified in path was not found.");
            }
            catch (DirectoryNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The specified path is invalid (for example, it is on an unmapped drive).");
            }
            catch (ArgumentNullException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The given path is NULL\n and some method received a null argument!");
            }
            catch (ArgumentException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Path is a zero-length string, contains only white space, or contains one or more invalid characters.");
            }
            catch (PathTooLongException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" The specified path, file name, or both exceed\n the system-defined maximum length." +
                    "Path must be\n less than 248 characters, and file names\n must be less than 260 characters.");
            }
            catch (NotSupportedException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The given path is in an invalid format.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("UnauthorizedAccesException detected! The reasons may be the following:\n" +
                    " - Path specified a file that is read-only.\n - This operation is not supported on the current platform.\n" +
                    " - The caller does not have the required permission.");
            }
            catch (SecurityException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sorry, but you does not have the required permission.");
            }
            catch (IOException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An I/O (Input/Output) error occurred while opening the file.");
            }
        }

        static void Main()
        {
            //Starting Game
            GameMenu.Menu(); 

            //Ending Game
            GameSuccessfullyCompletedEnding();
        }        
    }
}
