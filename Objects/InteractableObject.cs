using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InteractableObject : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent onClickEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        onClickEvent?.Invoke();
    }
}
