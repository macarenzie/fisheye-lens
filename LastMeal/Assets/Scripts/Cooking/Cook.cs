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
    [SerializeField]
    public AssetList allSprites;
    public Dictionary<string, bool> dictIngredientCooked = new Dictionary<string, bool>();


    //Triggers for finished dialogue
    bool contraTrigger = false;
    bool normalTrigger = false;
    [SerializeField]
    private TMP_Text gimnyText;
    [SerializeField]
    private GameObject textHolder;

    // Very poorly implemented cooking bools for recipes
    public bool isBread = false;
    public bool isLettuce = false;
    public bool isTomato = false;
    public bool isBacon = false;
    public bool isEgg = false;
    public bool isCheese = false;
    public bool isContraband = false;
    public bool isSandwich = false;
    public bool isSalad = false;
    public bool isSandwichContra = false;
    public bool isSaladContra = false;

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
    public bool IsBacon
    {
        set { isBacon = value; }
    }
    public bool IsEgg
    {
        set { isEgg = value; }
    }
    public bool IsCheese
    {
        set { isCheese = value; }
    }

    public bool IsContraband
    {
        set { isContraband = value; }
    }

    public bool IsSandwich
    {
        set { isSandwich = value; }
    }

    public bool IsSalad
    {
        set { isSalad = value; }
    }

    public bool IsSandwichContra
    {
        set { isSandwichContra = value; }
    }

    public bool IsSaladContra
    {
        set { isSaladContra = value; }
    }

    // Spawns food depending on the items that are
    // Currently works off a boolean system
    // Might cause problems if recipes have repeat ingrediants or overlapping recipes
    //
    // ERROR: There are sometimes errors when clearing the lists, reference values no longer existing is the problem
    // This could be a componding problem
    public void ConfirmIngredients()
    {
        dictIngredientCooked.Clear();
        dictIngredientCooked.Add("Bread", this.isBread);
        Debug.Log("Bread Added:" + this.isBread);

        dictIngredientCooked.Add("Tomato", this.isTomato);
        Debug.Log("Tomato Added:" + this.isTomato);

        dictIngredientCooked.Add("Egg", this.isEgg);
        Debug.Log("Egg Added:" + this.isEgg);

        dictIngredientCooked.Add("Cheese", this.isCheese);
        Debug.Log("Cheese Added:" + this.isCheese);

        dictIngredientCooked.Add("Lettuce", this.isLettuce);
        Debug.Log("Lettuce Added:" + this.isLettuce);

        dictIngredientCooked.Add("Bacon", this.isBacon);
        Debug.Log("Bacon Added:" + this.isBacon);

    }
    public void SpawnRecipeSprite()
    {
        
        if (isSandwich && isContraband)
        {
            ConfirmIngredients();

            manager.ClearList();
            manager.AddSprite(RecipeList[1], new Vector3(spawner.transform.position.x,
                spawner.transform.position.y,
                -1));
            normalTrigger = false;
            contraTrigger = true;
            textHolder.SetActive(true);
        }

        else if ((isSalad || (isCheese | isEgg | isLettuce | isTomato | isBacon)) && isContraband) 
        {
            ConfirmIngredients();

            manager.ClearList();
            manager.AddSprite(RecipeList[3], new Vector3(spawner.transform.position.x,
                spawner.transform.position.y,
                -1));
            normalTrigger = false;
            contraTrigger = true;
            textHolder.SetActive(true);
        }
        //Sandwich
        else if (isBread & !isContraband & (isCheese | isEgg | isLettuce | isTomato | isBacon))
        {
            ConfirmIngredients();

            // Right now clear the table to prevent errors,
            // Refer to complete order code for how to delete specific items
            manager.ClearList();
            manager.AddSprite(RecipeList[0], new Vector3(spawner.transform.position.x,
                spawner.transform.position.y,
                -1));
            contraTrigger = true;
            textHolder.SetActive(true);
        }

        //Sandwich w/ Contra
        else if (isBread & isContraband)
        {
            ConfirmIngredients();

            // Right now clear the table to prevent errors,
            // Refer to complete order code for how to delete specific items
            manager.ClearList();
            manager.AddSprite(RecipeList[1], new Vector3(spawner.transform.position.x,
                spawner.transform.position.y,
                -1));
            contraTrigger = true;
            textHolder.SetActive(true);
        }

        // Salad
        else if (!isSaladContra && !isSandwichContra && !isSandwich && !isBread && !isContraband && (isCheese | isEgg | isLettuce | isTomato))
        {
            ConfirmIngredients();

            // Right now clear the table to prevent errors,
            // Refer to complete order code for how to delete specific items
            manager.ClearList();
            manager.AddSprite(RecipeList[2], new Vector3(spawner.transform.position.x,
                spawner.transform.position.y,
                -1));
            normalTrigger = true;
            textHolder.SetActive(true);
        }

        //Salad w/ Contra
        else if (isSalad && isContraband && !isSandwich && !isBread)
        {
            ConfirmIngredients();

            // Right now clear the table to prevent errors,
            // Refer to complete order code for how to delete specific items
            manager.ClearList();
            manager.AddSprite(RecipeList[3], new Vector3(spawner.transform.position.x,
                spawner.transform.position.y,
                -1));
            normalTrigger = true;
            textHolder.SetActive(true);
        }



        //else
        //{
        //    manager.ClearList();
        //    manager.AddSprite(RecipeList[0], new Vector3(spawner.transform.position.x,
        //        spawner.transform.position.y,
        //        -1));
        //} //test
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
        foreach (Drag sprite in manager.spriteInfoList)
        {

            // Add to the list if the sprite is touching the tray and not already touching
            if (!sprite.spriteInfo.IsCombining && sprite.spriteInfo.IsSnapping)
            {
                sprite.spriteInfo.isCombining = true;

                // determine what ingrediant it is and turn on the bool
                // Based off acceptable combining ingrediants from manager list

                // Bread bool
                if (sprite.GetComponent<SpriteRenderer>().sprite ==
                    allSprites.listOfAllSprites[0])
                {
                    isBread = true;
                    Debug.Log(isBread);
                }

                // Tomato bool
                else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                    allSprites.listOfAllSprites[1])
                {
                    isTomato = true;
                    Debug.Log(isTomato);
                }
                // Egg bool
                else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                    allSprites.listOfAllSprites[2])
                {
                    isEgg = true;
                    Debug.Log(isTomato);
                }
                // Cheese bool
                else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                    allSprites.listOfAllSprites[3])
                {
                    isCheese = true;
                    Debug.Log(isTomato);
                }
                // Bacon bool
                else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                    allSprites.listOfAllSprites[4])
                {
                    isBacon = true;
                    Debug.Log(isTomato);
                }

                //Lettuce
                else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                    allSprites.listOfAllSprites[5])
                {
                    isLettuce = true;
                    Debug.Log(isLettuce);
                }

                // Contraband bool
                else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                    allSprites.listOfAllSprites[6])
                {
                    isContraband = true;
                    Debug.Log(isContraband);
                }

                // Sandwich bool
                else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                    allSprites.listOfAllSprites[7])
                {
                    isSandwich = true;
                    Debug.Log(isSandwich);
                }

                else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                    allSprites.listOfAllSprites[8])
                {
                    isSalad = true;
                    Debug.Log(isSalad);
                }

                // Sandwich Contra
                else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                    allSprites.listOfAllSprites[9])
                {
                    isSandwichContra = true;
                    Debug.Log(isSandwich);
                }

                //Salad Contra
                else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                    allSprites.listOfAllSprites[10])
                {
                    isSaladContra = true;
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
                    if (sprite == ingrediant)
                    {
                        // Compares sprites to determine bool status
                        // Could potentially use this in click method
                        // Instead of bools
                        // Compares sprites to determine bool status
                        // Could potentially use this in click method
                        // Instead of bools
                        if (sprite.GetComponent<SpriteRenderer>().sprite ==
                            allSprites.listOfAllSprites[0])
                        {
                            isBread = false;
                            Debug.Log(isBread);
                        }

                        // Tomato bool
                        else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                            allSprites.listOfAllSprites[1])
                        {
                            isTomato = false;
                            Debug.Log(isTomato);
                        }
                        // Egg bool
                        else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                            allSprites.listOfAllSprites[2])
                        {
                            isEgg = false;
                            Debug.Log(isTomato);
                        }
                        // Cheese bool
                        else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                            allSprites.listOfAllSprites[3])
                        {
                            isCheese = false;
                            Debug.Log(isTomato);
                        }
                        // Bacon bool
                        else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                            allSprites.listOfAllSprites[4])
                        {
                            isBacon = false;
                            Debug.Log(isTomato);
                        }

                        //Lettuce
                        else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                            allSprites.listOfAllSprites[5])
                        {
                            isLettuce = false;
                            Debug.Log(isLettuce);
                        }

                        // Contraband bool
                        else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                            allSprites.listOfAllSprites[6])
                        {
                            isContraband = false;
                            Debug.Log(isContraband);
                        }

                        // Sandwich bool
                        else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                            allSprites.listOfAllSprites[7])
                        {
                            isSandwich = false;
                            Debug.Log(isSandwich);
                        }

                        //Salad
                        else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                            allSprites.listOfAllSprites[8])
                        {
                            isSalad = false;
                            Debug.Log(isSalad);
                        }

                        // Sandwich Contra
                        else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                            allSprites.listOfAllSprites[9])
                        {
                            isSandwichContra = false;
                            Debug.Log(isSandwich);
                        }

                        //Salad Contra
                        else if (sprite.GetComponent<SpriteRenderer>().sprite ==
                            allSprites.listOfAllSprites[10])
                        {
                            isSaladContra = false;
                            Debug.Log(isSalad);
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



