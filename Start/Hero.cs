namespace SmileGame
{
    using System;

    /* 
     * Class to implement the hero
     * Moving Left/Right/Up/Down
     * Moving accepts true/false if it is permited to move in the direction
     * NOTE: map do not contain the hero, we only track its coordinates
     */

    class Hero
    {
        int x, y;   // coordinates of the hero
        int lives;  // how many lives

        // property
        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        public int Lives
        {
            get { return this.lives; }
            set { this.lives = value; }
        }

        // constructors
        public Hero() { }   // empty constructor

        /// <summary>
        /// Create Hero and position him
        /// </summary>
        public Hero(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Create Hero; position and give lives
        /// </summary>
        public Hero(int x, int y, int lives)
        {
            this.x = x;
            this.y = y;
            this.lives = lives;
        }

        // methods
        /// <summary>
        /// Move hero to the right
        /// </summary>
        /// <param name="isMovable"></param>
        public void moveRight(bool isMovable)
        {
            if (isMovable) { this.X++; }
        }

        /// <summary>
        /// Move hero to the left
        /// </summary>
        /// <param name="isMovable"></param>
        public void moveLeft(bool isMovable)
        {
            if (isMovable) { this.X--; }
        }

        /// <summary>
        /// Move hero Up
        /// </summary>
        /// <param name="isMovable"></param>
        public void moveUp(bool isMovable)
        {
            if (isMovable) { this.Y--; }
        }

        /// <summary>
        /// Move hero Down
        /// </summary>
        /// <param name="isMovable"></param>
        public void moveDown(bool isMovable)
        {
            if (isMovable) { this.Y++; }
        }

        /// <summary>
        /// Redraw hero
        /// </summary>
        public void Redraw()
        {
            Console.SetCursorPosition(this.X, this.Y);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write('\u25a0');
        }     
    }
}
