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
        timer.ResetTimer();

        isAtCounter = true;

        StartCoroutine(lerp_value(transform.position, counterPos));
    }

    /// <summary>
    /// Handles the movement of parented camera
    /// </summary>
    /// <param name="input"></param>
    public void swap_view(InputAction.CallbackContext input)
    {
        if (input.performed) {
            Debug.Log(receiptAppeared);
            if (co != null) { StopCoroutine(co); }

            if (isAtCounter) {
                co = lerp_value(transform.position, kitchenPos);
                cookCan.enabled = true;
                //orderCan.enabled = false;
                //receiptCan.enabled = false;

                //timer.ShowTimer(true);
            }

            else {
                co = lerp_value(transform.position, counterPos);
                //cookCan.enabled = false;
                orderCan.enabled = true;
                if (receiptAppeared)
                {
                    receiptCan.enabled = true;
                }

                //timer.ShowTimer(false);
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
    IEnumerator lerp_value(Vector2 start, Vector2 end) {
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
