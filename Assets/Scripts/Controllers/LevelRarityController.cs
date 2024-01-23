public class LevelRarityController
{
    private enum LevelsSpawnRarity
    {
        LuckyLevel = 10,
        DefaultLevel = 20,
        AbandonedLevel = 30,
        EnemyLevel = 50,
        LockedLevel = 50
    }

    public static int[] GetLevelSpawnRarity()
    {
        return new int[] 
        { 
            (int)LevelsSpawnRarity.LuckyLevel, 
            (int)LevelsSpawnRarity.DefaultLevel, 
            (int)LevelsSpawnRarity.AbandonedLevel, 
            (int)LevelsSpawnRarity.EnemyLevel, 
            (int)LevelsSpawnRarity.LockedLevel 
        };
    }
}
