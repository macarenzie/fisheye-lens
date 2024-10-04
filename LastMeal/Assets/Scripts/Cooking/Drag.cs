using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Drag : MonoBehaviour
{
    public GameObject Ingrediant;
    public SpriteInfo spriteInfo;
    public Vector3 MousePos;

    bool IsDragging = false;

    private void Update()
    {
        // Always update mmouse position
        MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 
            Camera.main.nearClipPlane));

        // If the mouse is hovering, you can drag
        if (Input.GetMouseButtonDown(0) & spriteInfo.IsColliding)
        {
            IsDragging = !IsDragging;
            
        }

        // Update the position of dragged item
        if(IsDragging)
        {
            Ingrediant.transform.position = MousePos;
        }

        // Turn off drag ability
        if(Input.GetMouseButtonUp(0))
        {
            IsDragging = false;
        }
    }
}
