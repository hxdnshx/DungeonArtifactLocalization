using BepInEx.Logging;

namespace catrice.DungeonArtifactTrans
{
    public static class Logger
    {
        public static ManualLogSource LogInstance;

        public static void Log(string format, params object[] args) => Log(string.Format(format, args));

        public static void Log(string str)
        {
            LogInstance?.Log(LogLevel.Message, str);
        }

        public static void Warning(string format, params object[] args) => Warning(string.Format(format, args));

        public static void Warning(string str)
        {
            LogInstance?.Log(LogLevel.Warning, str);
        }

        public static void Error(string format, params object[] args) => Error(string.Format(format, args));

        public static void Error(string str)
        {
            LogInstance?.Log(LogLevel.Error, str);
        }
    }
}