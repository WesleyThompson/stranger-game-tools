using UnityEngine;

namespace StrangerGameTools.Debugging
{
    /// <summary>
    /// A simple MonoBehaviour for debugging events in Unity.
    /// </summary>
    public class EventDebugger : MonoBehaviour
    {
        /// <summary>
        /// Logs a message to the console.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void LogMessage(string message)
        {
            Debug.Log(message);
        }

        /// <summary>
        /// Logs a warning message to the console.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        public void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }

        /// <summary>
        /// Logs an error message to the console.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        public void LogError(string message)
        {
            Debug.LogError(message);
        }
    }
}
