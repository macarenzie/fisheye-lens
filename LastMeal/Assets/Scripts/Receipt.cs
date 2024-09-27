using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Receipt : MonoBehaviour
{
    public Text foodChoice;
    public Text ingredients;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(foodChoice.text == "Sandwich".Trim().ToLower())
        {
            //print out ingredients of sandwich
            string tempString = "Bacon, Lettuce, Tomatoe"; //will add txt file
            string[] listOfIngredients = tempString.Split(',');
            foreach(string ingredient in listOfIngredients)
            {
                ingredients.text += $"  * {ingredient}\n";
            }
            
        }
    }
}
