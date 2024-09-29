using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonNavigation : MonoBehaviour
{
    public void play_game()
    {
        SceneManager.LoadScene("AndrewScene");
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
