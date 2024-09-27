using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextChange : MonoBehaviour
{
    [SerializeField]
    private TMP_Text gimnyText;
    private int textStage = 0;

    public void OnTextClick()
    {
        textStage++;
        if (textStage > 5)
        {
            textStage = 5;
        }
        switch (textStage)
        {
            case 1:
                gimnyText.SetText("Well since it’s your first day, I’ll help you through your first order! Consider it a welcoming gift from me to you. First you’ll have to check my Punch Card to see if it’s up to date. Make sure the day hasn’t been punched already, don’t wanna give people seconds. ");
                break;
            case 2:
                gimnyText.SetText("After you do that most people will tell you what they want to eat. It seems like your selection right now is a bit small, so I’ll just have a Bread Sandwich. To make it, look down (Space) and begin moving the ingredients to the right cooking station (Click the Bread and move it to the Cutting Board).");
                break;
            case 3:
                gimnyText.SetText("Oh and if you don’t mind, could you please put a cigarette in the sandwich. I owe Salvator Fini just one more and if he doesn’t get it, I don’t know what he’ll do. Hey it’s alright if you don’t give it to me, I understand completely. Don’t want to ruin your first day, haha!");
                break;
            case 4:
                gimnyText.SetText("(To put contraband in a dish, open the drawer on the bottom left of your cooking station and place it in the final product. Be careful, too much contraband may alert the guards and have lasting consequences for your cooking career)");
                break;
            case 5:
                gimnyText.SetText("");
                break;
        }
    }
}
