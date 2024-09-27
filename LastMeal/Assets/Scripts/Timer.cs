using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Class: Timer
/// Purpose: handles the logic behind the countdown timer for the cooking portion of the game
/// Author(s): McKenzie Lam
/// </summary>
public class Timer : MonoBehaviour
{
    // fields
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public TMP_Text timerText;
    
    void Start()
    {
        timerIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Order failed");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
        
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
