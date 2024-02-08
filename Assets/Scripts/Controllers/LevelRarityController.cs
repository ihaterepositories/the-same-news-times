using UnityEngine;

public class LevelRarityController
{
    public static string RarityDescription { get; private set; }

    private static int GetLevelRarity()
    {
        int num = Random.Range(1, 101);

        return (num >= 1 && num < 15) ? 1 :
               (num >= 15 && num < 30) ? 2 :
               (num >= 30 && num < 50) ? 3 :
               (num >= 50 && num <= 100) ? 4 : 0;
    }

    public static int GetLevelType()
    {
        int levelRarity = GetLevelRarity();

        switch (levelRarity)
        {
            case 1: RarityDescription = "Legendary"; break;
            case 2: RarityDescription = "Epic"; break;
            case 3: RarityDescription = "Rare"; break;
            case 4: RarityDescription = "Default"; break;
            default: return 404;
        }

        return levelRarity;
    }
}
