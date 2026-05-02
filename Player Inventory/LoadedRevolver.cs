using UnityEngine;

public class LoadedRevolver : MonoBehaviour, IInventoryUsable
{
    public bool TryUseItem()
    {
        PlayerInventory.instance.UnableToUseItemWarning();
        return false;
    }
}
