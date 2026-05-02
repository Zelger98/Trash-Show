using System.Collections.Generic;
using UnityEngine;

public class MirrorInspectable : CloseUpObject
{
    public MirrorObject mirrorMain;
    [SerializeField] SpriteRenderer spriteRend;
    [SerializeField] List<Sprite> damagedSprites;

    private void Start()
    {
        mirrorMain = closeUpObjectScript.GetComponent<MirrorObject>();

        spriteRend.sprite = damagedSprites[mirrorMain.currentDamageLevel];
    }

    public void FireAxeUsed()
    {
        mirrorMain.currentDamageLevel++;

        if (mirrorMain.currentDamageLevel == 4)
        {
            mirrorMain.MirrorBroken();
        }
        else
        {
            spriteRend.sprite = damagedSprites[mirrorMain.currentDamageLevel];
        }
    }
}
