using MazeGeneration;

namespace DataTransfers
{
    public class MazeData
    {
        public Cell[,] Cells;
        public int Width;
        public int Height;

        public MazeData(Cell[,] cells, int width, int height)
        {
            Cells = cells;
            Width = width;
            Height = height;
        }
    }
}