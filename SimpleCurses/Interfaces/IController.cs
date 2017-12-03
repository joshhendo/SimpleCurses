using System;
using SimpleCurses.Rendering;

namespace SimpleCurses.Interfaces
{
    public interface IController
    {
        IRenderable CurrentView();
    }
}