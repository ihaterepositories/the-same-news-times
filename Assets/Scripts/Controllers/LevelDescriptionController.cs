public class LevelDescriptionController
{
    public static string GetDescription(int levelTypeNumber)
    {
        switch (levelTypeNumber)
        {
            case 11: return "Congratulations, this is lucky temple, no enemies, no dificulties, just tresaures!";
            case 21: return "Default temple, just default temple...";
            case 31: return "Abandoned temple, there is nothing here...";
            case 41: return "Be careful, this temple is guarded by the temple keeper!";
            case 42: return "The exit of this temple is locked, find the key and be aware from traps!";
            default: return "I don`t want to explain something now :(";
        }
    }
}
