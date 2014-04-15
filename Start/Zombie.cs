namespace SmileGame
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    class Zombie
    {
        bool EndOfPath = false;
        int[,] path;
        int position;

        public int Position
        {
            get
            { return position; }
            set
            { position = value; }
        }

        public int X
        {
            get
            { return this.path[position, 0]; }
            set
            { this.path[position,0] = value; }
        }

        public int Y
        {
            get
            { return this.path[position,1]; }
            set
            { this.path[position, 1] = value; }
        }

        public Zombie()
        { }

        public Zombie(int[,] path)
        {
            this.path = (int[,])path.Clone();
        }

        /// <summary>
        /// Let the zombie loose upon path
        /// </summary>
        public void FollowPath()
        {
            DrawTrail(X, Y);

            if (EndOfPath)
            {
                Position--;
                if (Position <= 0)
                { EndOfPath = false; }
            }
            else
            {
                Position++;
                if (Position == path.GetLength(0)-1)
                { EndOfPath = true; }
            }

            Redraw();
        }

        /// <summary>
        /// Redraw zombie
        /// </summary>
        private void Redraw()
        {
            Console.SetCursorPosition(this.X, this.Y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write('\u25a0');

        }

        /// <summary>
        /// Take care of the trail that it leaves
        /// </summary>
        private static void DrawTrail(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = Map.ColorOf(block.Empty);
            Console.Write('\u25a0');
        }
    }
}
