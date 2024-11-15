using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CompleteOrder : MonoBehaviour
{
    // Andrew Edit: just a temp bool getter for when the 
    // Allows the player to advance to final dialogue. 
    private static bool orderComplete = false;
    public static bool OrderComplete { get { return orderComplete; } }


    public List<Drag> IngrediantsCompleting = new List<Drag>();
    public IngrediantManager manager;
    public GameObject deSpawner;
    public Cook cookScript;
    public Receipt userOrder;


    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private Timer timer;
    
    public bool validCookedMeal;


    public void Finish()
    {
        validCookedMeal = CrossCheck();

        if (IngrediantsCompleting.Count == 1 && validCookedMeal && timer.inPlay)
        {
            cameraMove.OrderComplete();
            orderComplete = true;

            // For now just call the clear function
            manager.ClearList();
        }
        Debug.Log("ValidCookedMeal = " + validCookedMeal);

    }
    bool CrossCheck()
    {

        foreach (string order in userOrder.userOrder)
        {
            Debug.Log("UserOrder" + order);
        }


        //int userOrderCount = userOrder.userOrder.Count;
        //int cookedCount = cookScript.dictIngredientCooked.Count;
        //validCookedMeal = true;
        //int validCount = 0;

        ////for (int i = 0; i < cookedCount; i++) {
        ////    if (cookScript.dictIngredientCooked[i]Va == userOrder.userOrder[i]) {
        ////}
        //for (int i = 0; i < cookedCount; i++)
        //{
        //    if(cookScript.dictIngredientCooked.ElementAt(i).Value == true)
        //    {
        //        validCount++;
        //    }
        //}
        //if (validCount != userOrderCount)
        //{
        //    validCookedMeal = false;
        //}

        //foreach(string item in userOrder.userOrder)
        //{
        //    bool returnValue = true;
        //    returnValue = cookScript.dictIngredientCooked.TryGetValue(item, out returnValue);
        //    if(returnValue == false)
        //    {
        //        validCookedMeal = false;
        //    }
        //}


        int userOrderCount = userOrder.userOrder.Count;
        validCookedMeal = true;
        int userInputtedIngredientCount = cookScript.dictIngredientCooked.Count;


        // Check if all items in userOrder are cooked
        foreach (string item in userOrder.userOrder)
        {
            bool isCooked;
            if (!cookScript.dictIngredientCooked.TryGetValue(item, out isCooked) || !isCooked)
            {
                validCookedMeal = false;
                break;
            }
        }

        int ingredientCount = 0;
        ///way more ingredients than whats ordered
        foreach (var ingredient in cookScript.dictIngredientCooked)
        {
            if(ingredient.Value == true)
            {
                ingredientCount++;
            }
        }


        if (ingredientCount != userOrderCount ) 
        { 
            validCookedMeal = false;
            Debug.Log(ingredientCount);
        }

        Debug.Log(ingredientCount);

        Console.WriteLine(validCookedMeal);
        Debug.Log("Valid" + validCookedMeal);
        return validCookedMeal;
    }


    // Always check to see if there are objects in the combine list
    void Update()
    {
        // Iterate through all objects
        foreach (Drag sprite in manager.spriteInfoList)
        {
            // Determine if the object is already in the list
            if (sprite.spriteInfo.isCompleting & manager.AABBCheck(sprite.spriteInfo,
                new Vector2(deSpawner.transform.position.x, deSpawner.transform.position.y)))
            {
                break;
            }

            // Add to the list if the sprite is touching the tray and not already touching
            else if (manager.AABBCheck(sprite.spriteInfo,
                new Vector2(deSpawner.transform.position.x, deSpawner.transform.position.y)))
            {
                sprite.spriteInfo.isCompleting = true;
                IngrediantsCompleting.Add(sprite);
                break;
            }

            // Remove the object from the list if its off the tray
            else if (sprite.spriteInfo.isCompleting & !manager.AABBCheck(sprite.spriteInfo,
                new Vector2(deSpawner.transform.position.x, deSpawner.transform.position.y)))
            {
                sprite.spriteInfo.isCompleting = false;

                // Find the object in the ingrediants list and remove it
                foreach (Drag ingrediant in IngrediantsCompleting)
                {
                    if (sprite == ingrediant)
                    {
                        IngrediantsCompleting.Remove(ingrediant);
                        break;
                    }
                }
                break;
            }

        }
    }
}
