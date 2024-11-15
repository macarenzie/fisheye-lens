using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
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
        if (textStage > 10)
        {
            textStage = 10;
        }
        else
        {
            if (cardScreen.enabled == true)
            {
                textStage = 4;
            }
            else
            {
                if (readWords.Count > 0)
                {
                    readWords.Clear();
                    wordRead = 0;
                    newText = "";
                }

                if (textStage != 4)
                {
                    dialogueWords = reader.ReadLine().Split(' ');
                    textStart = true;
                }
                //gimnyText.SetText(reader.ReadLine());
            }
        }
        
        switch (textStage)
        {
            case 4:
                textHolder.SetActive(false);
                normalScreen.enabled = false;
                cardScreen.enabled = true;
                textStart = false;
                break;
            case 5:
                checkedScreen.enabled = false;
                normalScreen.enabled = true;
                
                break;
            case 9:
                orderPaper.enabled = true;
                timer.StartTimer();
                break;
            case 10:
                
                // TODO: Currently, the player can just spam through the dialogue and advance the day.
                    // Should have some requirements to advance to next textStage
                if (CompleteOrder.OrderComplete)
                {
                    timer.CompleteDay();
                }

                gimnyText.SetText("");
                textHolder.SetActive(false);
                textStart = false;
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
                gimnyText.SetText("Hey Chef, I know you're just trying things out, but my card is good.");
                textHolder.SetActive(true);
            }
            else if (decisionButton.name == "ApproveButton")
            {
                gimnyText.SetText("Thanks Chef! By the way, make sure to memorize the id number of the prisoner. \nIt could come in handy!");
                wordRead = 0;
                cardScreen.enabled = false;
                checkedScreen.enabled = true;
                textHolder.SetActive(true);
            }
        }
        
    }
}
