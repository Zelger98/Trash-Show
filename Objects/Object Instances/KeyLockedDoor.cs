using UnityEngine;

public class KeyLockedDoor : MonoBehaviour, IInteractableByItem
{
    [SerializeField] Collider2D collToDisable;
    [SerializeField] GameObject traversalArrowToEnable;
    [SerializeField] SpriteRenderer spriteRend;
    [SerializeField] Sprite openSprite;
    [SerializeField] AudioClip openingSound;

    public void UseItemOnObject()
    {
        AudioManager.instance.PlaySound(openingSound);
        collToDisable.enabled = false;
        traversalArrowToEnable.SetActive(true);
        spriteRend.sprite = openSprite;
        ObjectInspectionManager.instance.StopObjectInspect();
    }
}
