using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// State of the visible menus during the main game loop
/// </summary>
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
public class ButtonNavigation : MonoBehaviour
{
    private MenuNav menuNav = MenuNav.PlayGame;

    [SerializeField] private Timer timer;


    // A key-press event for entering the game menus
    public void KeyPause(InputAction.CallbackContext value)
    {
        if (value.performed && menuNav == MenuNav.PlayGame)
        {
            OpenMenu(MenuNav.Settings);
        }
    }

    #region MENU_BUTTONS

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


    #region SCENE_BUTTONS
    /// <summary>
    /// Advance to the next day
    /// </summary>
    public void PlayGame()
    {
        // TEMP: If this is the last scene, instead call success state

        if (!SceneNav.Instance.LoadNextScene())
        {
            if (timer) { timer.ResetTimer(); }

            OpenMenu(MenuNav.Success);
        }
    }

    /// <summary>
    /// Reloads the current day
    /// </summary>
    public void Restart()
    {
        SceneNav.Instance.RestartDay();
    }

    /// <summary>
    /// Button Event. Returns to the main menu ( navigation scene )
    /// </summary>
    public void MainMenu()
    {
        // TODO: Eventually replace with SceneNav _days enum instead of 0

        SceneNav.Instance.LoadSelectedScene(0);
    }

    /// <summary>
    /// Button Event. Exits the application
    /// </summary>
    public void QuitGame()
    {
        // TODO: Save Player data to file for persistent gameplay

        Application.Quit();
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
}
