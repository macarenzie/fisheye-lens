using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public void Start()
    {
        TextMeshProUGUI textTemp = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        textTemp.enabled = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowText();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        HideText();
    }
    public void ShowText()
    {
        TextMeshProUGUI textTemp = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        textTemp.enabled = true;
    }
    public void HideText()
    {
        TextMeshProUGUI textTemp = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        textTemp.enabled = false;
    }
}


