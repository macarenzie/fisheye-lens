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
        MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 
            Camera.main.nearClipPlane));

        if (Input.GetMouseButtonDown(0) & spriteInfo.IsColliding)
        {
            IsDragging = !IsDragging;
            
        }

        if(IsDragging)
        {
            Ingrediant.transform.position = MousePos;
        }

        if(Input.GetMouseButtonUp(0))
        {
            IsDragging = false;
        }
    }
}
