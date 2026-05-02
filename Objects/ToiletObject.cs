using UnityEngine;

public class ToiletObject : MonoBehaviour
{
    public bool isFullyCloged;
    public int amountOfToiletPaperInside;
    public bool keyInside;

    [SerializeField] SpriteRenderer spriteRend;
    [SerializeField] Sprite cloggedSprite;

    public void ChangeToClogged()
    {
        spriteRend.sprite = cloggedSprite;
    }
}
