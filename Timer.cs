using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToDie;
    [SerializeField] TextMeshProUGUI textMesh;

    [SerializeField] float currentTime, currentTimeRounded;
    [SerializeField] AudioClip firstKnock, secondKnock, thirdKnock;
    [SerializeField] float firstKnockTime, secondKnockTime, thirdKnockTime;
    [SerializeField] InteractableObjectCloseUpSO gun, ammo, loadedGun;

    [SerializeField] GameObject transitionArrow;

    private bool firstKnockHappened, secondKnockHappened, thirdKnockHappened;
    private int items;

    public bool isPaused, gameStarted;

    public static Timer instance;
    

    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameStarted = true;
        
        if (PersistantManager.instance.deathCount == 0)
        {

        }
        else
        {
            transitionArrow.SetActive(true);
        }

        StartCoroutine(ResetAfterTime());
    }

    private void Update()
    {
        if (!isPaused && gameStarted)
        {
            currentTime -= Time.deltaTime;
        }


        if (currentTime < timeToDie - firstKnockTime && !firstKnockHappened)
        {
            AudioManager.instance.PlaySound(firstKnock);
            firstKnockHappened = true;

            if (PersistantManager.instance.deathCount == 0)
            {
                StartCoroutine(WaitAndSpawnArrow());
            }

            gameStarted = false;
        }

        if (currentTime < timeToDie - secondKnockTime && !secondKnockHappened)
        {
            AudioManager.instance.PlaySound(secondKnock);
            secondKnockHappened = true;
        }

        if (currentTime < timeToDie - thirdKnockTime && !thirdKnockHappened)
        {
            AudioManager.instance.PlaySound(thirdKnock);
            thirdKnockHappened = true;
        }
    }

    IEnumerator ResetAfterTime()
    {
        currentTime = timeToDie;

        while (currentTime >= 0f)
        {
            currentTimeRounded = Mathf.RoundToInt(currentTime);

            yield return null;

            if (isPaused)
            {
                textMesh.text = "Paused";
            }
            else
            {
                textMesh.text = "";
            }
        }


        foreach (var item in PlayerInventory.instance.inventorySlots)
        {
            if (item.itemInSlot == null)
            {
                continue;
            }

            Debug.Log(item.itemInSlot.interactableObjectCloseUpSO);
            
            if (item.itemInSlot.interactableObjectCloseUpSO == gun || item.itemInSlot.interactableObjectCloseUpSO == ammo)
            {
                items++;

                if (items == 2)
                {
                    SceneManager.instance.PlayTrueEndCutscene(false);
                    yield break;
                }
            }
            if (item.itemInSlot.interactableObjectCloseUpSO == loadedGun)
            {
                Debug.Log("Has Gun!");

                SceneManager.instance.PlayTrueEndCutscene(false);
                yield break;
            }
        }

        PersistantManager.instance.deathCount++;
        SceneManager.instance.PlayDeathCutscene();
    }

    IEnumerator WaitAndSpawnArrow()
    {
        yield return new WaitForSeconds(4f);
        transitionArrow.SetActive(true);
    }

    public void PauseTimer()
    {
        isPaused = true;
    }

    public void UnpauseTimer()
    {
        isPaused = false;
        gameStarted = true;
    }

    public void StopTimer()
    {
        isPaused = true;
        StopAllCoroutines();
    }
}
