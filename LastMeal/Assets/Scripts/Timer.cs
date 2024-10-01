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
    #region FIELDS
    // timer logic
    [SerializeField] protected float initialTime;
    private float timeRemaining;
    protected bool timerIsRunning;

    // ui
    /* used for initial testing
    [SerializeField] private Button startButton;
    [SerializeField] private Button resetButton;
    */
    [SerializeField] private Button sendOrderButton;
    [SerializeField] protected TMP_Text timerText;
    [SerializeField] private Canvas cookCan;
    #endregion

    void Start()
    {
        // set up timer
        timerIsRunning = false;
        timeRemaining = initialTime;

        // hook up the buttons to their methods
        // startButton.onClick.AddListener(startTimer);
        sendOrderButton.onClick.AddListener(SendOrder);
        // resetButton.onClick.AddListener(resetTimer);

        // print timer on screen
        DisplayTime(initialTime);
        timerText.gameObject.SetActive(false); // timer hidden until started
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
                DisplayTime(timeRemaining);
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

    #region METHODS
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ShowTimer(bool isVisible)
    {
        Debug.Log("ShowTimer");
        timerText.gameObject.SetActive(isVisible);
        StartTimer();
    }

    void StartTimer()
    {
        timerIsRunning = true;
        timerText.gameObject.SetActive(true);
    }

    void SendOrder()
    {
        timerIsRunning = false;
        timerText.text = "Order complete";
        Debug.Log("Order complete");
    }

    void ResetTimer()
    {
        // stop the timer from running
        timerIsRunning = false;

        // reset the time
        timeRemaining = initialTime;

        // update display
        DisplayTime(timeRemaining);
    }
    #endregion
}
