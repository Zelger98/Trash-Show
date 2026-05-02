using System.Collections.Generic;
using UnityEngine;

public class InteractableContainer : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite closedSprite, openSprite;
    [SerializeField] bool disableColliderOnOpen;
    [SerializeField] Collider2D coll;
    [SerializeField] Collider2D openCollider;
    [SerializeField] List<GameObject> objectsInside;
    [SerializeField] bool canBeClosed;
    [SerializeField] AudioClip openSound, closeSound;

    private bool isOpen;

    private void Start()
    {
        foreach (var obj in objectsInside)
        {
            obj.SetActive(false);
        }
    }

    public void Interacted()
    {
        if (!isOpen)
        {
            OpenContainer();
        }
        else if (canBeClosed)
        {
            CloseContainer();
        }
    }

    public void OpenContainer()
    {
        if (openSound != null)
        {
            AudioManager.instance.PlaySound(openSound);
        }

        spriteRenderer.sprite = openSprite;
        isOpen = true;

        if (disableColliderOnOpen)
        {
            coll.enabled = false;
        }

        if (openCollider != null)
        {
            openCollider.enabled = true;
        }

        foreach (var obj in objectsInside)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

    public void CloseContainer()
    {
        if (closeSound != null)
        {
            AudioManager.instance.PlaySound(closeSound);
        }

        spriteRenderer.sprite = closedSprite;

        coll.enabled = true;
        openCollider.enabled = false;
        isOpen = false;

        foreach (var obj in objectsInside)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }
}
