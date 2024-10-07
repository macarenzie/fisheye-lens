using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ButtonNavigation : MonoBehaviour
{
    [SerializeField] private AnimationCurve lerpCurve;

    // The original and secondary positions the menu will lerp between respectively
    [SerializeField] private Vector2 positionA;
    [SerializeField] private Vector2 positionB;

    [SerializeField] private float swapDuration = 0.2f;

    private bool isHidden = true;
    private IEnumerator co;

    public void play_game()
    {
        SceneManager.LoadScene("Main");
    }

    public void main_menu()
    {
        SceneManager.LoadScene("Navigation");
    }

    public void quit_game()
    {
        Application.Quit();
    }

    /// <summary>
    /// Handles the movement of parented camera
    /// </summary>
    /// <param name="input"></param>
    public void swap_view(InputAction.CallbackContext input)
    {
        if (input.performed)
        {
            if (co != null) { StopCoroutine(co); }

            if (isHidden)
            {
                co = lerp_value(transform.position, positionB);
            }

            else
            {
                co = lerp_value(transform.position, positionA);
            }

            StartCoroutine(co);

            isHidden = !isHidden;
        }
    }

    /// <summary>
    /// Uses math to provide a more smooth transition between start and end
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    private IEnumerator lerp_value(Vector2 start, Vector2 end)
    {
        float timeElapsed = 0;

        while (timeElapsed < swapDuration)
        {
            float t = timeElapsed / swapDuration;

            t = lerpCurve.Evaluate(t);

            transform.position = Vector3.Lerp(start, end, t);
            timeElapsed += Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);

            yield return null;
        }
    }
}
