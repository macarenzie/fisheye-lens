using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.VFX;

/// <summary>
/// Script: Timer
/// Purpose: handles the logic behind the countdown timer for the 
///          cooking portion of the game
/// Author(s): McKenzie Lam, Andrew Jameison
/// </summary>
public class Timer : MonoBehaviour
{
    #region FIELDS
    // timer logic
    [SerializeField] protected float initialTime;
    private float timeRemaining;
    protected bool timerIsRunning;
    private bool inPlay = false;

    //[SerializeField] private Button sendOrderButton;
    [SerializeField] protected TMP_Text timerText;
    //[SerializeField] private Canvas cookCan;

    [SerializeField] private GameObject menus;



    //private bool isHidden = true; // Replaced by timerIsRunning?
    private IEnumerator co;

    [Header("Lerp Movement")]
    [SerializeField] private AnimationCurve lerpCurve;

    [SerializeField] private Vector2 inView;
    [SerializeField] private Vector2 outOfView;

    [SerializeField] private float swapDuration;
    #endregion

    void Start()
    {
        // set up timer
        timerIsRunning = false;
        timeRemaining = initialTime;

        // hook up the buttons to their methods
        // startButton.onClick.AddListener(startTimer);
        // sendOrderButton.onClick.AddListener(SendOrder);
        // resetButton.onClick.AddListener(resetTimer);

        // print timer on screen
        ConvertTimeValue(initialTime);
        //timerText.gameObject.SetActive(false); // timer hidden until started
    }

    void Update()
    {
        // if startTimer button clicked
        if (timerIsRunning)
        {    
            // determine if the timer ran out
            if (timeRemaining <= 0)
            {
                Debug.Log("Order failed");
                
                timeRemaining = 0;

                OrderIncomplete();
            }

            timeRemaining -= Time.deltaTime;
            ConvertTimeValue(timeRemaining);
        }
    }

    #region METHODS
    /// <summary>
    /// Used to begin an order and coming back from the pause menu
    /// </summary>
    public void StartTimer()
    {
        Debug.Log("Timer Started");
        timerIsRunning = true;
        inPlay = true;
        MoveTimer();
    }

    /// <summary>
    /// Used to pause the timer, use ResetTimer when an order is complete
    /// </summary>
    public void PauseTimer()
    {        
        if (inPlay) 
        {
            timerIsRunning = !timerIsRunning;
            MoveTimer();
        }
    }

    /// <summary>
    /// Used when an order is complete, use StartTimer to begin the next order
    /// </summary>
    public void ResetTimer()
    {
        Debug.Log("Timer Ended");
        timeRemaining = initialTime;

        ConvertTimeValue(timeRemaining);

        timerIsRunning = false;
        inPlay = false;
        MoveTimer();
    }

    /// <summary>
    /// Moves the Timer out of view above the camera when not in use. Change timerIsRunning before calling MoveTimer
    /// </summary>
    private void MoveTimer()
    {
        if (co != null) { StopCoroutine(co); }

        if (timerIsRunning)
        {
            co = lerp_value(transform.localPosition, inView);
        }

        else
        {
            co = lerp_value(transform.localPosition, outOfView);
        }

        StartCoroutine(co);
    }

    /// <summary>
    /// Uses math to provide a more smooth transition between start and end
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    IEnumerator lerp_value(Vector2 start, Vector2 end)
    {
        float timeElapsed = 0;

        while (timeElapsed < swapDuration)
        {
            float t = timeElapsed / swapDuration;

            t = lerpCurve.Evaluate(t);

            transform.localPosition = Vector3.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;
            //transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);

            yield return null;
        }
    }

    /// <summary>
    /// Called when all the orders in a day have been completed
    /// </summary>
    public void CompleteDay()
    {
        ResetTimer();

        Debug.Log("Order complete");

        menus.GetComponent<ButtonNavigation>().OpenSuccess();
        //
        //// TODO: disable the ability to access the pause menu
        //
        //Transform Backdrop = menus.transform.GetChild(0);
        //Transform WinState = menus.transform.GetChild(2);
        //
        //if (Backdrop != null && WinState != null && Backdrop.name == "MenuBackdrop" && WinState.name == "Success") {
        //    Backdrop.gameObject.SetActive(true);
        //    WinState.gameObject.SetActive(true);
        //}
        //
        //else {
        //    Debug.Log("Timer.cs : Could not find Success menu in GameMenus heirarchy");
        //}

    }

    /// <summary>
    /// Reveals the failure menu under GameMenus
    /// </summary>
    private void OrderIncomplete()
    {
        // TODO: disable the ability to access the pause menu

        menus.GetComponent<ButtonNavigation>().OpenFailure();

        //timerIsRunning = false;
        //
        //// Looks for two children within the GameMenus object to activate
        //Transform Backdrop = menus.transform.GetChild(0);
        //Transform FailState = menus.transform.GetChild(3);
        //
        //if (Backdrop != null && FailState != null && Backdrop.name == "MenuBackdrop" && FailState.name == "Failure") {
        //    Backdrop.gameObject.SetActive(true);
        //    FailState.gameObject.SetActive(true);
        //}
        //
        //else {
        //    Debug.Log("Timer.cs : Could not find Failure menu in GameMenus heirarchy");
        //}
    }

    /// <summary>
    /// Parses the time from seconds into a minute : second timer display
    /// </summary>
    /// <param name="timeToDisplay"></param>
    private void ConvertTimeValue(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    #endregion
}
