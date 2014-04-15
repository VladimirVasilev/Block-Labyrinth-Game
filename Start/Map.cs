namespace SmileGame
{
    using System;
    using System.Text;

    //[Flags]
    enum block
    {
        Empty            = 0x0,     // walkable squares
        BoundaryWall     = 0x1,     // wall that can not be violated
        YellowKey        = 0x2,     // triggers or keys that when stepped upon brake walls (Yellow)
        YellowWall       = 0x3,     // Yellow wall that brake when yellow key is pressed
        GreenKey         = 0x4,     // green key/trigger
        GreenWall        = 0x5,     // green wall
        Start            = 0x6,     // Start position for the hero
        Exit             = 0x7,     // Exit point of the level <violet one>
        EnemyStart       = 0x8,
        EnemyPath        = 0x9,
        Marker           = 0x23      // Need this to Mark which walls fall when key press
        // TODO: Enemy start and path; up/down cw & ccw travel
    };

    class Map : ICloneable
    {
        static char tile = '\u25a0';    // block char representation
        public char Tile{get { return tile; }}

        /// <summary>
        /// Static Table containing the colors of every object on the map
        /// </summary>
        static ConsoleColor[] paint = {
                                       ConsoleColor.DarkGray,       // Empty spaces
                                       ConsoleColor.Blue,           // Border
                                       ConsoleColor.Yellow,     // YellowKey
                                       ConsoleColor.DarkYellow,         // YellowWall
                                       ConsoleColor.Green,      // GreenKey
                                       ConsoleColor.DarkGreen,          // GreenWall
                                       ConsoleColor.White,       // Start is meaningfull only for hero position; paint as Empty
                                       ConsoleColor.DarkMagenta,    // Exit
                                       ConsoleColor.DarkGray,
                                       ConsoleColor.DarkGray,
                                       ConsoleColor.White           // Marker
                                   };

        public const int MapHight = 27; // hardcode map Height
        public const int MapWidth = 39; // & Width

        block[,] gameMap; // map itself

// Constructors
        /// <summary>
        /// Empty constructor for the map.
        /// </summary>
        public Map() 
        {
            this.gameMap = new block[MapHight, MapWidth];
        }

        public Map(block[,] gMap)
        {
            this.gameMap = (block[,])gMap.Clone();
        }
        /// <summary>
        /// Constructor from string.
        /// <para>String is flatten array of a sort & we cut it upon new line.</para>
        /// </summary>
        public Map(string line)
        {
            // TODO: exceptions are needed
            //       string length may differ from dimensions
            this.gameMap = new block[MapHight, MapWidth];
            string[] flatArray = line.Split('\n');  // split upon new line
            for (int i = 0; i < MapHight; i++)
            {
                for (int j = 0; j < MapWidth; j++)
                {
                        this.gameMap[i, j] =(block)int.Parse(flatArray[i][j] + "");
                }
            }
        }

// METHODS

        public block this[int row, int col]
        {
            get
            { return gameMap[row, col]; }
            set
            { gameMap[row, col] = value; }
        }

        /// <summary>
        /// Return the color of block with coordinates [x,y]
        /// </summary>
        public ConsoleColor ColorOf(int x, int y)
        {
            // TODO: need exception handling
            //       coordinates are not checked
            return paint[(int)this.gameMap[y,x]];
        }

        /// <summary>
        /// Return the color of any of the block types
        /// </summary>
        public static ConsoleColor ColorOf(block b)
        {
            // TODO: Checks for (int)b
            return paint[(int)b];
        }

        /// <summary>
        /// Traverse all paths to wall of the same color as key
        /// </summary>
        /// <param name="key">Green or Yellow Key</param>
        /// <param name="x">Current  position x</param>
        /// <param name="y">Current position y</param>
        public void FindWall(block key, int x, int y)
        {
            // Depth First Search to find all walkable paths.
            // TODO: Exceptions
            // TODO: case when wall is consecutive blocks - one blocking the other.
            // FIX: Do not work in case of wall of Keys <exmaple Loop.lvl>
            // ... possible solution [Flags]

            int[,] directions = { 
                              { 1, 0, -1, 0 }, // x
                              { 0, 1, 0, -1 }  // y
                              };

            // mark current position as visited
            this.MarkAsWalked(x,y);

            for (int i = 0; i < directions.GetLength(1); i++)
            {
                int row = x + directions[0, i]; // next
                int col = y + directions[1, i]; // steps

                if (isWalkable(row, col))
                {
                    this.FindWall(key, row, col);
                }
                else if (this.gameMap[row,col]==key+1)
                {
                    // if wall is the same color as Key mark it as visited as well.
                    // TODO: implement function checking consecutive blocks of the same color.
                    this.gameMap[row, col] = block.Marker;
                }
            }
        }

        /// <summary>
        /// Mark only Empty space
        /// </summary>
        private void MarkAsWalked(int x, int y)
        {
            // TODO: will need fixing if incorporate [Flags]
            // FIX: possible infinite loop of several consecutive blocks
            //      that are walkable & are not marked <example Keys>
            switch (this.gameMap[x,y])
            {
                case block.BoundaryWall:
                case block.YellowKey:
                case block.YellowWall:
                case block.GreenKey:
                case block.GreenWall:
                case block.Exit:
                default:
                case block.Marker:
                    return;
                case block.Empty:
                case block.Start:
                    this.gameMap[x, y] = block.Marker; break;
            }

            Console.SetCursorPosition(y, x);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(Tile);
        }

        /// <summary>
        /// Check if block at position x,y is walkable
        /// </summary>
        public bool isWalkable(int row, int col)
        {
            // TODO: Exception handler
            // Console.WriteLine(this.gameMap[x,y]);
            switch (this.gameMap[row,col])
            {
                case block.BoundaryWall:
                case block.YellowWall:
                case block.GreenWall:
                case block.Marker:
                default:
                    return false;
                case block.Start:
                case block.Exit:
                case block.Empty:
                case block.YellowKey:
                case block.GreenKey:
                case block.EnemyPath:
                case block.EnemyStart:
                    return true;
            }
        }

        /// <summary>
        /// Return true if point with coordinates is within the map
        /// </summary>
        public bool isWithin(int x, int y)
        {
            bool horizont = (x >= 0) && (x < MapWidth);
            bool vertical = (y >= 0) && (y < MapHight);
            return horizont && vertical;
        }

        /// <summary>
        /// Clear all blocks that are marked to Empty
        /// </summary>
        public void ClearMarked()
        {
            // TODO: Needs modification if [Flags] is incorporated
            for (int i = 0; i < this.gameMap.GetLength(0); i++)
            {
                for (int j = 0; j < this.gameMap.GetLength(1); j++)
                {
                    if (this.gameMap[i,j]==block.Marker)
                    {
                        this.gameMap[i, j] = block.Empty;
                        Console.SetCursorPosition(j, i);
                        Console.ForegroundColor = ColorOf(block.Empty);
                        Console.Write(Tile);
                    }
                }
            }
        }

        /// <summary>
        /// Put block of certain type in any position
        /// </summary>
        /// <param name="b">Chose block type</param>
        public void AddBlock(int x, int y,block b)
        {
            // TODO: reconsider if not beter with property
            this.gameMap[y, x] = b;
        }

        /// <summary>
        /// Overload the mother f*cker
        /// </summary>
        /// <returns></returns>
        public override string ToString()//public Stringbuilder cannot be overriden
        {
            // TODO: better with string builder
            string str = string.Empty;//StringBuilder str = new StringBuilder().Clear();
            for (int i = 0; i < MapHight; i++)
            {
                for (int j = 0; j < MapWidth; j++)
                {
                    str += String.Format("{0}", (int)this.gameMap[i, j]);//str += (str.AppendFormat("{0}", (int)this.gameMap[i, j])).ToString();
                }

                str += "\n";//str += str.AppendLine("\n").ToString();
            }

            return str; //return str.ToString();
        }

        /// <summary>
        /// Redraw the current state of map
        /// </summary>
        public void Redraw()
        {
            for (int i = 0; i < MapHight; i++)
            {
                for (int j = 0; j < MapWidth; j++)
                {
                    Console.ForegroundColor = ColorOf(j, i);
                    Console.Write(tile);
                }

                Console.WriteLine();
            }
        }

        public object Clone()
        {
            return new Map(this.gameMap);
        }
    }
}
