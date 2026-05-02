using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

    public Scene startingScene;
    public Scene currentActiveScene;

    public Image fadeImage;
    [SerializeField] float fadeOutTime, fadeInTime, fullFadeTime;
    [SerializeField] Color fadeStartColor, fadeEndColor;
    [SerializeField] AnimationCurve fadeOutCurve, fadeInCurve;

    [Header("Cutscenes")]

    [SerializeField] GameObject playerUI;
    [SerializeField] GameObject deathCutsceneObject;
    [SerializeField] Animator guyShootsYouAnim;
    [SerializeField] Image flashOne, flashTwo;
    [SerializeField] AudioClip gunshotSound;
    [SerializeField] GameObject blackScreen;
    [SerializeField] AudioClip enemyFootsteps, doorOpenSound;

    [SerializeField] GameObject finalCutscene;
    [SerializeField] FinalConfrontationManager finalConfrontationManager;

    
    private void Awake()
    {
        instance = this;
    }

    private IEnumerator Start()
    {
        List<Scene> scenes = new List<Scene>(FindObjectsByType<Scene>());
        foreach (var scene in scenes)
        {
            if (scene.isActiveAndEnabled)
            {
                startingScene = scene;
            }
        }
        

        startingScene.gameObject.SetActive(true);
        currentActiveScene = startingScene;

        fadeImage.gameObject.SetActive(true);
        fadeImage.color = fadeEndColor;

        yield return new WaitForSeconds(2f);

        

        StartCoroutine(FadeInScene(1.5f));
    }

    public void ChangeSceneToNewScene(Scene newScene)
    {
        StartCoroutine(ChangeScene(newScene));
    }

    public IEnumerator ChangeScene(Scene newScene)
    {
        yield return null;


        AudioManager.instance.PlayScreenTransitionSound();

        yield return StartCoroutine(FadeOutScene());

        currentActiveScene.gameObject.SetActive(false);
        newScene.gameObject.SetActive(true);
        currentActiveScene = newScene;
        InfoTextManager.instance.StopShowingText();


        yield return new WaitForSeconds(fullFadeTime);

        yield return StartCoroutine(FadeInScene(fadeInTime));
    }

    public IEnumerator FadeOutScene()
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = fadeStartColor;

        for (float i = 0f; i < fadeOutTime; i += Time.deltaTime)
        {
            fadeImage.color = Color.Lerp(fadeStartColor, fadeEndColor, fadeOutCurve.Evaluate(i / fadeOutTime));
            yield return null;
        }

        fadeImage.color = fadeEndColor;
    }

    public IEnumerator FadeInScene(float fadeTime)
    {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = fadeEndColor;

        for (float i = 0f; i < fadeTime; i += Time.deltaTime)
        {
            fadeImage.color = Color.Lerp(fadeEndColor, fadeStartColor, fadeInCurve.Evaluate(i / fadeTime));
            yield return null;
        }

        fadeImage.color = fadeStartColor;
        fadeImage.gameObject.SetActive(false);
    }

    public void PlayDeathCutscene()
    {
        StopAllCoroutines();

        Timer.instance.StopTimer();
        StartCoroutine(DeathCutscene());
    }

    public void PlayDeathByDoorOpenCutscene()
    {
        Timer.instance.StopTimer();
        StartCoroutine(DeathCutsceneByDoorOpened());
    }

    public void PlayTrueEndCutscene(bool openedDoor)
    {
        Timer.instance.StopTimer();
        StartCoroutine(TrueEndingCutscene(openedDoor));
    }

    IEnumerator DeathCutsceneByDoorOpened()
    {
        yield return StartCoroutine(FadeOutScene());

        AudioManager.instance.Silence();
        playerUI.SetActive(false);
        deathCutsceneObject.SetActive(true);


        yield return new WaitForSeconds(1.5f);

        yield return StartCoroutine(FadeInScene(0.2f));

        guyShootsYouAnim.enabled = true;

        yield return new WaitForSeconds(1.7f);

        flashOne.gameObject.SetActive(true);

        AudioManager.instance.PlaySound(gunshotSound);

        yield return new WaitForSeconds(0.05f);

        flashOne.gameObject.SetActive(false);
        flashTwo.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.05f);

        blackScreen.SetActive(true);
        flashTwo.gameObject.SetActive(false);
        deathCutsceneObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
    }


    IEnumerator DeathCutscene()
    {
        yield return StartCoroutine(FadeOutScene());

        AudioManager.instance.Silence();

        playerUI.SetActive(false);
        deathCutsceneObject.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        AudioManager.instance.PlaySound(doorOpenSound);

        yield return new WaitForSeconds(1f);

        AudioManager.instance.PlaySound(enemyFootsteps);

        yield return new WaitForSeconds(4f);

        yield return StartCoroutine(FadeInScene(0.2f));

        guyShootsYouAnim.enabled = true;

        yield return new WaitForSeconds(1.7f);

        flashOne.gameObject.SetActive(true);

        AudioManager.instance.PlaySound(gunshotSound);

        yield return new WaitForSeconds(0.05f);

        flashOne.gameObject.SetActive(false);
        flashTwo.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.05f);

        blackScreen.SetActive(true);
        flashTwo.gameObject.SetActive(false);
        deathCutsceneObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneName);
    }

    IEnumerator TrueEndingCutscene(bool openedDoor)
    {

        yield return StartCoroutine(FadeOutScene());



        if (!openedDoor)
        {
            yield return new WaitForSeconds(1f);

            AudioManager.instance.PlaySound(doorOpenSound);
            playerUI.SetActive(false);

            yield return new WaitForSeconds(1.5f);

            AudioManager.instance.PlayFootsteopSound();

            yield return new WaitForSeconds(3f);
        }
        else
        {
            AudioManager.instance.PlaySound(doorOpenSound);
            playerUI.SetActive(false);
        }


        yield return new WaitForSeconds(2f);

        AudioManager.instance.Silence();
        
        finalCutscene.SetActive(true);


        yield return StartCoroutine(FadeInScene(0.2f));

        finalConfrontationManager.StartEverything();
    }

}
