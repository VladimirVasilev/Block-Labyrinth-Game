namespace SmileGame
{
    using System;
    using System.IO;
    using System.Security;
    using System.Text;
    using System.Threading;

    public class GameMenu
    {
        //Coordinates of our cursor at the start menu
        static int cursorX = Console.WindowWidth / 2;
        static int cursorY;
        static byte optionCursorPosition = 0;

        //Menu method itself
        public static void Menu()
        {
            optionCursorPosition = 0; // in case of reload

            Console.Title = "Block Labyrinth";
            Console.CursorVisible = false; //Make cursor invisible
            Console.SetWindowSize(80, 35);
            Console.SetBufferSize(80, 35);
            
            Console.SetCursorPosition(0, 1);
            //Read from a text file contains of our Start Game Logo and print it to the console          
            try
            {
                StreamReader welcomeLogo = new StreamReader("../../LogoGame/WelcomeLogo.txt", Encoding.GetEncoding("UTF-8"));

                using (welcomeLogo)
                {
                    string currentLine = welcomeLogo.ReadLine();

                    while (currentLine != null)
                    {
                        for (int i = 0; i < currentLine.Length; i++)
                        {
                            if (currentLine[i] != '=' && currentLine[i] != '|')
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.Write(currentLine[i]);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.Write(currentLine[i]);
                            }
                        }

                        //Console.WriteLine();
                        currentLine = welcomeLogo.ReadLine();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Logo visualization error!\nThe file specified in path was not found.");
            }
            catch (DirectoryNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Logo visualization error!\nThe specified path is invalid (for example, it is on an unmapped drive).");
            }
            catch (ArgumentNullException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Logo visualization error!\nThe given path is NULL\n and some method received a null argument!");
            }
            catch (ArgumentException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Logo visualization error!\nPath is a zero-length string, contains only white space, or contains one or more invalid characters.");
            }
            catch (PathTooLongException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Logo visualization error!\nThe specified path, file name, or both exceed\n the system-defined maximum length." +
                    "Path must be\n less than 248 characters, and file names\n must be less than 260 characters.");
            }
            catch (NotSupportedException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Logo visualization error!\nThe given path is in an invalid format.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Logo visualization error!\nUnauthorizedAccesException detected! The reasons may be the following:\n" +
                    " - Path specified a file that is read-only.\n - This operation is not supported on the current platform.\n" +
                    " - The caller does not have the required permission.");
            }
            catch (SecurityException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Logo visualization error!\nSorry, but you does not have the required permission.");
            }
            catch (IOException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Logo visualization error!.\nAn I/O (Input/Output) error occurred while opening the file.");
            }
            
            cursorY = Console.WindowHeight / 2 + 7; //Set coordinates where our start menu options will be printed

            //Call methods to:
            PrintOptions();
            RenderCursor(cursorX, cursorY);
            MoveCursor();
        }

        public static void PrintOptions()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.SetCursorPosition(cursorX + 3, cursorY);
            Console.WriteLine("Start Game");
            Console.SetCursorPosition(cursorX + 3, cursorY + 1);
            Console.WriteLine("Choose Level");
            Console.SetCursorPosition(cursorX + 3, cursorY + 2);
            Console.WriteLine("Create Level");
            Console.SetCursorPosition(cursorX + 3, cursorY + 3);
            Console.WriteLine("Exit Game");
        }

        public static void OptionSelected()
        {
            if (optionCursorPosition == 3) //Our cursor is at the position of EXIT option and the user has pressed ENTER, so he is quitting the game
            {
                EndingGame();
            }
            else if (optionCursorPosition == 2)
            {
                MapMaker.LoadNewLevel();
            }
            else if (optionCursorPosition == 1) //Our cursor is at the position of LOAD GAME otpion and the user has pressed ENTER
            {
                Console.Clear();

                //Print Load Menu options
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(Console.WindowWidth / 2 - 8, 3);
                Console.WriteLine("Choose Level");

                Console.ForegroundColor = ConsoleColor.Magenta;

                Console.SetCursorPosition(21, 6);
                Console.WriteLine("Level 1 - type \"1\" and press Enter");

                Console.SetCursorPosition(21, 7);
                Console.WriteLine("Level 2 - type \"2\" and press Enter");

                Console.SetCursorPosition(21, 8);
                Console.WriteLine("Level 3 - type \"3\" and press Enter");

                Console.SetCursorPosition(21, 9);
                Console.WriteLine("Level 4 - type \"4\" and press Enter");

                Console.SetCursorPosition(21, 10);
                Console.WriteLine("Level 5 - type \"5\" and press Enter");

                Console.SetCursorPosition(13, 12);
                Console.WriteLine("Return to Main Menu - type \"back\" and press Enter");

                //Command section printing
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(26, 15);
                Console.WriteLine(new string('=', 20));
                Console.SetCursorPosition(30, 16);
                Console.Write("Command: ");
                Console.SetCursorPosition(26, 17);
                Console.WriteLine(new string('=', 20));

                Console.CursorVisible = true; //Make cursor visible

                CheckForValidCommand();
            }
            else if (optionCursorPosition == 0) //Our cursor is at the position of START GAME option and the user has pressed ENTER
            {
                Program.LoadLevel(0);
            }
        }

        private static void CheckForValidCommand()
        {
            bool isValidCommand = false;

            while (isValidCommand == false)
            {
                Console.SetCursorPosition(39, 16);
                Console.ForegroundColor = ConsoleColor.Cyan;
                string userCommand = Console.ReadLine().ToLower();

                if (userCommand == "1")
                {
                    isValidCommand = true;
                    SimulateLoading();
                    Program.LoadLevel(0);
                }
                else if (userCommand == "2")
                {
                    isValidCommand = true;
                    SimulateLoading();
                    Program.LoadLevel(1);
                }
                else if (userCommand == "3")
                {
                    isValidCommand = true;
                    SimulateLoading();
                    Program.LoadLevel(2);
                }
                else if (userCommand == "4")
                {
                    isValidCommand = true;
                    SimulateLoading();
                    Program.LoadLevel(3);
                }
                else if (userCommand == "5")
                {
                    isValidCommand = true;
                    SimulateLoading();
                    Program.LoadLevel(4);
                }
                else if (userCommand == "back")
                {
                    isValidCommand = true;
                    optionCursorPosition = 0;
                    Console.Clear();
                    Menu();
                }
                else
                {
                    Program.PlaySound("../../sounds/error-command.wav");

                    Console.SetCursorPosition(17, 20);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Command is NOT VALID! Please try again!");

                    InvalidCommandRemove(userCommand);
                }
            }
        }

        private static void SimulateLoading()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 13, Console.WindowHeight - 10);
            Console.Write("Loading");
            for (int i = 0; i < 15; i++)
            {
                Console.Write(".");
                Thread.Sleep(150);
            }

            Console.WriteLine();
        }

        public static void EndingGame()
        {
            string exitMessage = "Thanks for playing our game! Bye-bye ;]";
            Console.SetCursorPosition(Console.WindowWidth / 2 - 20, Console.WindowHeight - 3);
            Console.ForegroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < exitMessage.Length; i++)
            {
                Console.Write(exitMessage[i]);
                Thread.Sleep(120);
            }

            Thread.Sleep(2000);
            Console.WriteLine();
            Environment.Exit(0);
            Console.ReadLine();
            Console.ResetColor();

        }

        //It removes the wrong inputted command and places the cursor in the right place for the new command
        private static void InvalidCommandRemove(string userCommand)
        {
            for (int i = 0; i < userCommand.Length; i++)
            {
                Console.SetCursorPosition(39 + i, 16);
                Console.WriteLine(" ");
            }
        }

        public static void RenderCursor(int xCoord, int yCoord)
        {
            Console.SetCursorPosition(xCoord, yCoord);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write('#');
        }

        public static void MoveCursor()
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            while (key.Key != ConsoleKey.Enter)
            {
                key = Console.ReadKey();

                if (key.Key == ConsoleKey.DownArrow && optionCursorPosition <= 2)
                {
                    Console.Beep(); //Make beep when we move cursor
                    Console.SetCursorPosition(cursorX, cursorY);
                    Console.Write(' '); //Delete the previous cursor symbol at the respective coords
                    cursorY++;
                    optionCursorPosition++;
                    RenderCursor(cursorX, cursorY);
                }
                else if (key.Key == ConsoleKey.UpArrow && optionCursorPosition >= 1)
                {
                    Console.Beep(); //Make beep when we move cursor
                    Console.SetCursorPosition(cursorX, cursorY);
                    Console.Write(' ');//Delete the previous cursor symbol at the respective coords
                    cursorY--;
                    optionCursorPosition--;
                    RenderCursor(cursorX, cursorY);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    OptionSelected();
                }
                else
                {
                    RenderCursor(cursorX, cursorY);
                }
            }
        }
    }
}
