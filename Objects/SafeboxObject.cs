using UnityEngine;

public class SafeboxObject : MonoBehaviour
{
    public bool isKeyInside;
    public bool hasBeenOpen;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite openSprite;

    public void OpenUp()
    {
        hasBeenOpen = true;
        spriteRenderer.sprite = openSprite;
    }
}
