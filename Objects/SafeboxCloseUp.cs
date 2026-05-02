using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeboxCloseUp : CloseUpObject
{
    [SerializeField] string buttonCombination;
    [SerializeField] InteractableObjectCloseUpSO openBoxSO;
    private string currentButtonCombination;

    [SerializeField] AudioClip buttonClick, endSequenceSound;
    [SerializeField] List<GameObject> objectsToDisable;
    [SerializeField] SpriteRenderer spriteRend;
    [SerializeField] Sprite openSprite;

    [SerializeField] LivingRoomKey key;

    private void Start()
    {
        key.gameObject.SetActive(false);

        if (closeUpObjectScript.TryGetComponent<SafeboxObject>(out SafeboxObject safebox))
        {
            if (safebox.hasBeenOpen)
            {
                foreach (GameObject obj in objectsToDisable)
                {
                    obj.SetActive(false);
                }

                spriteRend.sprite = openSprite;

                if (safebox.isKeyInside)
                {
                    key.gameObject.SetActive(true);
                    key.closeUp = closeUpObjectScript.GetComponent<SafeboxObject>();
                }
                else
                {
                    key.gameObject.SetActive(false);
                }
            }
        }
    }



    public void ButtonPressed(int index)
    {
        currentButtonCombination += index.ToString();
        AudioManager.instance.PlaySound(buttonClick);

        if (currentButtonCombination.Length == buttonCombination.Length)
        {
            AudioManager.instance.PlaySound(endSequenceSound);

            if (currentButtonCombination == buttonCombination)
            {
                Unlock();
            }
            else
            {
                currentButtonCombination = "";
            }
        }
    }

    public void Unlock()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
        closeUpObjectScript.GetComponent<SafeboxObject>().OpenUp();
        spriteRend.sprite = openSprite;
        key.gameObject.SetActive(true);
        key.closeUp = closeUpObjectScript.GetComponent<SafeboxObject>();

    }
}
