using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cook : MonoBehaviour
{
    public List<SpriteInfo> RecipeList = new List<SpriteInfo>();
    public List<SpriteInfo> IngrediantsCombining = new List<SpriteInfo>();
    public IngrediantManager manager;
    public GameObject spawner;

    // Very poorly implemented cooking bools for recipes
    [SerializeField]
    bool isBread = false;
    [SerializeField]
    bool isTomato = false;
    [SerializeField]
    bool isContraband = false;

    public bool IsBread
    {
        set { isBread = value; }
    }

    public bool IsTomato
    {
        set { isTomato = value; }
    }

    public bool IsContraband
    {
        set { isContraband = value; }
    }

    // Spawns food depending on the items that are
    // Currently works off a boolean system
    // Might cause problems if recipes have repeat ingrediants or overlapping recipes
    //
    // ERROR: There are sometimes errors when clearing the lists, reference values no longer existing is the problem
    // This could be a componding problem
    public void SpawnRecipeSprite()
    {
        //Sandwich
        if (isBread & isTomato & isContraband)
        {
            // Right now clear the table to prevent errors,
            // Refer to complete order code for how to delete specific items
            manager.ClearList();
            manager.AddSprite(RecipeList[1], new Vector3(spawner.transform.position.x,
                spawner.transform.position.y,
                -1));            
        }
        
        // Contraband sandwich
        else if(isBread & isTomato)
        {
            // Right now clear the table to prevent errors,
            // Refer to complete order code for how to delete specific items
            manager.ClearList();
            manager.AddSprite(RecipeList[0], new Vector3(spawner.transform.position.x,
                spawner.transform.position.y,
                -1));            
        }
    }   

    // Always check to see if there are objects in the combine list
    void Update()
    {
        // Iterate through all objects
        foreach(SpriteInfo sprite in manager.spriteInfoList)
        {
            // Determine if the object is already in the list
            if (sprite.isCombining & manager.AABBCheck(sprite,
                new Vector2(spawner.transform.position.x, spawner.transform.position.y)))
            {
                break;
            }

            // Add to the list if the sprite is touching the tray and not already touching
            else if (manager.AABBCheck(sprite,
                new Vector2(spawner.transform.position.x, spawner.transform.position.y)))
            {
                sprite.isCombining = true;

                // determine what ingrediant it is and turn on the bool
                // Based off acceptable combining ingrediants from manager list

                // Bread bool
                if (sprite.GetComponent<SpriteRenderer>().sprite == 
                    manager.IngrediantList[0].GetComponent<SpriteRenderer>().sprite)
                {
                    isBread = true;
                }

                // Tomato bool
                else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                    manager.IngrediantList[1].GetComponent<SpriteRenderer>().sprite)
                {
                    isTomato = true;
                }

                // Contraband bool
                else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                    manager.IngrediantList[2].GetComponent<SpriteRenderer>().sprite)
                {
                    isContraband = true;
                }
                IngrediantsCombining.Add(sprite);
                break;
            }

            // Remove the object from the list if its off the tray
            else if (sprite.IsCombining & !manager.AABBCheck(sprite,
                new Vector2(spawner.transform.position.x, spawner.transform.position.y)))
            {
                sprite.IsCombining = false;

                // Find the object in the ingrediants list and remove it
                foreach (SpriteInfo ingrediant in IngrediantsCombining)
                {
                    if(sprite == ingrediant)
                    {
                        // Compares sprites to determine bool status
                        // Could potentially use this in click method
                        // Instead of bools
                        //ERROR: The check can be spotty in times of quickness
                        //need more playtesting for why this is
                        if (sprite.GetComponent<SpriteRenderer>().sprite ==
                            manager.IngrediantList[0].GetComponent<SpriteRenderer>().sprite)
                        {
                            isBread = false;
                        }

                        else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                            manager.IngrediantList[1].GetComponent<SpriteRenderer>().sprite)
                        {
                            isTomato = false;
                        }

                        else if(sprite.GetComponent<SpriteRenderer>().sprite ==
                            manager.IngrediantList[2].GetComponent<SpriteRenderer>().sprite)
                        {
                            isContraband = false;
                        }
                        IngrediantsCombining.Remove(ingrediant);
                        break;
                    }
                }
                break;
            }
            
        }
    }
}
