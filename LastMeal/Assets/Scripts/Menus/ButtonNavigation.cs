using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public enum MenuNav
{
    PlayGame,
    
    Settings,
    Controls,
    Instructions,

    Success,
    Failure
}

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

    private MenuNav menuNav = MenuNav.PlayGame;

    [SerializeField] private Timer timer;

    #region BUTTON_EVENTS
    // A key-press event for entering the game menus
    public void KeyPause(InputAction.CallbackContext value)
    {
        if (value.performed && menuNav == MenuNav.PlayGame)
        {
            OpenMenu(MenuNav.Settings);
        }
    }

    // Button Events for menu navigation
    public void ResumeGame()
    {
        OpenMenu(MenuNav.PlayGame); 
    }

    public void OpenSettings()
    {
        OpenMenu(MenuNav.Settings);
    }

    public void OpenSuccess()
    {
        OpenMenu(MenuNav.Success);
    }

    public void OpenFailure()
    {
        OpenMenu(MenuNav.Failure);
    }

    public void OpenControls()
    {
        OpenMenu(MenuNav.Controls);
    }

    public void OpenInstructions() 
    {
        OpenMenu(MenuNav.Instructions);
    }
    #endregion

    /// <summary>
    /// Called by button events to be open a specific menu, prevents any lingering menus from overlapping
    /// </summary>
    /// <param name="value"></param>
    private void OpenMenu(MenuNav value)
    {
        if (menuNav == value)
        {
            Debug.Log("ButtonNavigation.cs: Tried to move to already active menu.");
            return;
        }

        // Where did we come from: what to turn off...
        switch(menuNav)
        {
            case MenuNav.PlayGame:
                timer.PauseTimer();
                break;

            case MenuNav.Settings:
                ActivateMenu(false, transform.GetChild(0), "Settings");
                break;

            case MenuNav.Success:
                ActivateMenu(false, transform.GetChild(1), "Success");
                break;

            case MenuNav.Failure:
                ActivateMenu(false, transform.GetChild(2), "Failure");
                break;

            case MenuNav.Controls:
                ActivateMenu(false, transform.GetChild(3), "Controls");
                break;

            case MenuNav.Instructions:
                ActivateMenu(false, transform.GetChild(4), "Instructions");
                break;

            default:
                Debug.Log("Still testing navigation tools, see FallThroughMenu instead...");
                break;
        }

        // Where are we going: What to activate...
        switch (value)
        {
            case MenuNav.PlayGame:
                timer.PauseTimer();
                break;

            case MenuNav.Settings:
                ActivateMenu(true, transform.GetChild(0), "Settings");
                break;

            case MenuNav.Success:
                ActivateMenu(true, transform.GetChild(1), "Success");
                break;

            case MenuNav.Failure:
                ActivateMenu(true, transform.GetChild(2), "Failure");
                break;

            case MenuNav.Controls:
                ActivateMenu(true, transform.GetChild(3), "Controls");
                break;

            case MenuNav.Instructions:
                ActivateMenu(true, transform.GetChild(4), "Instructions");
                break;

            default:
                Debug.Log("Still testing navigation tools, see FallThroughMenu instead...");
                break;
        }

        menuNav = value;
    }

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
    /// Button Event. Returns to the main menu ( navigation scene )
    /// </summary>
    public void main_menu()
    {
        SceneManager.LoadScene("Navigation");
    }

    private void ActivateMenu(bool activate, Transform child, string menuName)
    {
        if(child.gameObject != null && child.name == menuName) 
        {
            child.gameObject.SetActive(activate);
        }

        else
        {
            Debug.Log("ButtonNavigation.cs : Could not find " + menuName + " menu in GameMenus heirarchy");
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
