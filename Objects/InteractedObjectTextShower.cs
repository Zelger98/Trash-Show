using UnityEngine;

public class InteractedObjectTextShower : MonoBehaviour
{
    public InteractedObjectTextUI textObject;
    //public string text;
    //[SerializeField] float textLifetime;
    //[SerializeField] Color textColor;

    [SerializeField] TextInfo textInfo;

    public void ShowText()
    {
        InfoTextManager.instance.ShowText(textInfo);

        //InteractedObjectTextUI newText = Instantiate(textObject, GameManager.instance.GetMouseScreenPosition(), Quaternion.identity);
        //newText.Initialize(text, textColor, textLifetime);
    }
}
