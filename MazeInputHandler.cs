using System;

namespace MazeGenerator
{
    public enum InputArrow
    {
        Up,
        Down,
        Left, 
        Right,
        Garbage
    }
    public static class MazeInputHandler
    {
        public static InputArrow GetArrow()
        {
            ConsoleKeyInfo arrow = Console.ReadKey();

            switch(arrow.Key)
            {
                case ConsoleKey.UpArrow:
                    return InputArrow.Up;
                case ConsoleKey.DownArrow:
                    return InputArrow.Down;
                case ConsoleKey.LeftArrow:
                    return InputArrow.Left;
                case ConsoleKey.RightArrow:
                    return InputArrow.Right;
                default:
                    return InputArrow.Garbage;
            }
        }
    }
}
