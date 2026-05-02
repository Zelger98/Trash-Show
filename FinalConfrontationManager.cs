using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Characters
{
    player,
    attacker,
    host
}


public class FinalConfrontationManager : MonoBehaviour
{
    [SerializeField] GameObject textboxUI;
    [SerializeField] TextMeshProUGUI dialogueTextMesh;
    [SerializeField] List<TextLine> allText, victoryText, defeatText, victoryTextExtra;
    [SerializeField] GameObject nextDialogueArrow;
    [SerializeField] GameObject playerGun, shootButton;
    [SerializeField] Animator enemyAnim;
    [SerializeField] AudioClip shotSound;
    [SerializeField] Image flashOne, flashTwo;
    [SerializeField] GameObject blackScreen;
    [SerializeField] ParticleSystem shotParticle, confettiParticle;
    [SerializeField] AudioClip audienceCheerSound;
    [SerializeField] bool enemyHasBeenShot, drawEnding, sequenceEnded;
    [SerializeField] Color startColor, endColor;

    [SerializeField] AudioClip talkingSound, talkingSound2, talkingSound3, waitTalkingSound;

    [SerializeField] List<GameObject> objectsToDisable;

    [SerializeField] TextLine impatiantLine1, impatiantLine2, impatiantLine3, finalLine, enemyShotTextLine;

    private Coroutine scrollTextCo, finalCountdownCo, initialCo;
    [SerializeField] bool didFinishExplanation;
    [SerializeField] GameObject goodEndingGo, badEndingGo;
    [SerializeField] AudioClip goodEndingSound, badEndingSound, threeSound, twoSound, oneSound, micSound;


    public void NextText()
    {
        if (sequenceEnded)
        {
            FinalCutsceneText();
        }
        else
        {
            PreFinalCutsceneText();
        }
    }

    public void FinalCutsceneText()
    {
        nextDialogueArrow.SetActive(false);

        if (allText.Count == 0)
        {
            StartCoroutine(FinalFade());
        }
        else
        {
            scrollTextCo = StartCoroutine(ScrollText(allText[0], 0.1f));
            allText.RemoveAt(0);
        }
    }

    public void PreFinalCutsceneText()
    {
        if (enemyHasBeenShot)
        {
            nextDialogueArrow.SetActive(false);
            SceneManager.instance.FadeOutScene();
            StartCoroutine(TransitionToFinalScene());

            if (didFinishExplanation)
            {
                allText = victoryText;

            }
            else
            {
                allText = victoryTextExtra;
            }

            sequenceEnded = true;
            return;
        }
        if (drawEnding)
        {
            SceneManager.instance.FadeOutScene();
            nextDialogueArrow.SetActive(false);
            StartCoroutine(TransitionToFinalScene());
            allText = defeatText;
            sequenceEnded = true;
            return;
        }

        if (allText.Count < 6)
        {
            didFinishExplanation = true;
        }

        if (allText.Count == 15)
        {
            AudioManager.instance.PlaySound(micSound);
        }
        
        nextDialogueArrow.SetActive(false);

        scrollTextCo = StartCoroutine(ScrollText(allText[0], 0.1f));
        allText.RemoveAt(0);

        if (allText.Count == 0)
        {
            nextDialogueArrow.SetActive(false);
            finalCountdownCo = StartCoroutine(FinalConfrontation());
        }
    }

    private void Start()
    {
        textboxUI.SetActive(false);
    }

    public void StartEverything()
    {
        initialCo = StartCoroutine(Initial());
        Timer.instance.StopTimer();
    }

    public IEnumerator Initial()
    {
        yield return new WaitForSeconds(0.25f);

        AudioManager.instance.PlaySound(waitTalkingSound);

        yield return new WaitForSeconds(0.25f);

        shootButton.SetActive(true);



        yield return new WaitForSeconds(1f);

        textboxUI.SetActive(true);
        scrollTextCo = StartCoroutine(ScrollText(allText[0], 0.1f));
        allText.RemoveAt(0);
    }

    public void StartFinalConfrontation()
    {
        finalCountdownCo = StartCoroutine(FinalConfrontation());
    }

    IEnumerator FinalConfrontation()
    {
        yield return null;

        yield return new WaitForSeconds(3f);

        scrollTextCo = StartCoroutine(ScrollText(impatiantLine1, 0.1f));


        yield return new WaitForSeconds(4f);

        scrollTextCo = StartCoroutine(ScrollText(impatiantLine2, 0.1f));


        yield return new WaitForSeconds(4f);

        scrollTextCo = StartCoroutine(ScrollText(impatiantLine3, 0.1f));

        yield return new WaitForSeconds(3f);

        AudioManager.instance.PlaySound(threeSound);
        dialogueTextMesh.text = "Three...";
        yield return new WaitForSeconds(1.5f);

        AudioManager.instance.PlaySound(twoSound);
        dialogueTextMesh.text = "Two...";
        yield return new WaitForSeconds(1.5f);

        AudioManager.instance.PlaySound(oneSound);
        dialogueTextMesh.text = "One...";
        yield return new WaitForSeconds(3f);

        LowerGun();
        shootButton.SetActive(false);

        drawEnding = true;
        StartCoroutine(ScrollText(finalLine, 0.1f));
    }

