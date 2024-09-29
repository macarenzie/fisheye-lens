using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IngrediantManager : MonoBehaviour
{
    public List<GameObject> foodList = new List<GameObject> ();

    public List<SpriteInfo> spriteInfoList = new List<SpriteInfo> ();

    public IngrediantManager manager;

    Vector3 MousePos;
    SpriteInfo spriteInfo;

    public void AddSprite(SpriteInfo sprite, Vector3 pos)
    {
        spriteInfoList.Add(Instantiate(sprite));
    }

    public void AddObject(GameObject gameObject, Vector3 pos)
    {
        //foodList.Add(Instantiate(gameObject, pos, Quaternion.identity));
    }

    // Update is called once per frame
    void Update()
    {
        MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
           Camera.main.nearClipPlane));
        
        // Loop through the list to check each sprite for collision
        // Set sprite isColliding to true when collision exists
        foreach (SpriteInfo sprite in spriteInfoList)
        {
            // Iterate through the list checking for collisions
            if (AABBCheck(sprite, MousePos))
            {
                // Collision and stop iterating
                sprite.isColliding = true;
                
                // Reset location        
                break;
            }
            else
            {
                // No collision
                sprite.isColliding = false;
            }
        }
    }

    // Method for AABB collision
    public bool AABBCheck(SpriteInfo spriteA, Vector2 spriteB)
    {
        // Do check
        // Uses a method found in sprite info to get bounds of sprites

        if (spriteB.x < spriteA.RectMax.x &&
            spriteB.x > spriteA.RectMin.x &&
            spriteB.y < spriteA.RectMax.y &&
            spriteB.y > spriteA.RectMin.y)
        {
            // collision
            return true;
        }

        // No collision
        return false;
    }
}
