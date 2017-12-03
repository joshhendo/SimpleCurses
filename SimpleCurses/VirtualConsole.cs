using System;
using SimpleCurses.Rendering;

namespace SimpleCurses
{
    public class VirtualConsole
    {
        public static VirtualConsole Instance { get; private set; }
        public bool Ended { get; private set; } = false;

        public VirtualConsole()
        {
            currentState = new RenderableDotGenerator().GetRenderableDots();
        }
        
        public static VirtualConsole Create()
        {
            if (Instance != null)
            {
                return Instance;
            }
            
            Instance = new VirtualConsole();

            return Instance;
        }

        private object stateLock = new object();
        private RenderableDot[][] currentState = null;

        private bool DoDotsMatch(RenderableDot first, RenderableDot second)
        {
            return (first.Character == second.Character) && (first.Background == second.Background) &&
                   (first.Foreground == second.Foreground);
        }
        
        public void Update(RenderableDot[][] newState)
        {
            try
            {
                lock (stateLock)
                {
                    if (currentState.GetHashCode() == newState.GetHashCode())
                    {
                        return;
                    }

                    var updateAll = false;
                    if (newState.Length != currentState.Length || newState[0].Length != currentState[0].Length)
                    {
                        updateAll = true;
                        Console.Clear();
                        currentState = newState;
                    }

                    for (int y = 0; y < newState.Length; y++)
                    {
                        for (int x = 0; x < newState[y].Length; x++)
                        {
                            if (updateAll || !DoDotsMatch(newState[y][x], currentState[y][x]))
                            {
                                Console.SetCursorPosition(x, y);
                                var currentDot = newState[y][x];

                                if (currentDot.Background != null)
                                {
                                    Console.BackgroundColor = currentDot.Background.Value;
                                }

                                if (currentDot.Foreground != null)
                                {
                                    Console.ForegroundColor = currentDot.Foreground.Value;
                                }

                                Console.Write(newState[y][x].Character);
                            }
                        }
                    }

                    currentState = newState;
                }
            } catch (ArgumentOutOfRangeException) {}
        }

        public void End()
        {
            this.Ended = true;
        }
    }
}