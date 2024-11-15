using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text gimnyText;
    [SerializeField]
    private TMP_Text salText;
    [SerializeField]
    private TMP_Text prisonApprove;
    [SerializeField]
    private TMP_Text guardApprove;

    private void Start()
    {
        if (ConsTally.customConOne)
        {
            gimnyText.SetText("Gimny gave the knife to Sal, both are very happy. The guards are not...");
        }
        else
        {
            gimnyText.SetText("With no knife, Gimny got punched in the face. The guards are happy though.");
        }

        if (ConsTally.customConTwo)
        {
            salText.SetText("Helping Sal has boosted your rep tremendously, but the guards are none too happy.");
        }
        else if (ConsTally.customTwoFail)
        {
            salText.SetText("You awoke to a written message in your room: 'YOU'RE DEAD, Love Sal'.");
        }
        else
        {
            salText.SetText("Since you didn't help Sal, none of the prisoners want to talk to you. The guards think it's funny.");
        }

        prisonApprove.SetText("" + ConsTally.prisonApprove + "/20");
        guardApprove.SetText("" + ConsTally.guardApprove + "/20");

    }
}
