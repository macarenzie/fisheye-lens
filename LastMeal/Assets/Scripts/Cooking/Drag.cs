using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Drag : MonoBehaviour
{
    public GameObject Ingrediant;
    public SpriteInfo spriteInfo;
    public Vector3 MousePos;

    [SerializeField]
    bool isDragging = false;
    static bool isSingle = false;

    public delegate void DragEndDelegate(Drag draggableObject);
    public DragEndDelegate dragEndCallback;

    private Vector3 mouseDragStartPosition;
    private Vector3 spriteDragStartPosition;

    public bool IsDragging
    {
        get { return isDragging; }
        set { isDragging = value; }
    }

    public bool IsSingle
    {
        get { return isSingle; }
        set { isSingle = value; }
    }


    private void Update()
    {
        // Always update mmouse position
        MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 
            Camera.main.nearClipPlane));

       // If the mouse is hovering, you can drag
       if (Input.GetMouseButtonDown(0) && spriteInfo.IsColliding && !IsSingle)
       {
           IsDragging = true;
           IsSingle = true;
       }

       // Update the position of dragged item
       if(IsDragging && IsSingle)
       {
           Ingrediant.transform.position = MousePos;
       }

       // Turn off drag ability
       if(Input.GetMouseButtonUp(0))
       {
           IsDragging = false;
           IsSingle = false;
           dragEndCallback(this);
       }
    }

  // private void OnMouseDown()
  // {
  //     IsDragging = true;
  //     IsSingle = true;
  //     mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
  //     spriteDragStartPosition = transform.localPosition;
  // }
  // 
  // private void OnMouseDrag()
  // {
  //     if (IsDragging && IsSingle)
  //     {
  //         transform.localPosition = spriteDragStartPosition + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPosition);
  //     }
  // }
  // 
  //private void OnMouseUp()
  //{
  //    IsDragging = false;
  //    IsSingle = false;
  //    dragEndCallback(this);
  //}
}  
