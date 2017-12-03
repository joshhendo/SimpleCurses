# SimpleCurses
A simple curses library for .NET, with only a thin layer of abstraction. Currently in a very early state, it is not ready for production usage.

This project is designed to assist in making a Textual User Interface. It is loosely designed around two concepts: MVC and Virtual DOM of ReactJS. In short, each View created has to expose a function that returns a matrix representing what to render. This matrix is then passed into the Virtual Console, which compares the current state with the new state, and updates only the parts of the console that have changed.

A controller can have one or more views, and handles passing data into the views and handling the response from the views.

There is only a thin layer of abstraction currently. Each view is responsible for building the matrix, however there is a `RenderableDotGenerator` class which can assist in basic functions, such as writing text (rather than having the view loop through the matrix adding in individual characters)

## Planned Features

  * Modular Views. Currently only one view can be displayed at a time, and it is responsible for rendering the entire console window. In the future, there's plans to allow for views to have other views render certain parts of the window, for example, a `Split` view could render one view on half of the screen, and another view on the other half of the screen.

## Drawbacks

This project was hacked together over a few hours on a weekend, initially as a minimum viable product to use with another project. Whilst working on it, the architecture evolved very quickly (for example, there wasn't any concept of a Controller at first, everything was done in the View). There are many things about the current architecture that, whilst work, are not optimal. As such, the architecture will continue to evolve rapidly until the project reaches a stable state.

## How To Use

More detailed instructions on how to get a project up and running will be added in the future. For the time being, the example project is the best place to see the basics of how to use this library.


## Known Bugs
  * Text wrapping is currently handled in a very simplistic way. The sample menu view won't work properly if the menu items need to be wrapped.