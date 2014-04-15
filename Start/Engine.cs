namespace SmileGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;

    class Engine
    {
        static Map CurrentLevel;
        static Map ReloadedLevel;

        static Hero player;
        static int InitialLives = 4;

        static List<Zombie> zombies = new List<Zombie>();

        /// <summary>
        /// Init level with zombies
        /// </summary>
        public static void LoadNewLevel(Map level, List<Zombie> zombi)
        {
            zombies = zombi;
            LoadNewLevel(level);
        }

        /// <summary>
        /// Init level without zombies
        /// </summary>
        public static void LoadNewLevel(Map level)
        {
            CurrentLevel = level;
            ReloadedLevel = level.Clone() as Map;

            if (player == null) // if player is not created; deep in levels
            { player = new Hero(0, 0, InitialLives); }
            Redraw();
            PositionHero(player);
            // add enemies as well

            Run();
            if (player.Lives > 0)
            { return; }

            Program.PlaySound("../../sounds/game-over.wav"); //Game over sound

            Console.SetCursorPosition(52, 18);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("GAME OVER");

            GameMenu.EndingGame();

        }

        /// <summary>
        /// Reload level
        /// </summary>
        public static void Reload()
        {
            CurrentLevel = (Map)ReloadedLevel.Clone();
            PositionHero(player);
            Redraw();

        }

        /// <summary>
        /// Run the game
        /// </summary>
        private static void Run()
        {
            int counter = 0; //counter for zombie speed
            while (player.Lives > 0)
            {

                counter++;
                Thread.Sleep(50);
                CurrentLevel.ClearMarked();

                MoveHero(player);
                player.Redraw();
                if (counter % 3 == 0)
                {
                    if (counter == 100)
                    {
                        counter = 0;
                    }
                    HuntHero(zombies);
                }

                int x = player.X,
                    y = player.Y;
                if (CurrentLevel[y,x]==block.GreenKey||
                    CurrentLevel[y,x]==block.YellowKey)
                {
                    Program.PlaySound("../../sounds/trigger-hit.wav"); //Play a sound when hit the trigger

                    CurrentLevel.FindWall(CurrentLevel[y, x], y, x);
                    CurrentLevel.AddBlock(x, y, block.Empty);                    
                }
                else if (CurrentLevel[y,x]==block.Exit)
                {
                    return;
                }
            }

            return;
        }

        /// <summary>
        /// Redraw the screen
        /// </summary>
        public static void Redraw()
        {
            Console.BufferHeight = Console.WindowHeight = 30;
            Console.BufferWidth = Console.WindowWidth = 80;
            Console.Clear();
            CurrentLevel.Redraw();
            DrawInfo();
        }

        /// <summary>
        /// Draw info on the side of the game
        /// </summary>
        private static void DrawInfo()
        {
            int col = 40;
            int line = 0;
            ConsoleColor textColor = ConsoleColor.White;
            Console.ForegroundColor = textColor;

            Console.SetCursorPosition(col, line++);
            Console.Write("\tBlock Labyrinth");

            line++;
            Console.SetCursorPosition(col, line++);
            Console.Write("Move around using arows.");

            Console.SetCursorPosition(col, line++);
            Console.Write("Press 'R' to reset the level.");

            line++;
            Console.SetCursorPosition(col, line++);
            Console.Write("Step on:");
            Console.ForegroundColor = Map.ColorOf(block.YellowKey);
            Console.SetCursorPosition(col, line++);
            Console.Write('\u25a0');
            Console.ForegroundColor = textColor;
            Console.Write(" to remove yellow walls.");

            Console.SetCursorPosition(col, line++);
            Console.ForegroundColor = Map.ColorOf(block.GreenKey);
            Console.Write('\u25a0');
            Console.ForegroundColor = textColor;
            Console.Write(" to remove green walls.");

            Console.SetCursorPosition(col, line++);
            Console.ForegroundColor = Map.ColorOf(block.Exit);
            Console.Write('\u25a0');
            Console.ForegroundColor = textColor;
            Console.Write(" to finish the level.");

            line++;
            Console.SetCursorPosition(col, line++);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write('\u25a0');
            Console.ForegroundColor = textColor;
            Console.Write(" Will hunt you down.");

            line ++;
            Console.SetCursorPosition(col, line++);
            Console.Write("You have {0} lives left.", player.Lives);
        }
        /// <summary>
        /// Find starting position of the hero
        /// </summary>
        public static void PositionHero(Hero player)
        {

            for (int i = 0; i < Map.MapHight; i++)
            {
                for (int j = 0; j < Map.MapWidth; j++)
                {
                    if (CurrentLevel[i,j]==block.Start)
                    {// what if load a map with 2 or more block.Start
                        player.X = j;
                        player.Y = i;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Move hero around
        /// </summary>
        public static void MoveHero(Hero player)
        {
            int x = player.X,
                y = player.Y;

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo Pressed = Console.ReadKey(true);
                switch (Pressed.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (CurrentLevel.isWalkable(y+1,x))
                        {
                            DrawTrail(x, y);
                            player.moveDown(true);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (CurrentLevel.isWalkable(y,x - 1))
                        {
                            DrawTrail(x, y);
                            player.moveLeft(true);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (CurrentLevel.isWalkable(y,x + 1))
                        {
                            DrawTrail(x, y);
                            player.moveRight(true);
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (CurrentLevel.isWalkable(y - 1, x))
                        {
                            DrawTrail(x, y);
                            player.moveUp(true);
                        }
                        break;
                    case ConsoleKey.R:
                        Reload();
                        break;
                    default:
                        break;
                }
            }
        
        }

        /// <summary>
        /// Zombies are on the loose
        /// </summary>
        public static void HuntHero(List<Zombie> zombies)
        {
            foreach (var zombi in zombies)
            {
                zombi.FollowPath();
                if (player.X == zombi.X &&
                    player.Y == zombi.Y)
                {
                    Program.PlaySound("../../sounds/touch-enemy.wav"); //Play a sound when touch a zombie
                    player.Lives--;
                    Reload();
                }
            }
        }

        /// <summary>
        /// Remove trail
        /// </summary>
        private static void DrawTrail(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = CurrentLevel.ColorOf(x, y);
            Console.Write(CurrentLevel.Tile);
        }
    }
}
