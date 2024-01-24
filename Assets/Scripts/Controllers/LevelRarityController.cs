using UnityEngine;

public class LevelRarityController
{
    private enum LevelsSpawnRarity
    {
        LuckyLevel = /*10*/0,
        DefaultLevel = /*20*/0,
        AbandonedLevel = /*30*/0,
        EnemyLevel = 60,
        LockedLevel = /*50*/0
    }

    public static int GetLevelType()
    {
        int num = Random.Range(1, 101);

        int[] levelWeights = GetLevelSpawnRarity();

        int cumulativeWeight = 0;
        int selectedLevel = 0;

        for (int i = 0; i < levelWeights.Length; i++)
        {
            cumulativeWeight += levelWeights[i];

            if (num <= cumulativeWeight)
            {
                selectedLevel = i + 1;
                break;
            }
        }

        return selectedLevel;
    }

    private static int[] GetLevelSpawnRarity()
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
