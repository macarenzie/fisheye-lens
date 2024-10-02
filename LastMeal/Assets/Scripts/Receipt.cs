using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Receipt : MonoBehaviour
{
    [SerializeField] TMP_Text foodChoice;
    [SerializeField] TMP_Text ingredients;
    // Start is called before the first frame update
    void Start()
    {
        foodChoice.text = "Sandwich";
        ingredients.text = null;
        if (foodChoice.text.Trim().ToLower() == "sandwich".Trim().ToLower())
        {
            //print out ingredients of sandwich
            string tempString = "Bread, Tomato"; //will add txt file
            string[] listOfIngredients = tempString.Split(',');
            ingredients.text = "------------";
            foreach (string ingredient in listOfIngredients)
            {
                ingredients.text += $"\n* {ingredient}";
            }
            ingredients.text += "\n------------";

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
