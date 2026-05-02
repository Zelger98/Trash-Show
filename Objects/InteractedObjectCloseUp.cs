using UnityEngine;

public class InteractedObjectCloseUp : MonoBehaviour
{
    public InteractableObjectCloseUpSO closeUpSO;

    public virtual void OpenCloseUpInspection()
    {
        ObjectInspectionManager.instance.StartObjectInspect(this);
    }

    public virtual void TakeObject()
    {
        if (PlayerInventory.instance.TryAddItemToInventory(closeUpSO))
        {
            Destroy(gameObject);
        }
    }
}

