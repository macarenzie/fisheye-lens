using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Moves the parented camera between the counter and the kitchen space
/// </summary>
public class CameraMove : MonoBehaviour
{
    [SerializeField] private AnimationCurve lerpCurve;

    [SerializeField] private Vector2 kitchenPos;
    [SerializeField] private Vector2 counterPos;

    [SerializeField] private float swapDuration;

    private bool isAtCounter = true;
    private IEnumerator co;

    /// <summary>
    /// Handles the movement of parented camera
    /// </summary>
    /// <param name="input"></param>
    public void swap_view(InputAction.CallbackContext input)
    {
        if (input.performed) {
            if (co != null) { StopCoroutine(co); }

            if (isAtCounter) {
                co = lerp_value(transform.position, kitchenPos);
            }

            else {
                co = lerp_value(transform.position, counterPos);
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

            yield return null;
        }
    }
}
