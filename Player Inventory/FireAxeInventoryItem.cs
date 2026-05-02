using UnityEngine;

public class FireAxeInventoryItem : MonoBehaviour, IInventoryUsable
{
    [SerializeField] AudioClip attackSound;
    public bool TryUseItem()
    {
        if (ObjectInspectionManager.instance.currentSpawnedInteractionObject != null && ObjectInspectionManager.instance.currentSpawnedInteractionObject.TryGetComponent<MirrorInspectable>(out MirrorInspectable mirror))
        {
            AudioManager.instance.PlaySound(attackSound);
            mirror.FireAxeUsed();

            if (mirror.mirrorMain.currentDamageLevel == 4)
            {
                return true;
            }
        }
        else
        {
            PlayerInventory.instance.UnableToUseItemWarning();
        }

        return false;
    }
}
