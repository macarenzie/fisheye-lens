using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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
    /// <summary>
    /// NOTE: previously set to a static variable to be referenced, unknown behavior would revert to PlayGame value.
    /// </summary>
    public MenuNav menuNav { get; private set; }

    [SerializeField] private Timer timer;

    private void Awake()
    {
        menuNav = MenuNav.PlayGame;
    }

    // A key-press event for entering the game menus
    public void KeyPause(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            if (menuNav == MenuNav.PlayGame)
            {
                OpenMenu(MenuNav.Settings);
            }

            else if (menuNav == MenuNav.Settings)
            {
                OpenMenu(MenuNav.PlayGame);
            }
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

        SceneNav.Instance.LoadNextScene();
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
        ConsTally.prisonApprove = 10;
        ConsTally.guardApprove = 10;
    }

    /// <summary>
    /// Moves the player to the consequences scene
    /// </summary>
    public void ToConsequences()
    {
        SceneNav.Instance.LoadConsequences();
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
                gameObject.SetActive(true);
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
                gameObject.SetActive(false);
                break;

            case MenuNav.Settings:
                ActivateMenu(true, transform.GetChild(0), "Settings");
                break;

            case MenuNav.Success:
                if (ActivateMenu(true, transform.GetChild(1), "Success"))
                {
                    // Really make sure the body is available to change the text 
                    if (transform.GetChild(1).GetChild(1).name == "Body")
                    {
                        TMP_Text textBox = transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>();

                        textBox.text = SceneNav.Instance.FormatSuccessText();
                    }

                    else
                    {
                        Debug.Log("ButtonNavigation.cs : Could not find text body in Success.");
                    }
                }
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
    /// Confirms and activates the correct menu
    /// </summary>
    /// <param name="activate">Turning on OR off the menu</param>
    /// <param name="child">The menu transform</param>
    /// <param name="menuName">Menu name to be compared to</param>
    /// <returns>Whether activation was successful</returns>
    private bool ActivateMenu(bool activate, Transform child, string menuName)
    {
        if(child.gameObject != null && child.name == menuName) 
        {
            child.gameObject.SetActive(activate);
            return true;
        }

        else
        {
            Debug.Log("ButtonNavigation.cs : Could not find " + menuName + " menu in GameMenus heirarchy.");
            return false;
        }
    }
}
