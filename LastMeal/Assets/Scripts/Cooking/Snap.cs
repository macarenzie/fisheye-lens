using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    public List<Transform> snapPoints;
    public List<Drag> draggableObjects;
    public float snapRange = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Drag dragabbles in draggableObjects)
        {
            dragabbles.dragEndCallback = OnDragEnded;
        }
    }

    private void OnDragEnded(Drag draggable)
    {
        float closestDistance = -1;
        Transform closestSnappingPoint = null;

        foreach(Transform snapPoint in snapPoints)
        {
            float currentDistance = Vector2.Distance(draggable.transform.localPosition, snapPoint.localPosition);
            
            if(closestSnappingPoint == null || currentDistance < closestDistance)
            {
                closestSnappingPoint = snapPoint;
                closestDistance = currentDistance;
            }
        }

        if(closestSnappingPoint != null && closestDistance <= snapRange)
        {
            draggable.transform.localPosition = closestSnappingPoint.localPosition;
        }
    }
}
