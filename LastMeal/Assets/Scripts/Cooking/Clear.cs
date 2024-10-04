using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{
    public IngrediantManager manager;

    // Deletes everything based manager clear
    public void DeleteIngrediants()
    {
        manager.ClearList();
    }
}
