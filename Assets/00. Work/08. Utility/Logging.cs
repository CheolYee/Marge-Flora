
namespace _00._Work._08._Utility
{
    public static class Logging
    {
        [System.Diagnostics.Conditional("DEBUG_ENABLE")]
        public static void Log(string message)
        {
            UnityEngine.Debug.Log(message);
        }
        
        [System.Diagnostics.Conditional("DEBUG_ENABLE")]
        public static void LogWarning(string message)
        {
            UnityEngine.Debug.LogWarning(message);
        }
        
        [System.Diagnostics.Conditional("DEBUG_ENABLE")]
        public static void LogError(string message)
        {
            UnityEngine.Debug.LogError(message);
        }
    }
}
