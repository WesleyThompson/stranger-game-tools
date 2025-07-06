using System;
using System.Linq;
using UnityEngine;

namespace StrangerGameTools.Settings
{
    /// <summary>
    /// ScriptableObject to hold game mode settings.
    /// This can be used to configure different game modes in the game.
    /// </summary>
    [CreateAssetMenu(fileName = "GameModeSettings", menuName = "Scriptable Objects/GameModeSettings")]
    public class GameModeSettings : ScriptableObject
    {
        public GameStateSettings[] GameStateSettings;

        public GameStateSettings GetStateSettingsByName(string name)
        {
            return GameStateSettings.FirstOrDefault(gs => gs.GameModeName == name);
        }
    }

    /// <summary>
    /// Settings for the cursor in the game.
    /// </summary>
    [Serializable]
    public class CursorSettings
    {
        public CursorLockMode LockMode;
        public bool Visible;

        /// <summary>
        /// Applies the cursor settings.
        /// </summary>
        public void Apply()
        {
            Cursor.lockState = LockMode;
            Cursor.visible = Visible;
        }
    }

    /// <summary>
    /// Settings for a specific game state.
    /// </summary>
    [Serializable]
    public class GameStateSettings
    {
        public string GameModeName;
        public CursorSettings CursorSettings;
    }
}
