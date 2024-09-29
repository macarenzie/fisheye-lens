using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum testtype
{
    simple,
    lerp
}
public class CameraMove : MonoBehaviour
{
    // Move the camera up and down
    // For testing purposes and showing off, have a switch statement that provides different ways to swap between views
    // Simple swap of positions
        // Kitchen position
        // Counter position

    // Lerping back and forth
        // Start point
        // End point
        // Current point
    [SerializeField] private testtype testView = testtype.simple;

    private IEnumerator co;

    [SerializeField] private Vector2 kitchenPos;
    [SerializeField] private Vector2 counterPos;

    private float lerpedValue;

    [SerializeField] private float swapDuration;

    private bool isAtCounter = true;

    public void swap_view(InputAction.CallbackContext input)
    {
        if (input.performed) {
            switch (testView) {

                // Choice A: snappy transition
                case testtype.simple: 
                {
                    if (isAtCounter) {
                        transform.position = kitchenPos;
                    }

                    else {
                        transform.position = counterPos;
                    }

                    isAtCounter = !isAtCounter;

                    break;
                }

                // Choice B: smooth transition
                case testtype.lerp:
                {
                    if (co != null) { StopCoroutine(co); }

                    if (isAtCounter) {

                        co = lerp_value(transform.position, kitchenPos);
                        StartCoroutine(co);
                    }

                    else {
                        co = lerp_value(transform.position, counterPos);
                        StartCoroutine(co);
                    }

                    isAtCounter = !isAtCounter;
                    break;
                }
            }
        }
    }

    IEnumerator lerp_value(Vector2 start, Vector2 end) {
        float timeElapsed = 0;

        while (timeElapsed < swapDuration) {
            float t = timeElapsed / swapDuration;
            transform.position = Vector3.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
    }
}
