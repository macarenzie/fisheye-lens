using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IngrediantManager : MonoBehaviour
{
    public List<Drag> spriteInfoList = new List<Drag>();
    public List<GameObject> buttonList = new List<GameObject>();

    public IngrediantManager manager;
    public Cook cooker;
    public CompleteOrder complete;

    public Canvas Parent;

    public Vector3 MousePos;

    // Creates a given sprite from whatever button is called
    public void AddSprite(Drag sprite, Vector3 pos)
    {
        Drag childObj = Instantiate(sprite, pos, Quaternion.identity);
        childObj.transform.SetParent(Parent.transform);
        spriteInfoList.Add(childObj);
        childObj.IsDragging = true;
        childObj.IsSingle = true;
    }

    // Clears all lists, resets bools, and destroys all sprites
    public void ClearList()
    {
        for (int i = 0; i < manager.spriteInfoList.Count; i++)
        {
            DestroyImmediate(this.manager.spriteInfoList[i].gameObject);
        }

        cooker.IsBread = false;
        cooker.IsTomato = false;
        cooker.IsBacon = false;
        cooker.IsEgg = false;
        cooker.IsCheese = false;
        cooker.IsLettuce = false;

        cooker.IsContraband = false;
        cooker.IsSalad = false;
        cooker.IsSandwich = false;
        cooker.IsSaladContra = false;
        cooker.IsSandwichContra = false;

        manager.spriteInfoList.Clear();
        cooker.IngrediantsCombining.Clear();
        complete.IngrediantsCompleting.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
           Camera.main.nearClipPlane));

        // Loop through the list to check each sprite for collision
        // Set sprite isColliding to true when collision exists
        if (spriteInfoList.Count != 0)
        { 
            foreach (Drag sprite in spriteInfoList)
            {
                // Iterate through the list checking for collisions
                if (AABBCheck(sprite.spriteInfo, MousePos))
                {
                    // Collision and stop iterating
                    sprite.spriteInfo.isColliding = true;

                    // Reset location        
                    break;
                }
                else
                {
                    // No collision
                    sprite.spriteInfo.isColliding = false;
                }
            }
        }

        if (spriteInfoList.Count != 0)
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                for (int j = 0; j < spriteInfoList.Count; j++)
                {
                    Reset(buttonList[i], spriteInfoList[j]);
                }
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

    public bool AABBRectCheck(Vector2 spriteA, Drag spriteB)
    {
        // Do check
        // Uses a method found in sprite info to get bounds of sprites

        if (spriteA.x < spriteB.spriteInfo.RectMax.x &&
            spriteA.x > spriteB.spriteInfo.RectMin.x &&
            spriteA.y < spriteB.spriteInfo.RectMax.y &&
            spriteA.y > spriteB.spriteInfo.RectMin.y)
        {                             
            // collision
            return true;
        }
        
        // No collision
        return false;
    }

    /// <summary>
    /// Moves sprites off of spawn buttons to a set position
    /// </summary>
    /// <returns></returns>
    public void Reset(GameObject buttonPos, Drag sprite)
    {
        if (AABBRectCheck(buttonPos.transform.position, sprite))
        {
            Debug.Log("AABB");
            sprite.transform.position = new Vector3(buttonPos.transform.position.x,
                buttonPos.transform.position.y - 1.0f, 0.0f);
        }
    }
}


