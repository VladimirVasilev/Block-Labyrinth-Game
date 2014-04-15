namespace SmileGame
{
    using System;
    using System.Text;
    using System.IO;
    using System.Threading;

    class MapMaker
    {
        // Map to work upon 
        static Map Level;
        static bool isSaved;

        struct Cursor
        {
            int x, y;

            // x property
            public int X
            {
                get { return x; }
                set
                {   // Check if not out of map
                    if (Level.isWithin(value, Y))
                    {
                        DrawPoint(x, y);    // Repaint the trail before moving.
                        x = value;
                    }
                }
            }

            // y property
            public int Y
            {
                get { return y; }
                set
                {   // Check if not out of map
                    if (Level.isWithin(X, value))
                    {
                        DrawPoint(x, y);    // Repaint the trail before moving.
                        y = value;
                    }
                }
            }

            public ConsoleColor color;
        };

        // Cursor to move and change map blocks
        static Cursor m = new Cursor();

        /// <summary>
        /// Create new level
        /// </summary>
        public static void LoadNewLevel()
        {
            isSaved = false; // in case of reload
            Level = new Map();  // initialize the map
            Redraw();
            m.X = 19; m.Y = 13;     // I like the center; To be removed
            while (!isSaved)
            {
                // TODO: Move file Saving here
                Thread.Sleep(10);   // slow a little bit 
                setCursor(ref m);
            }
            Console.Clear();
            GameMenu.Menu();
        }

        /// <summary>
        /// Redraw the map
        /// </summary>
        private static void Redraw()
        {
            Console.BufferHeight = Console.WindowHeight = 30;
            Console.BufferWidth = Console.WindowWidth = 60;
            Console.Clear();
            Level.Redraw();
            DrawHelp();
        }

        /// <summary>
        /// Draw point with coordinates [x,y]
        /// <para>Get the color from the map. It is usefull to get rid from trails of the currsor</para> 
        /// </summary>
        private static void DrawPoint(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = Level.ColorOf(x,y);
            Console.Write(Level.Tile);
        }

        /// <summary>
        /// Set cursor position and Color according to key press
        /// </summary>
        /// <param name="c"></param>
        private static void setCursor(ref Cursor c)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo Pressed = Console.ReadKey(true);
                switch (Pressed.Key)
                {
                        // Move with arrows
                    case ConsoleKey.DownArrow:
                        c.Y++;
                        break;
                    case ConsoleKey.LeftArrow:
                        c.X--;
                        break;
                    case ConsoleKey.RightArrow:
                        c.X++;
                        break;
                    case ConsoleKey.UpArrow:
                        c.Y--;
                        break;
                    case ConsoleKey.E:          // Put Empty block on the map [x,y]
                        Level.AddBlock(c.X, c.Y, block.Empty);
                        c.color = Map.ColorOf(block.Empty);
                        break;
                    case ConsoleKey.W:          // Put Boundary block on the map [x,y]
                        Level.AddBlock(c.X, c.Y, block.BoundaryWall);
                        c.color = Map.ColorOf(block.BoundaryWall);
                        break;
                    case ConsoleKey.Y:          // Put YellowKey block on the map [x,y]
                        Level.AddBlock(c.X, c.Y, block.YellowKey);
                        c.color = Map.ColorOf(block.YellowKey);
                        break;
                    case ConsoleKey.L:          // Put YellowWall block on the map [x,y]
                        Level.AddBlock(c.X, c.Y, block.YellowWall);
                        c.color = Map.ColorOf(block.YellowWall);
                        break;
                    case ConsoleKey.G:          // Put GreenKey block on the map [x,y]
                        Level.AddBlock(c.X, c.Y, block.GreenKey);
                        c.color = Map.ColorOf(block.GreenKey);
                        break;
                    case ConsoleKey.N:          // Put GreenWall block on the map [x,y]
                        Level.AddBlock(c.X, c.Y, block.GreenWall);
                        c.color = Map.ColorOf(block.GreenWall);
                        break;
                    case ConsoleKey.H:          // Put Start block on the map [x,y]
                        Level.AddBlock(c.X, c.Y, block.Start);
                        c.color = ConsoleColor.White;
                        break;
                    case ConsoleKey.X:          // Put Exit block on the map [x,y]
                        Level.AddBlock(c.X, c.Y, block.Exit);
                        c.color = Map.ColorOf(block.Exit);
                        break;
                    case ConsoleKey.Escape:     // Refresh the screen with Esc
                        Redraw();
                        break;
                    case ConsoleKey.F10:        // Save to file; 
                        Save();
                        break;
                    default:                    // Any other Key
                        break;
                }

                Console.SetCursorPosition(c.X, c.Y);    // Set Currsor
                Console.ForegroundColor = c.color;      // Change Color to match if block key is pressed
                Console.Write(Level.Tile);
                c.color = ConsoleColor.Black;           // Go back to black mode <looks hollow this way>
            }
        }

        /// <summary>
        /// Save the current level to file in levels directory
        /// </summary>
        private static void Save()
        {
            // TODO: Need exceptions
            //       Check file names; If file already Exist ...
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter file name for this level :");
            string filename = @"..\..\levels\"+Console.ReadLine()+".lvl";
            try
            {
                using (var newLevel = new StreamWriter(filename))
                {
                    newLevel.Write(Level);
                }
            }
            catch (FileNotFoundException)
            {

                Console.WriteLine("Level not found");
            }
            catch (IOException)
            {
                var exist = File.Exists(@"..\..\levels\filename.lvl");//check file name property
                Console.WriteLine(exist);
            }
            isSaved = true;
        }

        /// <summary>
        /// Draw help menu for creating maps
        /// </summary>
        private static void DrawHelp()
        {
            string[] helper = {
                                  "| Use arrows",
                                  "| to move around",
                                  "|",
                                  "| Press :",
                                  "| E - Empty Block",
                                  "| W - Wall",
                                  "| Y - Yellow Key",
                                  "| L - Yellow Wall",
                                  "| G - Green Key",
                                  "| N - Green Wall",
                                  "| H - Hero Start",
                                  "| X - Exit",
                                  "| Esc - Refresh",
                                  "| F10 - Save Level",
                                  "|___________________"
                              };
            for(int i=0; i<helper.Length;i++)
            {
            Console.SetCursorPosition(40, i);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(helper[i]);
    
            }

            return;
        }
    }
}
