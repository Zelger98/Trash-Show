using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoTextManager : MonoBehaviour
{
    public static InfoTextManager instance;
    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] GameObject scrollTextArrow;

    [SerializeField] TextInfo startingText;
    [SerializeField] InteractedSceenTransitioner scene;
    [SerializeField] Scene intitialScene, afterDeathScene;

    public TextInfo currentTextInfo;

    private Coroutine showTextForTimeCo;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (PersistantManager.instance.deathCount == 0)
        {
            scene.sceneToChangeTo = intitialScene;
        }
        else
        {
            scene.sceneToChangeTo = afterDeathScene;
        }

        scrollTextArrow.SetActive(false);

        StartCoroutine(ShowAfterDelay());
    }

    IEnumerator ShowAfterDelay()
    {
        yield return new WaitForSeconds(3.5f);

        if (PersistantManager.instance.deathCount == 0)
        {
            ShowText(startingText);
        }
    }

    public void ShowText(TextInfo text)
    {
        currentTextInfo = new TextInfo();
        currentTextInfo.textLine = new List<string>(text.textLine);
        textMesh.text = currentTextInfo.textLine[0];
        currentTextInfo.time = text.time;
        currentTextInfo.isTimed = text.isTimed;
        currentTextInfo.textLine.RemoveAt(0);

        CheckArrow();

        if (showTextForTimeCo != null)
        {
            StopCoroutine(showTextForTimeCo);
            showTextForTimeCo = null;
        }

        if (text.isTimed && currentTextInfo.textLine.Count == 0)
        {
            showTextForTimeCo = StartCoroutine(ShowTextForATime(currentTextInfo.time));
        }
    }

    public void ScrollText()
    {
        textMesh.text = currentTextInfo.textLine[0];
        currentTextInfo.textLine.RemoveAt(0);

        Debug.Log(currentTextInfo.isTimed + " " + currentTextInfo.textLine.Count);

        if (currentTextInfo.isTimed && currentTextInfo.textLine.Count == 0)
        {
            showTextForTimeCo = StartCoroutine(ShowTextForATime(currentTextInfo.time));
        }

        CheckArrow();
    }

    public void CheckArrow()
    {
        if (currentTextInfo.textLine.Count > 0)
        {
            scrollTextArrow.SetActive(true);
        }
        else
        {
            scrollTextArrow.SetActive(false);
        }
    }

    public void StopShowingText()
    {
        currentTextInfo = null;
        textMesh.text = "";
        scrollTextArrow.SetActive(false);
    }

    public IEnumerator ShowTextForATime(float time)
    {
        yield return new WaitForSeconds(time);

        StopShowingText();
        showTextForTimeCo = null;
    }
}

[System.Serializable]
public class TextInfo
{
    public List<string> textLine;
    public bool isTimed;
    public float time;
}

