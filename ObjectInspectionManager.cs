using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInspectionManager : MonoBehaviour
{
    public static ObjectInspectionManager instance;

    [SerializeField] Image blackBackgroundImage;
    [SerializeField] Transform interactedObjectPlaceholderTransform;
    [SerializeField] GameObject interactionUI;
    [SerializeField] GameObject objectInfoTextBox;
    [SerializeField] TextMeshProUGUI objectInfoTextMesh;
    [SerializeField] AudioClip UIbuttonSound;
    public GameObject currentSpawnedInteractionObject { get; private set; }
    public InteractableObjectCloseUpSO currentCloseUpSO { get; private set; }
    public InteractedObjectCloseUp currentInteractedObject { get; private set; }

    [SerializeField] GameObject takeButton;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        blackBackgroundImage.enabled = false;
        interactionUI.SetActive(false);
        objectInfoTextBox.SetActive(false);
    }

    public void StartObjectInspect(InteractedObjectCloseUp closeUpObject)
    {
        if (closeUpObject.closeUpSO.pauseTimerWhileInteracting)
        {
            Timer.instance.PauseTimer();
        }

        InfoTextManager.instance.StopShowingText();
        if (closeUpObject.closeUpSO.startInspectSound != null)
        {
            AudioManager.instance.PlaySound(closeUpObject.closeUpSO.startInspectSound);
        }
        EndInspect();

        if (closeUpObject.closeUpSO.canTakeToInventory)
        {
            takeButton.SetActive(true);
        }
        else
        {
            takeButton.SetActive(false);
        }

        blackBackgroundImage.enabled = true;

        objectInfoTextBox.SetActive(false);
        interactionUI.SetActive(true);
        currentSpawnedInteractionObject = Instantiate(closeUpObject.closeUpSO.objectToShow, interactedObjectPlaceholderTransform.position, Quaternion.identity);

        if (currentSpawnedInteractionObject.TryGetComponent<CloseUpObject>(out CloseUpObject closeUp))
        {
            closeUp.closeUpObjectScript = closeUpObject;
        }

        objectInfoTextMesh.text = "";
        currentInteractedObject = closeUpObject;
        currentCloseUpSO = closeUpObject.closeUpSO;
    }

    public void StartObjectInspectFromInventoryItem(InteractableObjectCloseUpSO closeUpObject)
    {
        if (closeUpObject.pauseTimerWhileInteracting)
        {
            //Timer.instance.PauseTimer();
        }

        EndInspect();

        if (closeUpObject.startInspectSound != null)
        {
            AudioManager.instance.PlaySound(closeUpObject.startInspectSound);
        }

        blackBackgroundImage.enabled = true;

        takeButton.SetActive(false);

        objectInfoTextBox.SetActive(false);
        interactionUI.SetActive(true);

        currentSpawnedInteractionObject = Instantiate(closeUpObject.objectToShow, interactedObjectPlaceholderTransform.position, Quaternion.identity);
        objectInfoTextMesh.text = "";
        currentCloseUpSO = closeUpObject;
    }

    public void ShowInspectedObjectInfo()
    {
        PlayButtonSound();
        InfoTextManager.instance.ShowText(currentCloseUpSO.textInfo);

        //if (objectInfoTextBox.activeSelf)
        //{
        //    objectInfoTextBox.SetActive(false);
        //    objectInfoTextMesh.text = "";
        //}
        //else
        //{
        //    objectInfoTextBox.SetActive(true);
        //    objectInfoTextMesh.text = currentCloseUpSO.objectInfo;
        //}
    }

    public void StopObjectInspect()
    {

        PlayButtonSound();
        InfoTextManager.instance.StopShowingText();
        EndInspect();
    }

    public void TryTakeObject()
    {
        InfoTextManager.instance.StopShowingText();
        currentInteractedObject.TakeObject();
        EndInspect();
    }

    public void EndInspect()
    {
        if (currentSpawnedInteractionObject != null)
        {
            Destroy(currentSpawnedInteractionObject);
        }

        if (currentInteractedObject != null && currentInteractedObject.closeUpSO.pauseTimerWhileInteracting)
        {
            Timer.instance.UnpauseTimer();
        }

        blackBackgroundImage.enabled = false;
        currentSpawnedInteractionObject = null;
        currentInteractedObject = null;
        currentCloseUpSO = null;
        interactionUI.SetActive(false);
    }

    private void PlayButtonSound()
    {
        AudioManager.instance.PlaySound(UIbuttonSound);
    }
}
