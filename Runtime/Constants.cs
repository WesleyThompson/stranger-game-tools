using UnityEngine;

namespace StrangerGameTools
{
    /// <summary>
    /// A class to hold constants used throughout the Stranger Game Tools package.
    /// </summary>
    public static class Constants
    {
        public const string CONTROL_SCHEME_KEYBOARDMOUSE = "KeyboardMouse";
        public const string CONTROL_SCHEME_GAMEPAD = "Gamepad";
        //Default settings
        public const float INPUT_MIN_THRESHOLD = 0.01f;
        //Math
        public const float MAX_DEGREES = 360f;
        //Physics
        public const string DEFAULT_LAYER_NAME = "Default";
        public const string PLAYER_LAYER_NAME = "Player";
        //Color
        public static readonly Color CLEAR_GREEN = new Color(0.0f, 1.0f, 0.0f, 0.35f);
        public static readonly Color CLEAR_RED = new Color(1.0f, 0.0f, 0.0f, 0.35f);
        //Game states
        public const string STATE_MAIN_MENU = "MainMenu";
        public const string STATE_GAME = "Game";
        public const string STATE_PAUSE = "Pause";
        public const string STATE_CREDITS = "Credits";
        public const string STATE_LOADING = "Loading";
    }
}

