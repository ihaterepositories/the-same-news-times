using UnityEngine;

namespace Controllers.InGameControllers
{
    public static class LevelRarityGenerator
    {
        public static string RarityDescription { get; private set; }

        public static int GetLevelType()
        {
            var levelRarity = GetLevelRarity();

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
        
        private static int GetLevelRarity()
        {
            var num = Random.Range(1, 101);

            return num switch
            {
                >= 1 and < 15 => 1,
                >= 15 and < 30 => 2,
                >= 30 and < 50 => 3,
                >= 50 and <= 100 => 4,
                _ => 0
            };
        }
    }
}
