using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeGenerator
{
    public class MazeHandler
    {
        private const int DefaultWidth = 40;
        private const int DefaultHeight = 20;

        public MazeInfo MazeInformations { get; set; }
        private int CountToExit { get; set; }
        private List<Point> Exposed { get; set; } = new List<Point>();
        public SquareType[,] ResolveMap { get; set; }

        public MazeHandler(int height, int width)
        {
            Random rnd = new Random();

            this.MazeInformations = new MazeInfo
            {
                Map = new SquareType[height, width],
                Width = width,
                Height = height,
                EntryPositionWidth = rnd.Next(1, width - 1)
            };

            this.CountToExit = 0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || i == height - 1 || j == 0 || j == width - 1)
                    {
                        MazeInformations.Map[i, j] = SquareType.Wall;
                    }
                    else
                    {
                        MazeInformations.Map[i, j] = SquareType.Unknown;
                    }
                }
            }

            ResolveMap = new SquareType[height, width];

        }

        public MazeHandler() : this(DefaultHeight, DefaultWidth) { }

        public void Generate()
        {
            Random rnd = new Random();
            MazeInformations.Map[0, MazeInformations.EntryPositionWidth] = SquareType.Free;
            Point entryPoint = new Point(1, MazeInformations.EntryPositionWidth);
            Exposed.Add(entryPoint);

            while (Exposed.Any())
            {
                int rand = rnd.Next(Exposed.Count());

                Point point = Exposed[rand];

                if (Decide(point))
                {
                    Dig(point);
                }
                else
                {
                    MazeInformations.Map[point.X, point.Y] = SquareType.Wall;
                }
                Exposed.RemoveAt(rand);
            }

            for (int i = 0; i < MazeInformations.Height; i++)
            {
                for (int j = 0; j < MazeInformations.Width; j++)
                {
                    if (MazeInformations.Map[i, j] == SquareType.Unknown) MazeInformations.Map[i, j] = SquareType.Wall;
                    ResolveMap[i, j] = MazeInformations.Map[i, j];
                }
            }
        }

        public bool Decide(Point point)
        {
            int nb = 0;

            if (point.X > 0)
            {
                if (MazeInformations.Map[point.X - 1, point.Y] == SquareType.Free)
                {
                    nb++;
                    if (point.Y > 0)
                    {
                        if (MazeInformations.Map[point.X + 1, point.Y - 1] == SquareType.Free) nb++;
                    }

                    if (point.Y < MazeInformations.Width - 1)
                    {
                        if (MazeInformations.Map[point.X + 1, point.Y + 1] == SquareType.Free) nb++;
                    }
                }
            }

            if (point.Y > 0)
            {
                if (MazeInformations.Map[point.X, point.Y - 1] == SquareType.Free)
                {
                    nb++;

                    if (point.X > 0 &&
                        MazeInformations.Map[point.X - 1, point.Y + 1] == SquareType.Free)
                    {
                        nb++;
                    }
                    if (point.X < MazeInformations.Height - 1 &&
                        MazeInformations.Map[point.X + 1, point.Y + 1] == SquareType.Free)
                    {
                        nb++;
                    }

                }
            }

            if (point.Y < MazeInformations.Width - 1)
            {
                if (MazeInformations.Map[point.X, point.Y + 1] == SquareType.Free)
                {
                    nb++;

                    if (point.X > 0 &&
                        MazeInformations.Map[point.X - 1, point.Y - 1] == SquareType.Free)
                    {
                        nb++;
                    }
                    if (point.X < MazeInformations.Height - 1 &&
                        MazeInformations.Map[point.X + 1, point.Y - 1] == SquareType.Free)
                    {
                        nb++;
                    }
                }
            }

            if (point.X < MazeInformations.Height - 1)
            {
                if (MazeInformations.Map[point.X + 1, point.Y] == SquareType.Free)
                {
                    nb++;

                    if (point.X > 0 &&
                        MazeInformations.Map[point.X - 1, point.Y - 1] == SquareType.Free)
                    {
                        nb++;
                    }
                    if (point.Y < MazeInformations.Width - 1 &&
                        MazeInformations.Map[point.X - 1, point.Y + 1] == (SquareType.Free))
                    {
                        nb++;
                    }
                }
            }

            return nb == 1;

        }

        public void Dig(Point point)
        {
            MazeInformations.Map[point.X, point.Y] = SquareType.Free;

            if (point.X > 1)
            {
                if (MazeInformations.Map[point.X - 1, point.Y] == SquareType.Unknown)
                {
                    Point discoveredPoint = new Point(point.X - 1, point.Y);
                    Exposed.Add(discoveredPoint);
                }
            }

            if (point.X < MazeInformations.Height - 2)
            {
                if (MazeInformations.Map[point.X + 1, point.Y] == SquareType.Unknown)
                {

                    Point discoveredPoint = new Point(point.X + 1, point.Y);
                    Exposed.Add(discoveredPoint);
                }
            }

            if (point.Y > 1)
            {
                if (MazeInformations.Map[point.X, point.Y - 1] == SquareType.Unknown)
                {
                    Point discoveredPoint = new Point(point.X, point.Y - 1);
                    Exposed.Add(discoveredPoint);
                }
            }

            if (point.Y < MazeInformations.Width - 2)
            {
                if (MazeInformations.Map[point.X, point.Y + 1] == SquareType.Unknown)
                {
                    Point discoveredPoint = new Point(point.X, point.Y + 1);
                    Exposed.Add(discoveredPoint);
                }
            }

            if (point.X == MazeInformations.Height - 2)
            {
                CountToExit += 1;
                if (CountToExit == MazeInformations.Width / 2 - 1)
                {
                    MazeInformations.Map[point.X + 1, point.Y] = SquareType.Free;
                    this.MazeInformations.ExitPositionWidth = point.Y;
                }
            }
        }

        public void AddLoops()
        {

            Random rnd = new Random();

            for (int i = 1; i < MazeInformations.Height - 2; i++)
            {
                for (int j = 1; j < MazeInformations.Width - 2; j++)
                {
                    if (MazeInformations.Map[i, j] == SquareType.Wall)
                    {
                        int adjacentCount = 0;

                        if (MazeInformations.Map[i + 1, j] == SquareType.Free) adjacentCount++;
                        if (MazeInformations.Map[i, j - 1] == SquareType.Free) adjacentCount++;
                        if (MazeInformations.Map[i, j + 1] == SquareType.Free) adjacentCount++;
                        if (MazeInformations.Map[i - 1, j] == SquareType.Free) adjacentCount++;

                        if (adjacentCount > 1 && rnd.Next(0, 15) == 1) MazeInformations.Map[i, j] = SquareType.Free;

                    }
                }
            }
            for (int i = 0; i < MazeInformations.Height; i++)
            {
                for (int j = 0; j < MazeInformations.Width; j++)
                {
                    if (MazeInformations.Map[i, j] == SquareType.Unknown) MazeInformations.Map[i, j] = SquareType.Wall;
                    ResolveMap[i, j] = MazeInformations.Map[i, j];
                }
            }
        }

        public bool Resolve(int i, int j)
        {
            //Le point fourni doit être une case vide.
            if (ResolveMap[i, j] != SquareType.Free) return false;

            //Si oui, il est marqué comme exploré.
            ResolveMap[i, j] = SquareType.Explored;

            //Récursivité sur les cases ajdacentes.
            if ((i == MazeInformations.Height - 1 && j == MazeInformations.ExitPositionWidth) ||
                (i + 1 > 0 && Resolve(i + 1, j)) ||
                (Resolve(i, j - 1)) ||
                (Resolve(i, j + 1)) ||
                (Resolve(i - 1, j)))
            {
                //La case est marqué comme faisant partie de la solution si un de ses chemins enfants mêne à la sortie.
                MazeInformations.Map[i, j] = SquareType.Path;
                return true;
            }
            return false;
        }
    }
}
