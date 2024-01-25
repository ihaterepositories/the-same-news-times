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
            case 1: RarityDescription = "Legendary"; return Random.Range(11, 12);
            case 2: RarityDescription = "Epic"; return Random.Range(21, 22);
            case 3: RarityDescription = "Rare"; return Random.Range(31, 32);
            case 4: RarityDescription = "Default"; return Random.Range(41, 43);
            default: return 404;
        }
    }
}
