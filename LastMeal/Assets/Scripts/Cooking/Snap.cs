using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    public List<Transform> snapPoints;
    public IngrediantManager ingrediantManager;
    public float snapRange = 0.5f;
    public float closestDistance;
    public Drag completeOrderDrag;

    // Must use update instead of start as the objects are being updated frequently
    void Update()
    {
        foreach(Drag dragabbles in ingrediantManager.spriteInfoList)
        {
            dragabbles.dragEndCallback = OnDragEnded;
        }
    }

    private void OnDragEnded(Drag draggable)
    {
        closestDistance = -1;
        Transform closestSnappingPoint = null;

        foreach(Transform snapPoint in snapPoints)
        {
            float currentDistance = Vector2.Distance(draggable.transform.localPosition, snapPoint.transform.localPosition);
            
            if(closestSnappingPoint == null || currentDistance < closestDistance)
            {
                closestSnappingPoint = snapPoint.transform;
                closestDistance = currentDistance;
            }
        }

        if(closestSnappingPoint != null && closestDistance <= snapRange)
        {
            draggable.transform.localPosition = closestSnappingPoint.localPosition;
            draggable.spriteInfo.IsSnapping = true;
            completeOrderDrag = draggable;
        }

        if(closestSnappingPoint != null && closestDistance > snapRange)
        {
            draggable.spriteInfo.IsSnapping = false;
            completeOrderDrag = draggable;
        }
    }
}
