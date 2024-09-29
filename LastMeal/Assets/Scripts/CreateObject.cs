using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreateObject : MonoBehaviour
{
    public SpriteInfo spriteInfo;
    public GameObject ingrediant;
    public IngrediantManager manager;
    public Vector3 spawner;

    public void SpawnIngrediantSprite(SpriteInfo sprite)
    {
        manager.AddSprite(sprite, spawner);
    }

    public void SpawnIngrediantObject(GameObject ingrediant)
    {
        manager.AddObject(ingrediant, spawner);
    }
}
