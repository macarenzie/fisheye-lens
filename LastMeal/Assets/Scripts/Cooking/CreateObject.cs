using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreateObject : MonoBehaviour
{
    public SpriteInfo spriteInfo;
    public GameObject ingrediant;
    public IngrediantManager manager;
    public GameObject spawner;

    // Creates a given ingrediant at a given spawn location onClick
    public void SpawnIngrediantSprite(Drag sprite)
    {
        manager.AddSprite(sprite, new Vector3(spawner.transform.position.x, 
            spawner.transform.position.y, spawner.transform.position.z - 1));
    }
}
