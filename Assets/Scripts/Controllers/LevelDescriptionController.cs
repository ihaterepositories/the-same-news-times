public class LevelDescriptionController
{
    public static string GetDescription(int levelTypeNumber)
    {
        switch (levelTypeNumber)
        {
            case 1: return "Congratulations, this is lucky temple, no enemies, no dificulties, just tresaures!";
            case 2: return "Default temple, just default temple...";
            case 3: return "Abandoned temple, there is nothing here...";
            case 4: return "Be careful, this temple is guarded by the temple keeper!";
            case 5: return "The exit of this temple is locked, find the key and be aware from traps!";
            default: return "LEVEL TYPE ERROR, please send a bug report";
        }
    }
}
