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

    public void OnTextClick()
    {
        textStage++;
        if (textStage > 6)
        {
            textStage = 6;
        }
        if (cardScreen.enabled == true)
        {
            textStage = 2;
        }
        switch (textStage)
        {
            case 1:
                gimnyText.SetText("Well since it’s your first day, I’ll help you through your first order! Consider it a welcoming gift from me to you. First you’ll have to check my Punch Card to see if it’s up to date. Make sure the day hasn’t been punched already, don’t wanna give people seconds. ");
                break;
            case 2:
                gimnyText.SetText("");
                normalScreen.enabled = false;
                cardScreen.enabled = true;
                break;
            case 3:
                checkedScreen.enabled = false;
                normalScreen.enabled = true;
                orderPaper.enabled = true;
                gimnyText.SetText("After you do that most people will tell you what they want to eat. It seems like your selection right now is a bit small, so I’ll just have a Sandwich. To make it, look down (Space) and begin moving the ingredients to the right cooking station (Click the Bread and move it to the Cutting Board)....");
                break;
            case 4:
                gimnyText.SetText("Oh and if you don’t mind, could you please put a knife in the sandwich. I owe Salvator Fini just one more and if he doesn’t get it, I don’t know what he’ll do. Hey it’s alright if you don’t give it to me, I understand completely. Don’t want to ruin your first day, haha!");
                break;
            case 5:
                gimnyText.SetText("(To put contraband in a dish, open the drawer on the bottom left of your cooking station and place it in the final product. Be careful, too much contraband may alert the guards and have lasting consequences for your cooking career)");
                break;
            //Case 6 is currently the final stage but will be changed when an ending is able to
            //be created
            case 6:
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
            }
            else if (decisionButton.name == "ApproveButton")
            {
                gimnyText.SetText("Thanks Chef, make sure to memorize the id number of the prisoner too. \nCould come in handy!");
                cardScreen.enabled = false;
                checkedScreen.enabled = true;
            }
        }
        
    }
}
