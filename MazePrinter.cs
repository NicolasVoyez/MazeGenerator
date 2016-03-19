using System;

namespace MazeGenerator
{
    public class MazePrinter
    {
        public MazeInfo MazeInformations { get; set; } 

        public MazePrinter(MazeInfo infos)
        {
            this.MazeInformations = infos;
        }

        public void Print()
        {
            Console.Clear();

            for (int i = 0; i < MazeInformations.Height; i++)
            {
                for (int j = 0; j < MazeInformations.Width; j++)
                {
                    Console.Write((char)MazeInformations.Map[i, j]);
                }
                Console.WriteLine("");
            }
        }

        public void DynamicPrint()
        {
            Console.Clear();

            bool[,] explored = new bool[MazeInformations.Height, MazeInformations.Width];

            int x = 0;
            int y = MazeInformations.EntryPositionWidth;

            for (int i = 0; i < MazeInformations.Height; i++)
            {
                for (int j = 0; j < MazeInformations.Width; j++)
                {
                    explored[i, j] = false;
                }
            }

            while (true)
            {
                MazeInformations.Map[x, y] = SquareType.CurrentPos;

                for (int i = 0; i < MazeInformations.Height; i++)
                {
                    for (int j = 0; j < MazeInformations.Width; j++)
                    {
                        if ((i >= x - 1 && i <= x + 1) && (j >= y - 1 && j <= y + 1))
                        {
                            explored[i,j] = true;
                        }
                        if (explored[i,j])
                        {
                            Console.Write((char)MazeInformations.Map[i, j]);
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine("");
                }

                MazeInformations.Map[x, y] = SquareType.Free;

                if (x == MazeInformations.Height - 1 && y == MazeInformations.ExitPositionWidth) break;

                InputArrow arrow = MazeInputHandler.GetArrow();
                
                if (x > 0 && arrow == InputArrow.Up && MazeInformations.Map[x - 1, y] == SquareType.Free) x--;
                if (arrow == InputArrow.Down && MazeInformations.Map[x + 1, y] == SquareType.Free) x++;
                if (arrow == InputArrow.Left && MazeInformations.Map[x, y - 1] == SquareType.Free) y--;
                if (arrow == InputArrow.Right && MazeInformations.Map[x, y + 1] == SquareType.Free) y++;

                Console.Clear();
            }

            Console.WriteLine("You won !");
        }
    }
}
