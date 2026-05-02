using UnityEngine;

public class LivingRoomKey : MonoBehaviour
{
    [SerializeField] InteractableObjectCloseUpSO objectCloseUpSO;
    public SafeboxObject closeUp;

    public void TakeToInventory()
    {
        if (PlayerInventory.instance.TryAddItemToInventory(objectCloseUpSO))
        {
            closeUp.isKeyInside = false;
            Destroy(gameObject);
        }
    }
}
