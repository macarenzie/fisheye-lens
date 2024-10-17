using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


/// <summary>
/// Transitions between menus and scenes using events, most connected to button events
/// </summary>
/// Author(s): Andrew Jameison
/// 

public class ButtonNavigation : MonoBehaviour
{
    /// <summary>
    /// Should be "Main" when building, or "AndrewScene" when testing
    /// </summary>
    private const string game_scene = "AndrewScene";

    /// <summary>
    /// Button Event. Loads up the game
    /// </summary>
    public void play_game()
    {
        // TODO: Currently used to reset the game after fail-condition, does this cause some overhead?
            // Perhaps change to resetting individual gameobjects, not sure...

        SceneManager.LoadScene(game_scene);

        prepare_game();
    }

    /// <summary>
    /// Will be used to make any changes between scenes
    /// </summary>
    public void prepare_game()
    {
        // If you ever need some specific thing to happen when the game resets, put it into this method

        // TODO: Eventually pass in saved data and what day the player is currently on 
    }

    /// <summary>
    /// Button Event. Returns to the main menu / navigation scene
    /// </summary>
    public void main_menu()
    {
        SceneManager.LoadScene("Navigation");
    }

    /// <summary>
    /// Opens the pause menu during the game.
    /// </summary>
    public void reveal_menu()
    {
        // The first two children under the 
        Transform Backdrop = transform.GetChild(0);
        Transform Settings = transform.GetChild(1);

        if (Backdrop != null && Settings != null && Backdrop.name == "MenuBackdrop" && Settings.name == "Settings") {
            Backdrop.gameObject.SetActive(true);
            Settings.gameObject.SetActive(true);
        }

        else {
            Debug.Log("Timer.cs : Could not find Settings menu in GameMenus heirarchy");
        }
    }

    /// <summary>
    /// Button Event. Exits the application
    /// </summary>
    public void quit_game()
    {

        Application.Quit();
    }
}
