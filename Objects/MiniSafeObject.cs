using UnityEngine;

public class MiniSafeObject : MonoBehaviour, IInteractableByItem
{
    [SerializeField] SpriteRenderer spriteRend;
    [SerializeField] Sprite openSprite;
    [SerializeField] Collider2D safeCollider;
    [SerializeField] GameObject revolverInside;
    [SerializeField] AudioClip openingSound;

    public void UseItemOnObject()
    {
        spriteRend.sprite = openSprite;
        ObjectInspectionManager.instance.EndInspect();
        AudioManager.instance.PlaySound(openingSound);
        Debug.Log("yippie!");
        safeCollider.enabled = false;
        revolverInside.SetActive(true);
    }
}
