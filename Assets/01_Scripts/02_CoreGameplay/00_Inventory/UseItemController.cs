using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UseItemController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private InventorySlotController slotController;
    

    private void OnEnable()
    {
        HitboxRecognitionSystem.AddInteractableObject(transform, SelectItem);
    }
    
    


    public void SelectItem()
    {
        InteractionsManager.current.SelectItem(slotController.GetClueInSlot());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        InteractionsManager.current.SelectItem(slotController.GetClueInSlot());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
