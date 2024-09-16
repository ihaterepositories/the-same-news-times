using Spawners.EnemiesSpawners;
using Spawners.ItemsSpawners;

namespace Spawners
{
    public class ObjectsSpawner
    {
        public MazeExitSpawner MazeExitSpawner { get; }
        public PointsSpawner PointsSpawner { get; }
        public ModificatorsSpawner ModificatorsSpawner { get; }
        public TempleKeeperSpawner TempleKeeperSpawner { get; }
        public TrapSpawner TrapSpawner { get; }
        public GhostSpawner GhostSpawner { get; }

        public ObjectsSpawner(
            MazeExitSpawner mazeExitSpawner, 
            PointsSpawner pointsSpawner, 
            ModificatorsSpawner modificatorsSpawner,
            TempleKeeperSpawner templeKeeperSpawner, 
            TrapSpawner trapSpawner, 
            GhostSpawner ghostSpawner)
        {
            MazeExitSpawner = mazeExitSpawner;
            PointsSpawner = pointsSpawner;
            ModificatorsSpawner = modificatorsSpawner;
            TempleKeeperSpawner = templeKeeperSpawner;
            TrapSpawner = trapSpawner;
            GhostSpawner = ghostSpawner;
        }
    }
}