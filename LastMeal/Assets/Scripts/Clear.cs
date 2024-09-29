using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{
    public IngrediantManager manager;

    public void DeleteIngrediants()
    {
        for(int i = 0; i < manager.foodList.Count; i++)
        {
            DestroyImmediate(this.manager.foodList[i].gameObject, true);
            DestroyImmediate(this.manager.spriteInfoList[i], true);
        }
        manager.foodList.Clear();
        manager.spriteInfoList.Clear();
    }
}
