using System;
using StrangerGameTools;
using UnityEngine;

public class GameStateListenerTest : MonoBehaviour
{
    void Awake()
    {
        GameStateManager.OnEnterMainMenu += OnEnterMainMenu;
        GameStateManager.OnExitMainMenu += OnExitMainMenu;
        GameStateManager.OnEnterGame += OnEnterGame;
        GameStateManager.OnExitGame += OnExitGame;
    }

    private void OnExitGame()
    {
        Debug.Log("Exiting Game State");
    }

    private void OnExitMainMenu()
    {
        Debug.Log("Exiting Main Menu State");
    }

    private void OnEnterMainMenu()
    {
        Debug.Log("Entering Main Menu State");
    }

    private void OnEnterGame()
    {
        Debug.Log("Entering Game State");
    }
}
