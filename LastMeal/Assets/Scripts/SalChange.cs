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
    private GameObject gameMenus;
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
    private string[] dialogueWords;
    private List<string> readWords = new List<string>();
    private float counter;
    private int wordRead = 0;
    private string newText;
    private bool textStart;


    /// <summary>
    /// Used when an order is begun, calls StartTimer
    /// </summary>
    [SerializeField] private Timer timer;

    private void Update()
    {
        if (textStart && wordRead < dialogueWords.Length)
        {
            counter += Time.deltaTime;
            if (counter >= 0.1)
            {
                readWords.Add(dialogueWords[wordRead]);
                newText += readWords[wordRead] + " ";
                wordRead++;
                counter = 0;
                gimnyText.SetText(newText);

            }
        }

    }


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
                if (readWords.Count > 0)
                {
                    readWords.Clear();
                    wordRead = 0;
                    newText = "";
                }

                if (checkedScreen.enabled == false)
                {
                    textStart = true;
                    dialogueWords = reader.ReadLine().Split(' ');
                }

                //gimnyText.SetText(reader.ReadLine());
            }
        }

        switch (textStage)
        {
            case 5:
                textHolder.SetActive(false);
                normalScreen.enabled = false;
                cardScreen.enabled = true;
                textStart = false;
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
                if (hasEnded == false)
                {
                    if (reader.EndOfStream)
                    {
                        hasEnded = true;
                        reader.Close();
                    }
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
                gimnyText.SetText("You have got to be kidding me Chef!\nYou dare spit in the face\nof Salvatore Fini?!\nI'll see you dead…");
                ConsTally.customTwoFail = true;
                ConsTally.prisonApprove -= 3;
                ConsTally.guardApprove -= 1;
                gameMenus.SetActive(true);
                failScreen.SetActive(true);
                normalScreen.enabled = true;
                cardScreen.enabled = false;
                textHolder.SetActive(true);
            }
            else if (decisionButton.name == "ApproveButton")
            {
                wordRead = 0;
                gimnyText.SetText("Thanks for doing your job properly. Now if you'd be so kind, I would like to have a salad. Oh and throw in extra utensils, if you catch my drift…");
                cardScreen.enabled = false;
                checkedScreen.enabled = true;
                textHolder.SetActive(true);
            }
        }

    }
}