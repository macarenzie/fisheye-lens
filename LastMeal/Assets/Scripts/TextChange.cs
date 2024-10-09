using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Stuff to Add: Dialogue for receiving the right or wrong meal,
//dialogue for getting his contraband or not, ending screen as well

public class TextChange : MonoBehaviour
{
    //GimnyText is what dialogue is being displayed with textStage determining what
    //line is being spoken
    [SerializeField]
    private TMP_Text gimnyText;
    private int textStage = 0;
    [SerializeField]
    private Button decisionButton;
    [SerializeField]
    private SpriteRenderer normalScreen;
    [SerializeField]
    private SpriteRenderer cardScreen;
    [SerializeField]
    private SpriteRenderer checkedScreen;
    [SerializeField]
    private Canvas orderPaper;
    [SerializeField]
    private GameObject textHolder;

    public void OnTextClick()
    {
        textStage++;
        if (textStage > 7)
        {
            textStage = 7;
        }
        if (cardScreen.enabled == true)
        {
            textStage = 2;
        }
        switch (textStage)
        {
            case 1:
                gimnyText.SetText("Well since it's your first day, I'll help you through your first order! Consider it a welcoming gift from me to you. First youll have to check my Punch Card to see if it's up to date. Make sure the day hasn't been punched already, don't wanna give people seconds. \n(Today is R) ");
                break;
            case 2:
                gimnyText.SetText("");
                textHolder.SetActive(false);
                normalScreen.enabled = false;
                cardScreen.enabled = true;
                break;
            case 3:
                checkedScreen.enabled = false;
                normalScreen.enabled = true;
                orderPaper.enabled = true;
                gimnyText.SetText("After you do that most people will tell you what they want to eat. It seems like your selection right now is a bit small, so I'll just have a Sandwich. To make it, look down (Space) and begin moving the ingredients to the right cooking station (Click the Ingredients and move them to the Cutting Board)....");
                break;
            case 4:
                gimnyText.SetText("Oh and if you don't mind, could you please put a knife in the sandwich. I owe Salvator Fini just one more and if he doesn't get it, I don't know what he'll do. Hey it's alright if you don't give it to me, I understand completely. Don't want to ruin your first day, haha!....");
                break;
            case 5:
                gimnyText.SetText("(To put contraband in a dish, open the drawer on the bottom left of your cooking station and place it in the final product. Be careful, too much contraband may alert the guards and have lasting consequences for your cooking career)....");
                break;
            case 6:
                gimnyText.SetText("(For now: Spawn in each ingredient and place them in reverse order.\nSpawn: Bread, Tomato, Knife (Optional)\nPlace: Knife (Optional), Tomato, Bread");
                break;
            case 7:
                gimnyText.SetText("");
                break;

        }
    }

    public void ApprovalClick()
    {
        if (cardScreen.enabled == true)
        {
            if (decisionButton.name == "RefuseButton")
            {
                gimnyText.SetText("Hey Chef, I know you're just trying things out, but my card is good.");
                textHolder.SetActive(true);
            }
            else if (decisionButton.name == "ApproveButton")
            {
                gimnyText.SetText("Thanks Chef, make sure to memorize the id number of the prisoner too. \nCould come in handy!");
                cardScreen.enabled = false;
                checkedScreen.enabled = true;
                textHolder.SetActive(true);
            }
        }
        
    }
}
