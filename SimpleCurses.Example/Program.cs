using System;
using System.Collections.Generic;
using System.Threading;
using SimpleCurses.Example.Controllers;
using SimpleCurses.Interfaces;

namespace SimpleCurses.Example
{
    class Program
    {
        private static IController currentController = null;
        private static Stack<IController> parents = new Stack<IController>();

        public static void AddChildController(IController childController)
        {
            parents.Push(currentController);
            currentController = childController;
        }

        public static void ReplaceCurrentController(IController newController)
        {
            currentController = newController;
        }

        public static void CloseCurrentController(IController caller)
        {
            if (parents.Count == 0)
            {
                throw new Exception("Can't close root controller");
            }

            if (caller != currentController)
            {
                return;
            }

            currentController = parents.Pop();
        }
        
        static void Main(string[] args)
        {
            Console.Title = "Simple Curses Example";
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var virtualConsole = VirtualConsole.Create();

            currentController = new HomeController();
            
            while (!virtualConsole.Ended)
            {
                virtualConsole.Update(currentController.CurrentView().GetRenderable());
                
                if (Console.KeyAvailable)
                {
                    currentController.CurrentView().HandleKeyPress(Console.ReadKey(true));
                }

                Thread.Sleep(1);
            }
            
            Console.ReadKey();
        }
    }
}