    public IEnumerator ScrollText(TextLine textLine, float time)
    {
        yield return null;

        string textString = textLine.text;
        string currentText = null;
        string blankText = null;

        int textNumber = 0;

        dialogueTextMesh.text = "";
        dialogueTextMesh.color = textLine.textColor;

        foreach (var item in textString)
        {
            if (item.Equals(' '))
            {
                blankText += " ";
            }
            else
            {
                blankText += "_";
            }
        }

        int current = 0;
        
        while (current <= textString.Length)
        {
            textNumber++;

            if (textNumber == 4)
            {
                if (textLine.character == Characters.player)
                {
                    AudioManager.instance.PlaySound(talkingSound);
                }
                if (textLine.character == Characters.attacker)
                {
                    AudioManager.instance.PlaySound(talkingSound2);
                }
                if (textLine.character == Characters.host)
                {
                    AudioManager.instance.PlaySound(talkingSound3);
                }

                textNumber = 0;
            }

            currentText = textString.Substring(0, current) + "<color=#00000000>" + blankText.Substring(current) + "</color>";
            current++;
            yield return new WaitForSeconds(0.02f);

            dialogueTextMesh.text = currentText;
        }

        if (textLine.skipToNext)
        {
            scrollTextCo = StartCoroutine(ScrollText(allText[0], 0f));
            allText.RemoveAt(0);
            yield break;
        }

        yield return new WaitForSeconds(0.1f);

        if (allText.Count == 0 && (sequenceEnded || drawEnding || enemyHasBeenShot))
        {
            nextDialogueArrow.SetActive(true);
        }

        if (allText.Count > 0)
        {
            nextDialogueArrow.SetActive(true);
        }
    }

    public void LowerGun()
    {
        StartCoroutine(LowerGunAnim());
    }

    public void ShootGun()
    {
        if (finalCountdownCo != null)
        {
            StopCoroutine(finalCountdownCo);
        }

        if (scrollTextCo != null)
        {
            StopCoroutine(scrollTextCo);
        }

        if (initialCo != null)
        {
            StopCoroutine(initialCo);
        }
        StartCoroutine(ShootGunAnim());
    }

    IEnumerator FinalFade()
    {
        yield return StartCoroutine(FadeToBlack());

        yield return new WaitForSeconds(3f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    IEnumerator TransitionToFinalScene()
    {
        yield return StartCoroutine(FadeToBlack());

        yield return new WaitForSeconds(2f);

        textboxUI.SetActive(false);
        dialogueTextMesh.text = "";

        foreach (var item in objectsToDisable)
        {
            item.SetActive(false);
        }

        if (enemyHasBeenShot)
        {
            badEndingGo.SetActive(true);

            AudioManager.instance.GetComponent<AudioSource>().clip = badEndingSound;
            AudioManager.instance.GetComponent<AudioSource>().Play();
        }
        else
        {
            goodEndingGo.SetActive(true);

            AudioManager.instance.GetComponent<AudioSource>().clip = goodEndingSound;
            AudioManager.instance.GetComponent<AudioSource>().Play();
        }

        yield return StartCoroutine(FadeIn());

        yield return new WaitForSeconds(3f);

        textboxUI.SetActive(true);


        StartCoroutine(ScrollText(allText[0], 0.1f));
        allText.RemoveAt(0);
    }

    IEnumerator LowerGunAnim()
    {
        for (int i = 0; i < 100; i++)
        {
            playerGun.transform.position -= new Vector3(0, 0.02f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator ShootGunAnim()
    {
        dialogueTextMesh.text = "";
        nextDialogueArrow.SetActive(false);

        shotParticle.Play();

        flashOne.gameObject.SetActive(true);

        AudioManager.instance.PlaySound(shotSound);

        yield return new WaitForSeconds(0.05f);

        flashOne.gameObject.SetActive(false);
        flashTwo.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.05f);

        flashTwo.gameObject.SetActive(false);

        if (enemyHasBeenShot)
        {
            yield break;
        }

        enemyHasBeenShot = true;
        enemyAnim.Play("ShotDown", 0, 0f);


        yield return new WaitForSeconds(2f);

        StartCoroutine(LowerGunAnim());
        shootButton.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        confettiParticle.Play();
        AudioManager.instance.PlaySound(audienceCheerSound);

        yield return new WaitForSeconds(0.5f);
        //nextDialogueArrow.SetActive(true);
        textboxUI.SetActive(true);
        StartCoroutine(ScrollText(enemyShotTextLine, 0.1f));
    }

    IEnumerator FadeToBlack()
    {
        blackScreen.SetActive(true);
        float fadeTime = 1f;
        for (float i = 0f; i < fadeTime; i += Time.deltaTime)
        {
            blackScreen.GetComponent<Image>().color = Color.Lerp(startColor, endColor, i / fadeTime);
            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        blackScreen.SetActive(true);
        float fadeTime = 1f;
        for (float i = 0f; i < fadeTime; i += Time.deltaTime)
        {
            blackScreen.GetComponent<Image>().color = Color.Lerp(endColor, startColor, i / fadeTime);
            yield return null;
        }

        blackScreen.SetActive(false);
    }
}

[System.Serializable]
public class TextLine
{
    public string text;
    public Color textColor;

    public bool skipToNext;
    public Characters character;
}

