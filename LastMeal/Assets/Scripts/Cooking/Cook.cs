using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Cook : MonoBehaviour
{
    public List<Drag> RecipeList = new List<Drag>();
    public List<Drag> IngrediantsCombining = new List<Drag>();
    public IngrediantManager manager;
    public GameObject spawner;
    public Receipt receipt;

    //Triggers for finished dialogue
    bool contraTrigger = false;
    bool normalTrigger = false;
    [SerializeField]
    private TMP_Text gimnyText;
    [SerializeField]
    private GameObject textHolder;

    // Very poorly implemented cooking bools for recipes
    [SerializeField]
    bool isBread = false;
    [SerializeField]
    bool isLettuce = false;
    [SerializeField]
    bool isTomato = false;
    [SerializeField]
    bool isContraband = false;
    [SerializeField]
    bool isSandwich = false;
    [SerializeField]
    bool isSalad = false;

    //This bool determines (for now) if you are in the Sal or Gimny scene
    [SerializeField]
    bool isSal = false;

    public bool IsBread
    {
        set { isBread = value; }
    }

    public bool IsLettuce
    {
        set { isLettuce = value; }
    }

    public bool IsTomato
    {
        set { isTomato = value; }
    }

    public bool IsContraband
    {
        set { isContraband = value; }
    }

    public bool IsSandwhich
    {
        set { isSandwich = value; }
    }

    public bool IsSalad
    {
        set { isSalad = value; }
    }


    /// <summary>
    /// Sets the recipe based on input from the recipt
    /// </summary>
    public void RandomRecipe()
    {
        foreach(string ingrediant in receipt.userOrder)
        {

        }
        
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
            contraTrigger = true;
            textHolder.SetActive(true);
        }

        else if (isLettuce & isTomato & isContraband)
        {
            // Right now clear the table to prevent errors,
            // Refer to complete order code for how to delete specific items
            manager.ClearList();
            manager.AddSprite(RecipeList[3], new Vector3(spawner.transform.position.x,
                spawner.transform.position.y,
                -1));
            contraTrigger = true;
            textHolder.SetActive(true);
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
            normalTrigger = true;
            textHolder.SetActive(true);
        }

        else if (isLettuce & isTomato)
        {
            // Right now clear the table to prevent errors,
            // Refer to complete order code for how to delete specific items
            manager.ClearList();
            manager.AddSprite(RecipeList[2], new Vector3(spawner.transform.position.x,
                spawner.transform.position.y,
                -1));
            normalTrigger = true;
            textHolder.SetActive(true);
        }

        // Add contraband to a completed order
        else if(isSandwich & isContraband)
        {
            manager.ClearList();
            manager.AddSprite(RecipeList[1], new Vector3(spawner.transform.position.x,
                spawner.transform.position.y,
                -1));
            normalTrigger = false;
            contraTrigger = true;
            textHolder.SetActive(true);
        }

        else if (isSalad & isContraband)
        {
            manager.ClearList();
            manager.AddSprite(RecipeList[3], new Vector3(spawner.transform.position.x,
                spawner.transform.position.y,
                -1));
            normalTrigger = false;
            contraTrigger = true;
            textHolder.SetActive(true);
        }
    }   

    // Always check to see if there are objects in the combine list
    void Update()
    {
        if (contraTrigger)
        {
            if (isSal == false)
            {
                gimnyText.SetText("Thanks Chef, you're a lifesaver! Your good pal Gimny will put in a good word with the other inmates. You're gonna have a great time here man, I just know it!\n(YOU WIN||NEXT CUSTOMER)");
            }
            else
            {
                gimnyText.SetText("Now THAT is a salad, you've got a gift kid! Listen, anyone messes with you and you tell me, okay? I'll see you around kid, let Gimny know I said \"Salutations\".\nThank you for playing!");
            }

        }
        else if (normalTrigger)
        {
            if (isSal == false)
            {
                gimnyText.SetText("Thanks for the food Chef! Hopefully I'll be able to get that knife somewhere else. Have a good day man…\n(YOU WIN||NEXT CUSTOMER)");
            }
            else
            {
                gimnyText.SetText("You either can't catch hints, are stupid, or are really stupid. I got my eye on you Chef…\nThank you for playing!");
            }
            
        }

        // Iterate through all objects
        foreach(Drag sprite in manager.spriteInfoList)
        {

            // Add to the list if the sprite is touching the tray and not already touching
            if (!sprite.spriteInfo.IsCombining && sprite.spriteInfo.IsSnapping)
            {
                sprite.spriteInfo.isCombining = true;

                // determine what ingrediant it is and turn on the bool
                // Based off acceptable combining ingrediants from manager list

                // Bread bool
                if (sprite.GetComponent<SpriteRenderer>().sprite == 
                    manager.IngrediantList[0].GetComponent<SpriteRenderer>().sprite)
                {
                    isBread = true;
                    Debug.Log(isBread);
                }

                // Tomato bool
                else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                    manager.IngrediantList[1].GetComponent<SpriteRenderer>().sprite)
                {
                    isTomato = true;
                    Debug.Log(isTomato);
                }

                // Contraband bool
                else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                    manager.IngrediantList[2].GetComponent<SpriteRenderer>().sprite)
                {
                    isContraband = true;
                    Debug.Log(isContraband);
                }

                // Sandwich bool
                else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                    manager.IngrediantList[3].GetComponent<SpriteRenderer>().sprite)
                {
                    isSandwich = true;
                    Debug.Log(isSandwich);
                }

                else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                    manager.IngrediantList[4].GetComponent<SpriteRenderer>().sprite)
                {
                    isLettuce = true;
                    Debug.Log(isLettuce);
                }

                else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                    manager.IngrediantList[5].GetComponent<SpriteRenderer>().sprite)
                {
                    isSalad = true;
                    Debug.Log(isSalad);
                }
                IngrediantsCombining.Add(sprite);
                break;
            }

            // Remove the object from the list if its off the tray
            else if (sprite.spriteInfo.IsCombining & !sprite.spriteInfo.IsSnapping)
            {
                sprite.spriteInfo.IsCombining = false;

                // Find the object in the ingrediants list and remove it
                foreach (Drag ingrediant in IngrediantsCombining)
                {
                    if(sprite == ingrediant)
                    {
                        // Compares sprites to determine bool status
                        // Could potentially use this in click method
                        // Instead of bools
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

                        else if(sprite.GetComponent<SpriteRenderer>().sprite ==
                            manager.IngrediantList[3].GetComponent<SpriteRenderer>().sprite)
                        {
                            IsSandwhich = false;
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
