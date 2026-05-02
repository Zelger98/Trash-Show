using UnityEngine;

public class FireAxeBox : MonoBehaviour
{
    [SerializeField] SpriteRenderer boxRenderer;
    [SerializeField] Collider2D boxCollider, axeCollider;
    [SerializeField] Sprite openSprite;
    [SerializeField] AudioClip openSound;
    public void OpenUp()
    {
        boxCollider.enabled = false;
        axeCollider.enabled = true;
        boxRenderer.sprite = openSprite;

        AudioManager.instance.PlaySound(openSound);
    }
}
