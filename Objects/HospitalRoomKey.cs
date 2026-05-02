using UnityEngine;

public class HospitalRoomKey : MonoBehaviour
{
    [SerializeField] ToiletCloseUp toiletCloseUp;
    [SerializeField] InteractableObjectCloseUpSO closeUpSO;
    [SerializeField] AudioClip takeSound;

    public void TakeKey()
    {
        if (PlayerInventory.instance.TryAddItemToInventory(closeUpSO))
        {
            //AudioManager.instance.PlaySound(takeSound);
            toiletCloseUp.TakeKey();
            Destroy(gameObject);
        }
    }
}
