using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInfo : MonoBehaviour
{
    [SerializeField]
    Vector2 rectSize = Vector2.one;

    [SerializeField]
    SpriteRenderer renderor;

    public bool isColliding = false;
    public bool isCombining = false;
    public bool isCompleting = false;
    bool isSnapping = false;

    // Methods for all states
    public bool IsColliding
    {
        get { return isColliding; }
        set { isColliding = value; }
    }

    public bool IsCombining
    {
        get { return isCombining; }
        set { isCombining = value; }
    }

    public bool IsCompleting
    {
        get { return isCompleting; }
        set { isCompleting = value; }
    }

    public bool IsSnapping
    {
        get { return isSnapping; }
        set { isSnapping = value; }
    }

    // Gets the Minimum bounds
    public Vector2 RectMin
    {
        get { return new Vector2(transform.position.x - rectSize.x / 2, transform.position.y - rectSize.y / 2); }
    }

    // Gets the maximum bounds
    public Vector2 RectMax
    {
        get { return new Vector2(transform.position.x + rectSize.x / 2, transform.position.y + rectSize.y / 2); }
    }

    // Update is called once per frame
    void Update()
    {
        // Shows collision state, different for every state
        // Only use for debugging and testing
        //if (isColliding)
        //{
        //    renderor.color = Color.red;
        //}
        //if(isCombining)
        //{
        //    renderor.color = Color.blue;
        //}
        //if(isCompleting)
        //{
        //    renderor.color = Color.green;
        //}
        //else
        //{
        //    renderor.color = Color.white;
        //}
    }

    // Use selected when looking at a spcific object
    // Gizmo for regular
    private void OnDrawGizmosSelected()
    {
        // Stes color, needs to be first
        Gizmos.color = Color.blue;

        // Actual drawing, wireframe
        // Make sure variables in gizmo are the ones checking against
        //Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.DrawWireCube(transform.position, rectSize);
    }
}
