using UnityEngine;

public class MirrorObject : MonoBehaviour
{
    public int currentDamageLevel;
    [SerializeField] GameObject ammoBehindTheMirror;
    [SerializeField] Collider2D mirrorCollider;
    [SerializeField] SpriteRenderer mirrorSpriteRend;
    [SerializeField] Sprite destroyedSprite;

    public void MirrorBroken()
    {
        ObjectInspectionManager.instance.EndInspect();
        mirrorCollider.enabled = false;
        mirrorSpriteRend.sprite = destroyedSprite;
        ammoBehindTheMirror.SetActive(true);
    }
}
