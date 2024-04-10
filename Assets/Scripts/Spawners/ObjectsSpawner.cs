using Spawners.EnemiesSpawners;
using Spawners.ItemsSpawners;

namespace Spawners
{
    public class ObjectsSpawner
    {
        public PinkScoreSpawner PinkScoreSpawner { get; }
        public GreenScoresSpawner GreenScoresSpawner { get; }
        public TempleKeeperSpawner TempleKeeperSpawner { get; }
        public LockSpawner LockSpawner { get; }
        public KeySpawner KeySpawner { get; }
        public TrapSpawner TrapSpawner { get; }
        public GhostSpawner GhostSpawner { get; }
        public BoosterSpawner BoosterSpawner { get; }
        public LifeSaverSpawner LifeSaverSpawner { get; }
        public PoisonSpawner PoisonSpawner { get; }

        public ObjectsSpawner(
            PinkScoreSpawner pinkScoreSpawner, 
            GreenScoresSpawner greenScoresSpawner, 
            TempleKeeperSpawner templeKeeperSpawner, 
            LockSpawner lockSpawner, 
            KeySpawner keySpawner, 
            TrapSpawner trapSpawner, 
            GhostSpawner ghostSpawner,
            BoosterSpawner boosterSpawner,
            LifeSaverSpawner lifeSaverSpawner,
            PoisonSpawner poisonSpawner)
        {
            PinkScoreSpawner = pinkScoreSpawner;
            GreenScoresSpawner = greenScoresSpawner;
            TempleKeeperSpawner = templeKeeperSpawner;
            LockSpawner = lockSpawner;
            KeySpawner = keySpawner;
            TrapSpawner = trapSpawner;
            GhostSpawner = ghostSpawner;
            BoosterSpawner = boosterSpawner;
            LifeSaverSpawner = lifeSaverSpawner;
            PoisonSpawner = poisonSpawner;
        }
    }
}