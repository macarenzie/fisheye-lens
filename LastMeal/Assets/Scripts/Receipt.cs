using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using System.Linq;
//using Unity.Mathematics;
using System.Security.Cryptography;

public class Receipt : MonoBehaviour
{
    [SerializeField] TMP_Text foodChoice;
    [SerializeField] TMP_Text ingredients;
    private Dictionary<string, string[]> recipeDict = new Dictionary<string, string[]>(); //store recipes and specific ingredients for that recipe
    public List<string> userOrder = new List<string>(); //will store the user order, will be used to confirm whats cooked is whats asked for
    private System.Random rng = new System.Random();
    private int recipeIndexNum;
    private int maxIngredientAmount = 3;
    private int totalNumRecipe = 3;
    // Start is called before the first frame update
    void Start()
    {
        GenerateRecipeChosen();
        ReadFileAndPopulateDict();
        GenerateUserOrder();
        DisplayUserOrder();

    }
    /// <summary>
    /// GenerateRandomRecipe with randomized ingredients
    /// </summary>
    private void DisplayUserOrder()
    {

        foodChoice.text = recipeDict.ElementAt(recipeIndexNum).Key; //get the random index key(recipe type)

        ingredients.text = null; //clear
        //print out ingredients of sandwich
        ingredients.text = "------------";
        foreach (string ingredient in userOrder)
        {
            ingredients.text += $"\n* {ingredient}";
        }
        ingredients.text += "\n------------";
    }
    private void GenerateRecipeChosen()
    {
        //choose recipe at random
        //recipeIndexNum = rng.Next(0, totalNumRecipe); //make sure the maxValue is the num of recipe 
        recipeIndexNum = 0; //make sure the maxValue is the num of recipe 

    }
    private void GenerateUserOrder()
    {
        List<string> tempIngredientList = new List<string>(); //create a tempList to store all ingredients in this recipe
        userOrder = new List<string>();//clears it
        foreach (string ingredient in recipeDict.ElementAt(recipeIndexNum).Value)
        {
            tempIngredientList.Add(ingredient);
        }
        int randNum;

        //sandwich, add bread as ingredient
        if (recipeIndexNum == 0) {
            userOrder.Add("Bread");
            userOrder.Add("Tomato");
        }

        //for (int i = 0; i < maxIngredientAmount; i++) { 
        //    randNum = rng.Next(0, tempIngredientList.Count);
        //    userOrder.Add(tempIngredientList[randNum]); //add ingredient to order
        //    //delete current ingredient so its not in the list again
        //    tempIngredientList.Remove(tempIngredientList[randNum]);
        //}
    }
    /// <summary>
    /// Read from fileName and populate private Dictionary<string,string> recipeDict
    /// </summary>
    private void ReadFileAndPopulateDict()
    {
        string fileName = "/Resources/Recipe.txt";
        StreamReader sr = null;

        try
        {
            Console.WriteLine("this happened");
            sr = new StreamReader(Application.dataPath + fileName); //declare new SR to read this textfile
            string line;
            while ((line = sr.ReadLine()) != null)//until we reach the end of the file
            {
                //first line has : which separates key and value so we split that first
                //ie (Sandwich:Cheese,Lettuce,Bacon)
                string[] keyValue = line.Split(':');
                string[] valueIngredients = keyValue[1].Split(','); //split the second portion of keyValue which is 'value'
                int i = 0;
                recipeDict.Add(keyValue[i], valueIngredients); //add recipe as key, ingredients as values to dictionary
                i++; //increment
                //Console.WriteLine(line); //test
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read!");
            Console.WriteLine(e.Message);
        }
        finally
        {
            if (sr != null) sr.Close();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
