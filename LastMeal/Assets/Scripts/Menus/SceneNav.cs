using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A Singleton scene manager that persists between scenes, and remembers saved data
/// </summary>
/// Author(s): Andrew Jameison
public class SceneNav : MonoBehaviour
{
    private static SceneNav _instance;
    public static SceneNav Instance { get { return _instance; } }

    // TODO: Replace dayIndex with an enum? would replace loading a scene by index
        // could also replace sceneName string in _days if figure out enum name --> string conversion
    private int dayIndex;

    /// <summary>
    /// Each scene is considered a day, and can store this Player's current save data about it
    /// </summary>
    /// 
    /// TODO: Save data between scenes
    private struct Day
    {
        public string sceneName;
        public float timeSpent;
        public int ordersComplete;

        // bool isCompleted - Player has completed this day should have perm data
        // bool isCurrent - what the current working day is, no permenant data on this one
        // How many orders completed
        // Average rating
        // Story choices made during this day, i.e whether you gave Sal contraband
    }

    // TODO: List of all the scenes in the game 
    private Day[] _days =
    {
        new() { sceneName = "Navigation" },
        new() { sceneName = "Main" },
        new() { sceneName = "SalScene" }
    };


    public void Awake()
    {
        // Preserves the Scene Navigation object and player data, never more than one
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Looks for the current active scene among the list of _days
            // TODO: Could this be replaced by active scene data in build menu? Seemed like it only remember the current active scene

            for (int i = 0; i < _days.Length; ++i)
            {
                if (_days[i].sceneName == SceneManager.GetActiveScene().name)
                {
                    dayIndex = i;
                    break;
                }
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);

            // TODO: If player has persistent save data, load that file in
        }
    }

    /// <summary>
    /// Moves to the next day in the game's sequence
    /// </summary>
    /// <returns>False upon finding the last scene</returns>
    public void LoadNextScene()
    {
        // TODO: adds any new data to long term memory

        // loads the next day / scene
        if (dayIndex + 1 < _days.Length)
        {
            SceneManager.LoadScene(_days[++dayIndex].sceneName);
        }
        else
        {
            // If last day, pull up winstate instead
            SceneManager.LoadScene("DayOneCons");
        }
    }

    /// <summary>
    /// Restarts the current day currently being played, resets data. Called upon fail states / restart button
    /// </summary>
    public void RestartDay()
    {
        // Restart the current day, erase any temporary data accumulated this round
        SceneManager.LoadScene(_days[dayIndex].sceneName);

        _days[dayIndex].timeSpent = 0.0f;
        _days[dayIndex].ordersComplete = 0;
    }

    /// <summary>
    /// Called in mainMenu for the player to advance to a specific  scene
    /// </summary>
    /// <param name="_dayIndex"></param>
    public void LoadSelectedScene(int _dayIndex)
    {
        // TODO: For players wanting to replay a days, should warn them that it will erase later days data for consistentcy
            // Perhaps a coroutine while waiting for a response? return ref to coroutine? 

        dayIndex = _dayIndex;

        SceneManager.LoadScene(_days[_dayIndex].sceneName);
    }

    /// <summary>
    /// Formats a string to be displayed at the end of the day
    /// </summary>
    /// <returns></returns>
    public string FormatSuccessText()
    {
        string formatted;

        int mealsServed = 0;
        float timeSpent = 0.0f;

        foreach(Day day in _days)
        {
            mealsServed += day.ordersComplete;
            timeSpent += day.timeSpent;
        }

        formatted = "Day: 1\n" +
                    "Meals Served: " + mealsServed + "\n" +
                    "Time Spent: " + MathF.Round(timeSpent, 2, MidpointRounding.AwayFromZero);

        return formatted;
    }

    /// <summary>
    /// Called when an order is complete to update this day's save data
    /// </summary>
    /// <param name="ordersComplete"></param>
    /// <param name="timeSpent"></param>
    public void SaveData(int ordersComplete, float timeSpent)
    {
        _days[dayIndex].timeSpent = timeSpent;
        _days[dayIndex].ordersComplete = ordersComplete;
    }

    /// <summary>
    /// Sends the player to the consequences screen
    /// </summary>
    public void LoadConsequences()
    {
        // TODO: Save long term data HERE, since this should feel like a checkpoint for the player

        //_dayIndex = 0; // Might also reset the "cycle:

        SceneManager.LoadScene("DayOneCons");
    }
}
