using StrangerGameTools.FSM.Game;
using UnityEngine;

namespace StrangerGameTools
{
    /// <summary>
    ///
    /// </summary>
    public class GameOrchestrator : MonoBehaviour
    {
        public static GameOrchestrator Instance { get; private set; }
        public static GameStateManager GameStateManager;
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            GameStateManager.Instance.Initialize();
        }
    }
}
