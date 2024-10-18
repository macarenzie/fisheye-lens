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


    private void Start()
    {
        reader = new StreamReader("Assets/Text/" + dialogue.name + ".txt");
    }


    public void OnTextClick()
    {
        
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
                gimnyText.SetText(reader.ReadLine());
            }
        }
        
        switch (textStage)
        {
            case 4:
                textHolder.SetActive(false);
                normalScreen.enabled = false;
                cardScreen.enabled = true;
                break;
            case 6:
                checkedScreen.enabled = false;
                normalScreen.enabled = true;
                orderPaper.enabled = true;
                break;
            case 10:
                gimnyText.SetText("");
                textHolder.SetActive(false);
                if (reader.EndOfStream)
                {
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
                gimnyText.SetText("Hey Chef, I know you're just trying things out, but my card is good.");
                textHolder.SetActive(true);
            }
            else if (decisionButton.name == "ApproveButton")
            {
                gimnyText.SetText("Thanks Chef! By the way, make sure to memorize the id number of the prisoner. \nIt could come in handy!");
                cardScreen.enabled = false;
                checkedScreen.enabled = true;
                textHolder.SetActive(true);
            }
        }
        
    }
}
