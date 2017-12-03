using System;

namespace SimpleCurses.Rendering
{
    public class RenderableDotGenerator
    {
        private RenderableDot[][] renderableDots;
        private int width;
        private int height;

        private int currentXPosition;
        private int currentYPosition;

        public RenderableDotGenerator(): this(Console.WindowWidth, Console.WindowHeight)
        {
        }

        public RenderableDotGenerator(int width, int height)
        {
            renderableDots = new RenderableDot[height][];
            for (int y = 0; y < height; y++)
            {
                renderableDots[y] = new RenderableDot[width];
            }

            this.width = width;
            this.height = height;
        }

        public void IncrementY()
        {
            if (++currentYPosition >= height)
            {
                currentYPosition = 0;
            }
        }
        
        public void IncrementX(int amount = 1)
        {
            currentXPosition += amount;
            if (currentXPosition >= width)
            {
                currentXPosition = 0;
                IncrementY();
            }
        }

        public RenderableDot[][] GetRenderableDots()
        {
            return renderableDots;
        }

        public void SetPosition(int x, int y)
        {
            if (x >= width || y >= height)
            {
                // TODO: Better exceptions
                throw new Exception();
            }

            currentXPosition = x;
            currentYPosition = y;
        }

        public void SetX(int x)
        {
            if (x >= width)
            {
                throw new Exception();
            }

            currentXPosition = x;
        }

        public void SetY(int y)
        {
            if (y >= height)
            {
                throw new Exception();
            }

            currentYPosition = y;
        }

        public void Write(string text, ConsoleColor? background = null, ConsoleColor? foreground = null)
        {
            var startingX = currentXPosition;
            
            foreach (var c in text)
            {
                if (c == '\n')
                {
                    IncrementY();
                    currentXPosition = startingX;
                    continue;
                }

                if (c == '\t')
                {
                    IncrementX(3);
                    continue;
                }
                
                renderableDots[currentYPosition][currentXPosition] = new RenderableDot
                {
                    Character = c,
                    Background = background ?? ConsoleColor.Black,
                    Foreground = foreground ?? ConsoleColor.White,
                };
                
                IncrementX();
            }
        }
    }
}