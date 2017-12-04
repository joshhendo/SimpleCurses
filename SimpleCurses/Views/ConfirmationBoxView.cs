using System;
using System.Linq;
using SimpleCurses.Handlers;
using SimpleCurses.Rendering;

namespace SimpleCurses.Views
{
    public class ConfirmationBoxView: MessageBoxView
    {
        private int currentButton;
        private string[] buttons;
        
        public ConfirmationBoxView(string message, string[] buttons) : base(message, string.Empty)
        {
            if (buttons.Length == 0)
            {
                throw new ArgumentException("Need at least one button");
            }
            
            this.currentButton = 0;
            this.buttons = buttons;
        }
        
        public override void HandleKeyPress(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    currentButton = currentButton == 0 ? 0 : currentButton - 1;
                    break;
                case ConsoleKey.RightArrow:
                    currentButton = currentButton == buttons.Length - 1 ? buttons.Length - 1 : currentButton + 1;
                    break;
                case ConsoleKey.Enter:
                    OnFinished(this, currentButton);
                    break;
            }
        }
        
        protected override void GetButtons(RenderableDotGenerator generator)
        {
            var totalLength = buttons.Sum(x => x.Length) + (4 * buttons.Length) + (buttons.Length - 1);
            
            generator.SetX((Console.WindowWidth / 2) - totalLength / 2);

            for (int i = 0; i < buttons.Length; i++)
            {
                var color = ConsoleColor.Black;
                if (i == currentButton)
                {
                    color = ConsoleColor.Blue;
                }
                
                generator.Write($"  {buttons[i]}  ", color);
            }
        }
    }
}