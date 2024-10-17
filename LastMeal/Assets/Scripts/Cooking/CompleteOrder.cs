using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteOrder : MonoBehaviour
{
    public List<Drag> IngrediantsCompleting = new List<Drag>();
    public IngrediantManager manager;
    public GameObject deSpawner;

    public void Finish()
    {
        // Temporary untl we decide what to do with this
        if(IngrediantsCompleting.Count == 1)
        {
            // For now just call the clear function
            manager.ClearList();
        }
        
        //// Go through all completed orders and destory them
        //if (IngrediantsCompleting.Count == 1)
        //{
        //    foreach (SpriteInfo finish in IngrediantsCompleting)
        //    {
        //        // Remove those objects from the normal ingrediants list
        //        foreach (SpriteInfo ingrediant in manager.spriteInfoList)
        //        {
        //            if (finish == ingrediant)
        //            {
        //                manager.spriteInfoList.Remove(ingrediant);
        //                break;
        //            }
        //        }
        //        GameObject temp = finish.gameObject;
        //        IngrediantsCompleting.Remove(finish);
        //        Destroy(temp);
        //    }
        //    IngrediantsCompleting.Clear();
        //}
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
