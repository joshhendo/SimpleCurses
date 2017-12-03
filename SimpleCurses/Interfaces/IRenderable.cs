using System;
using SimpleCurses.Handlers;
using SimpleCurses.Rendering;

namespace SimpleCurses.Interfaces
{
    public interface IRenderable
    {
        event ViewEventHandler Finished;
        void HandleKeyPress(ConsoleKeyInfo key);
        RenderableDot[][] GetRenderable();
    }
}