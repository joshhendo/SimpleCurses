using System;
using SimpleCurses.Handlers;
using SimpleCurses.Interfaces;
using SimpleCurses.Rendering;

namespace SimpleCurses.Views
{
    public class MenuView: IRenderable
    {
        private string[] options;
        private int selected;
        
        public MenuView(string[] options)
        {
            this.options = options;
            this.selected = 0;
        }

        public void MenuUp()
        {
            if (selected > 0)
            {
                this.selected -= 1;
            }
        }

        public void MenuDown()
        {
            if (selected < options.Length - 1)
            {
                this.selected += 1;
            }
        }

        public event ViewEventHandler Finished;

        public void HandleKeyPress(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.DownArrow:
                    MenuDown();
                    break;
                case ConsoleKey.UpArrow:
                    MenuUp();
                    break;
                case ConsoleKey.Enter:
                    Finished?.Invoke(this, selected);
                    break;
            }
        }

        public RenderableDot[][] GetRenderable()
        {
            var generator = new RenderableDotGenerator();
            generator.SetPosition(10, 5);

            foreach (var option in options)
            {
                generator.Write(option + "\n");
            }
            
            generator.SetPosition(5, 5 + this.selected);
            generator.Write("-->", ConsoleColor.Black, ConsoleColor.Green);

            return generator.GetRenderableDots();
        }
    }
}