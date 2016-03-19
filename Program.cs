using System;

namespace MazeGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MazeHandler maze = new MazeHandler();
            maze.Generate();

            MazePrinter mazePrint = new MazePrinter(maze.MazeInformations);
            mazePrint.Print();
            Console.ReadKey(true);
            
            //maze.AddLoops();
            //mazePrint.DynamicPrint();
            //System.Console.ReadKey(true);

            maze.Resolve(0, maze.MazeInformations.EntryPositionWidth);

            MazePrinter mazePrintBis = new MazePrinter(maze.MazeInformations);
            mazePrintBis.Print();
            Console.ReadKey(true);
        }
    }
}
