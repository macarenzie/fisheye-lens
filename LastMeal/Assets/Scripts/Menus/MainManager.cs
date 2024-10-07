using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the movement between scenes, and setting up the next scene based on where it just came from.
/// </summary>
/// Author(s): Andrew Jameison
public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    // Problem One: Reusing the main menu as an end state, want to make some items visible / not visible
        // Instead of performing the actions here, keep a refence to where the manager just came from
        // At the start of every scene, scripts can check what state they should be in based on that fact
        // Will add later, for right now only one way to move between scenes, starting a brand new level

    // Problem Two: The timer should control whether the game ends for now, should have some public methods to call and change the scene
        // If the player runs out of time, end the round, game over screen
        // If the player exits to main menu, main menu screen
        // If the player successfully completes an order, send them to the win-screen (same as game-over, with different message)

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void play_game()
    {
        SceneManager.LoadScene("Main");
    }

    public void main_menu()
    {
        SceneManager.LoadScene("Navigation");
    }

    public void quit_game()
    {
        Application.Quit();
    }
}
