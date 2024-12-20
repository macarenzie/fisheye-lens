using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CreateObject : MonoBehaviour
{
    public Drag sprite;
    public IngrediantManager manager;
    public GameObject spawner;

    // Creates a given ingrediant at a given spawn location onClick
    public void SpawnIngrediantSprite()
    {
       manager.AddSprite(sprite, new Vector3(spawner.transform.position.x,
            spawner.transform.position.y, Input.mousePosition.z));
    }
}


