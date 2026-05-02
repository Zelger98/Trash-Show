using System.Collections.Generic;
using UnityEngine;

public class ToiletCloseUp : CloseUpObject
{
    [SerializeField] ToiletObject toilet;
    [SerializeField] SpriteRenderer cloggedSpriteRend;
    [SerializeField] GameObject key;
    [SerializeField] List<GameObject> clogObjects;
    [SerializeField] AudioClip putToiletPaperSound, fullSound;
    private void Start()
    {
        foreach (var obj in clogObjects)
        {
            obj.SetActive(false);
        }

        cloggedSpriteRend.enabled = false;

        toilet = closeUpObjectScript.GetComponent<ToiletObject>();
        key.SetActive(false);

        for (int i = 0; i < toilet.amountOfToiletPaperInside; i++)
        {
            clogObjects[i].SetActive(true);
        }

        if (toilet.amountOfToiletPaperInside == 3)
        {
            cloggedSpriteRend.enabled = true;

            if (toilet.keyInside)
            {
                key.SetActive(true);
            }
        }
    }

    public void AddToiletPaper()
    {
        toilet.amountOfToiletPaperInside++;
        clogObjects[toilet.amountOfToiletPaperInside - 1].SetActive(true);

        AudioManager.instance.PlaySound(putToiletPaperSound);

        if (toilet.amountOfToiletPaperInside == 3)
        {
            AudioManager.instance.PlaySound(fullSound);

            cloggedSpriteRend.enabled = true;
            key.SetActive(true);
            toilet.ChangeToClogged();
        }
    }

    public void TakeKey()
    {
        toilet.keyInside = false;
    }
}
