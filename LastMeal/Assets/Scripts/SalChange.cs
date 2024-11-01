using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

//Stuff to Add: Dialogue for receiving the right or wrong meal,
//dialogue for getting his contraband or not, ending screen as well

public class SalChange : MonoBehaviour
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
    private GameObject failScreen;
    [SerializeField]
    private Canvas orderPaper;
    [SerializeField]
    private GameObject textHolder;
    [SerializeField]
    private TextAsset dialogue;
    StreamReader reader;
    private bool hasEnded = false;


    /// <summary>
    /// Used when an order is begun, calls StartTimer
    /// </summary>
    [SerializeField] private Timer timer;


    public void OnTextClick()
    {
        if (textStage == 0)
        {
            reader = new StreamReader(Application.dataPath + "/Resources/" + dialogue.name + ".txt");
        }

        textStage++;

        if (textStage > 7)
        {
            textStage = 7;
        }
        else
        {
            if (cardScreen.enabled == true)
            {
                textStage = 5;
            }
            else
            {
                gimnyText.SetText(reader.ReadLine());
            }
        }

        switch (textStage)
        {
            case 5:
                textHolder.SetActive(false);
                normalScreen.enabled = false;
                cardScreen.enabled = true;
                break;
            case 6:
                checkedScreen.enabled = false;
                normalScreen.enabled = true;
                orderPaper.enabled = true;
                textHolder.SetActive(false);
                timer.StartTimer();
                break;
            case 7:
                // TODO: Currently, the player can just spam through the dialogue and advance the day.
                // Should have some requirements to advance to next textStage

                if (CompleteOrder.OrderComplete)
                {
                    timer.CompleteDay();
                }

                gimnyText.SetText("");
                textHolder.SetActive(false);
                if (reader.EndOfStream && hasEnded == false)
                {
                    hasEnded = true;
                    reader.Close();
                }
                break;

        }
    }

    public void ApprovalClick()
    {
        if (cardScreen.enabled == true)
        {
            if (decisionButton.name == "RefuseButton")
            {
                gimnyText.SetText("You have got to be kidding me Chef!\nYou dare spit in the face\nof Salvatore Fini?!\nI'll see you dead…\nThank you for playing, you failed!");
                failScreen.SetActive(true);
                normalScreen.enabled = true;
                cardScreen.enabled = false;
                textHolder.SetActive(true);
            }
            else if (decisionButton.name == "ApproveButton")
            {
                gimnyText.SetText("Thanks for doing your job properly. Now if you'd be so kind, I would like to have a salad. Oh and throw in extra utensils, if you catch my drift…");
                cardScreen.enabled = false;
                checkedScreen.enabled = true;
                textHolder.SetActive(true);
            }
        }

    }
}