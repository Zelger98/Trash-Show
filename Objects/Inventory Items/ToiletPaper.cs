using UnityEngine;

public class ToiletPaper : MonoBehaviour, IInventoryUsable
{
    [SerializeField] InteractableObjectCloseUpSO toiletCloseUpSO;
    public bool TryUseItem()
    {
        if (ObjectInspectionManager.instance.currentCloseUpSO == toiletCloseUpSO)
        {
            Debug.Log(ObjectInspectionManager.instance.currentSpawnedInteractionObject);
            ObjectInspectionManager.instance.currentSpawnedInteractionObject.GetComponent<ToiletCloseUp>().AddToiletPaper();

            return true;
        }
        else
        {
            PlayerInventory.instance.UnableToUseItemWarning();
        }
        return false;
    }
}
