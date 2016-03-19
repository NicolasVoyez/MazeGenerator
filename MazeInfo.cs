namespace MazeGenerator
{
    public enum SquareType
    {
        Wall = '#',
        Free = '.',
        CurrentPos = '0',
        Path = '°',
        Unknown = '?',
        Explored = '*'
    }
    public class MazeInfo
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int EntryPositionWidth { get; set; }
        public int ExitPositionWidth { get; set; }
        public SquareType[,] Map { get; set; }
    }
}
