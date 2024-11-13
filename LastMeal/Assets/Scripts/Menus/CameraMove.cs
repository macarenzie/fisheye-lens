using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Moves the parented object smoothly along an animation curve from point A to point B.
/// Primarily used to move from the Counter to the Kitchen.
/// </summary>
/// Author(s): Andrew Jameison

public class CameraMove : MonoBehaviour
{
    private bool isAtCounter = true;
    private IEnumerator co;

    [Header("Lerp Movement")]
    [SerializeField] private AnimationCurve lerpCurve;

    [SerializeField] private Vector2 kitchenPos;
    [SerializeField] private Vector2 counterPos;

    [SerializeField] private float swapDuration;

    [Header("Canvas Info")]
    // Information to make the canvases visible only in the kitchen
    [SerializeField] private Canvas orderCan;
    [SerializeField] private Canvas cookCan;
    [SerializeField] private Canvas receiptCan;
    private bool receiptAppeared = false;

    // timer stuff, adjust later
    [SerializeField] private Timer timer;

    private void Update()
    {
        if (receiptCan.enabled == true)
        {
            receiptAppeared = true;
        }
    }

    /// <summary>
    /// Forcibly moves the camera back to the counter for the end of the order
    /// </summary>
    public void OrderComplete()
    {
        //SceneNav.Instance.SaveData(1, timer.timeRemaining);
        timer.ResetTimer();

        isAtCounter = true;

        StartCoroutine(LerpValue(transform.position, counterPos));
    }

    /// <summary>
    /// Handles the movement of parented camera
    /// </summary>
    /// <param name="input"></param>
    public void SwapView(InputAction.CallbackContext input)
    {
        if (input.performed && timer.inPlay && ButtonNavigation.menuNav == MenuNav.PlayGame) {
            Debug.Log(receiptAppeared);
            if (co != null) { StopCoroutine(co); }

            if (isAtCounter) {
                co = LerpValue(transform.position, kitchenPos);
                cookCan.enabled = true;
            }

            else {
                co = LerpValue(transform.position, counterPos);

                orderCan.enabled = true;
                if (receiptAppeared)
                {
                    receiptCan.enabled = true;
                }
            }

            StartCoroutine(co);

            isAtCounter = !isAtCounter;
        }
    }

    /// <summary>
    /// Uses math to provide a more smooth transition between start and end
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    IEnumerator LerpValue(Vector2 start, Vector2 end) {
        float timeElapsed = 0;

        while (timeElapsed < swapDuration) {
            float t = timeElapsed / swapDuration;

            t = lerpCurve.Evaluate(t);

            transform.position = Vector3.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);

            yield return null;
        }
    }
}
