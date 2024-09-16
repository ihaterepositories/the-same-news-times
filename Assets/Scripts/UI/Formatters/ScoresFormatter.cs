namespace UI.Formatters
{
    public static class ScoresFormatter
    {
        public static string FormatNumber(int number)
        {
            return number switch
            {
                >= 1000000 => number / 1000000 + "m",
                >= 1000 => number / 1000 + "k",
                _ => number.ToString()
            };
        }
    }
}