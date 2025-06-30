using UnityEngine;

namespace StrangerGameTools.Debugging
{
    /// <summary>
    /// A simple MonoBehaviour for debugging events in Unity.
    /// </summary>
    public class EventDebugger : MonoBehaviour
    {
        private const string _logFormat = "[EventDebugger] {0} from {1}";

        /// <summary>
        /// Logs a message to the console.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void LogMessage(string message)
        {
            Debug.Log(FormatLogMessage(message));
        }

        /// <summary>
        /// Logs a warning message to the console.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        public void LogWarning(string message)
        {
            Debug.LogWarning(FormatLogMessage(message));
        }

        /// <summary>
        /// Logs an error message to the console.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        public void LogError(string message)
        {
            Debug.LogError(FormatLogMessage(message));
        }

        private string FormatLogMessage(string message)
        {
            return string.Format(_logFormat, message, gameObject.name);
        }
    }
}
