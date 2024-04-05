namespace MazeGeneration
{
    public class Cell
    {
        public int X;
        public int Y;
        public int DistanceFromStartPoint;

        public bool IsHaveLeftWall = true;
        public bool IsHaveBottomWall = true;
        public bool IsVisitedByGenerator = false;
    }
}
