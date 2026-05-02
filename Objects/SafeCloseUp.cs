using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SafeCloseUp : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentCodeText;
    [SerializeField] string code;
    [SerializeField] Color correctColor, neutralColor, incorrectColor;
    [SerializeField] AudioClip beepSound, buttonSound, openSound;

    private string currentCode;
    private bool isInDelay;

    public void Start()
    {
        currentCodeText.text = "";
    }

    public void KeypadNumberClicked(int keypadNumber)
    {
        if (isInDelay)
        {
            return;
        }

        AudioManager.instance.PlaySound(buttonSound);

        currentCode += keypadNumber.ToString();
        currentCodeText.text += "*";

        if (currentCode.Length == code.Length)
        {
            StartCoroutine(CheckCodeDelay());
        }
    }

    public void FinishCode()
    {
        if (currentCode == code)
        {
            UnlockSafe();
        }
        else
        {
            currentCode = "";
            currentCodeText.text = "";
        }
    }

    public void UnlockSafe()
    {
        SafeObject.instance.OpenSafe();
        AudioManager.instance.PlaySound(openSound);

    }

    public IEnumerator CheckCodeDelay()
    {
        isInDelay = true;

        yield return new WaitForSeconds(0.1f);

        if (currentCode == code)
        {
            currentCodeText.color = correctColor;
        }
        else
        {
            currentCodeText.color = incorrectColor;
        }

        for (int i = 0; i < 5; i++)
        {
            AudioManager.instance.PlaySound(beepSound);


            currentCodeText.enabled = false;
            yield return new WaitForSeconds(0.04f);
            currentCodeText.enabled = true;
            yield return new WaitForSeconds(0.04f);
        }

        currentCodeText.color = neutralColor;

        FinishCode();
        isInDelay = false;
    }
}
