using UnityEngine;

public class DoorKey : MonoBehaviour, IInventoryUsable
{
    [SerializeField] InteractableObjectCloseUpSO doorCloseUpSO;
    [SerializeField] InteractableObjectCloseUpSO doorCloseUpSOSecond;

    public bool TryUseItem()
    {
        if (ObjectInspectionManager.instance.currentCloseUpSO == doorCloseUpSO)
        {
            ObjectInspectionManager.instance.currentInteractedObject.GetComponent<IInteractableByItem>().UseItemOnObject();
            return true;
        }
        else if (doorCloseUpSOSecond != null && ObjectInspectionManager.instance.currentCloseUpSO == doorCloseUpSOSecond)
        {
            ObjectInspectionManager.instance.currentInteractedObject.GetComponent<IInteractableByItem>().UseItemOnObject();
            return true;
        }
        else
        {
            PlayerInventory.instance.UnableToUseItemWarning();
        }

        return false;
    }
}
