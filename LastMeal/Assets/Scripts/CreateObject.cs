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

    // Creates a given ingrediant at a given spawn location onClick
    public void SpawnIngrediantSprite(SpriteInfo sprite)
    {
        manager.AddSprite(sprite, spawner);
    }
}
