using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetList : MonoBehaviour
{
    [SerializeField]
    Sprite breadSprite;
    [SerializeField]
    Sprite tomatoSprite;
    [SerializeField]
    Sprite eggSprite;
    [SerializeField]
    Sprite cheeseSprite;
    [SerializeField]
    Sprite baconSprite;
    [SerializeField]
    Sprite lettuceSprite;

    [SerializeField]
    Sprite contrabandSprite;
    [SerializeField]
    Sprite sandwichSprite;
    [SerializeField]
    Sprite saladSprite;
    [SerializeField]
    Sprite sandwichSpriteContra;
    [SerializeField]
    Sprite saladSpriteContra;

    public List<Sprite> listOfAllSprites = new List<Sprite>();


    // Start is called before the first frame update
    void Start()
    {
        AddAllSprites();
    }

    /// <summary>
    /// bread 0
    /// tomatoe 1
    /// egg 2
    /// cheese 3
    /// bacon 4
    /// lettuce 5
    /// contra 6
    /// sandwich 7
    /// salad 8
    /// sandwich/contra 9
    /// salad/contra 10
    /// </summary>
    public void AddAllSprites()
    {
        listOfAllSprites.Clear();
        listOfAllSprites.Add(breadSprite);
        listOfAllSprites.Add(tomatoSprite);
        listOfAllSprites.Add(eggSprite);
        listOfAllSprites.Add(cheeseSprite);
        listOfAllSprites.Add(baconSprite);
        listOfAllSprites.Add(lettuceSprite);
        listOfAllSprites.Add(contrabandSprite);
        listOfAllSprites.Add(sandwichSprite);
        listOfAllSprites.Add(saladSprite);
        listOfAllSprites.Add(sandwichSpriteContra);
        listOfAllSprites.Add(saladSprite);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
