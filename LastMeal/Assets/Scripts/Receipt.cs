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
       
        ingredients.text = null;
        if (foodChoice.text.Trim().ToLower() == "sandwich")
        {
            //print out ingredients of sandwich
            string tempString = "Bacon, Lettuce, Tomatoe"; //will add txt file
            string[] listOfIngredients = tempString.Split(',');
            foreach (string ingredient in listOfIngredients)
            {
                ingredients.text += $"  * {ingredient}\n";
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
