using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

/// <summary>
/// Script: Timer
/// Purpose: handles the logic behind the countdown timer for the 
///          cooking portion of the game
/// Author(s): McKenzie Lam
/// </summary>
public class Timer : MonoBehaviour
{
    // FIELDS -----------------------------------------------------------------
    
    // timer logic
    [SerializeField] protected float initialTime;
    private float timeRemaining;
    protected bool timerIsRunning;
    
    // ui
    [SerializeField] private Button startButton;
    [SerializeField] private Button sendOrderButton;
    [SerializeField] private Button resetButton;
    [SerializeField] protected TMP_Text timerText;


    void Start()
    {
        // set up timer
        timerIsRunning = false;
        timeRemaining = initialTime;

        // hook up the buttons to their methods
        startButton.onClick.AddListener(startTimer);
        sendOrderButton.onClick.AddListener(sendOrder);
        resetButton.onClick.AddListener(resetTimer);

        // print timer on screen
        displayTime(initialTime);
    }

    void Update()
    {
        // if startTimer button clicked
        if (timerIsRunning)
        {
            // start the timer if it hasn't finished yet
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                displayTime(timeRemaining);
            }
            
            // determine if the timer ran out
            else
            {
                Debug.Log("Order failed");
                timerText.text = "Order failed";
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
        
    }

    // METHODS ----------------------------------------------------------------
    void displayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void startTimer()
    {
        timerIsRunning = true;
    }

    void sendOrder()
    {
        timerIsRunning = false;
        timerText.text = "Order complete";
    }

    void resetTimer()
    {
        // stop the timer from running
        timerIsRunning = false;

        // reset the time
        timeRemaining = initialTime;

        // update display
        displayTime(timeRemaining);
    }
}
