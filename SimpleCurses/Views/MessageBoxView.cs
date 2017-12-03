using System;
using System.Collections.Generic;
using SimpleCurses.Handlers;
using SimpleCurses.Interfaces;
using SimpleCurses.Rendering;

namespace SimpleCurses.Views
{
    public class MessageBoxView: IRenderable
    {
        private string message;
        private string buttonLabel;
        
        public MessageBoxView(string message, string buttonLabel = "Got It")
        {
            this.message = message;
            this.buttonLabel = buttonLabel;
        }

        public event ViewEventHandler Finished;

        public void HandleKeyPress(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    Finished.Invoke(this, null);
                    break;
            }
        }

        private string[] SplitStringToLength(string input, int length)
        {
            var output = new List<string>();

            while (input.Length > 0)
            {
                if (input.Length <= length)
                {
                    output.Add(input);
                    break;
                }

                var currentLine = input.Substring(0, length);
                output.Add(currentLine);
                input = input.Remove(0, length);
            }

            return output.ToArray();
        }

        public RenderableDot[][] GetRenderable()
        {
            var generator = new RenderableDotGenerator();

            var fromTop = 5;
            var fromSides = 10;
            var width = Console.WindowWidth - (10 * 2);

            var lines = SplitStringToLength(message, width);

            generator.SetPosition(fromSides, fromTop);
            
            foreach (var line in lines)
            {
                generator.Write(line);
                generator.IncrementY();
            }
            
            generator.IncrementY();

            var buttonWidth = buttonLabel.Length + 4;
            generator.SetX((Console.WindowWidth / 2) - buttonWidth/2);
            generator.Write($"  {buttonLabel}  ", ConsoleColor.Blue);

            return generator.GetRenderableDots();
        }
    }
}