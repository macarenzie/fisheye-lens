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
/// </summary>
/// Author(s): McKenzie Lam, Andrew Jameison
public class Timer : MonoBehaviour
{
    #region FIELDS
    [SerializeField] protected TMP_Text timerText;
    [SerializeField] private GameObject menus;
    [SerializeField] protected float initialTime;

    public float timeRemaining { get; private set; }
    protected bool timerIsRunning;

    /// <summary>
    /// Whether an order is currently being worked on
    /// </summary>
    public bool inPlay { get; private set; }

    [Header("Lerp Movement")]
    [SerializeField] private AnimationCurve lerpCurve;

    private Vector2 inView;
    private Vector2 outOfView;
    [SerializeField] private float yShift = -100.0f;

    [SerializeField] private float swapDuration = 0.3f;

    private IEnumerator co;
    #endregion

    void Start()
    {
        outOfView = transform.localPosition;
        inView = transform.localPosition;
        inView.y += yShift;

        inPlay = false;
        timerIsRunning = false;
        timeRemaining = initialTime;

        ConvertTimeValue(initialTime);
    }

    void Update()
    {
        // if startTimer button clicked
        if (timerIsRunning)
        {    
            // determine if the timer ran out
            if (timeRemaining <= 0)
            {                
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
        SceneNav.Instance.SaveData(1, initialTime - timeRemaining);

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
            co = LerpValue(transform.localPosition, inView);
        }

        else
        {
            co = LerpValue(transform.localPosition, outOfView);
        }

        StartCoroutine(co);
    }

    /// <summary>
    /// Uses math to provide a more smooth transition between start and end
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    IEnumerator LerpValue(Vector2 start, Vector2 end)
    {
        float timeElapsed = 0;

        while (timeElapsed < swapDuration)
        {
            float t = timeElapsed / swapDuration;

            t = lerpCurve.Evaluate(t);

            transform.localPosition = Vector3.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
    }

    /// <summary>
    /// Called when all the orders in a day have been completed
    /// </summary>
    public void CompleteDay()
    {
        //ResetTimer();

        //if ( !SceneNav.Instance.LoadNextScene(1, timeRemaining) )
        //{
        //    menus.GetComponent<ButtonNavigation>().OpenSuccess();
        //}

        // Inefficent: a method that just calls a method from another script? Bad

        menus.GetComponent<ButtonNavigation>().PlayGame();
    }

    /// <summary>
    /// Reveals the failure menu under GameMenus
    /// </summary>
    private void OrderIncomplete()
    {
        ResetTimer();

        menus.GetComponent<ButtonNavigation>().OpenFailure();
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
