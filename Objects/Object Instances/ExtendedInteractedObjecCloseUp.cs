using UnityEngine;


public class ExtendedInteractedObjecCloseUp : InteractedObjectCloseUp
{
    public InteractableObjectCloseUpSO secondCloseUp;
    [SerializeField] int deathCountToTrigger;
    private void Start()
    {
        if (PersistantManager.instance.deathCount >= deathCountToTrigger)
        {
            closeUpSO = secondCloseUp;
        }
    }
}
