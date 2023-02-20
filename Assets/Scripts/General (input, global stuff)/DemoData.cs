public static class DemoData
{
    static public bool isDemoBuild { get; private set; } = false;
    static ushort highestPlayableLevel = 50;
    static public bool unlockAllLevels = false;

    static public ushort GetHighestPlayableLevel()
    {
        if (isDemoBuild)
        {
            return highestPlayableLevel;
        }
        else
        {
            return 50;
        }
    }
}
