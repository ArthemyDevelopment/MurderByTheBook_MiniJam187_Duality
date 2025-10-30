using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotNotebookController : MonoBehaviour, IDragHandler, IPointerUpHandler,IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private InventorySlotController SlotController;
    [SerializeField] private Collider2D _collider2D;

    /*private void OnEnable()
    {
        HitboxRecognitionSystem.AddInteractableObject(_collider2D,OnHover);
    }

    private void OnDisable()
    {
        HitboxRecognitionSystem.RemoveInteratableObject(_collider2D);
    }*/

    public void OnHover()
    {
        if (!SlotController.IsClueInSlot()) return;
        NotebookManager.current.InventoryController.HoverSlot(SlotController.GetClueInSlot());
    }

    public void StopHover()
    {
        NotebookManager.current.InventoryController.HoverSlot(null);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NotebookMouse")) OnHover();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("NotebookMouse")) StopHover();
    }

    public void OnDrag(PointerEventData eventData)
    {
        NotebookManager.current.InventoryController.DragClue(SlotController.GetClueInSlot());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        NotebookManager.current.InventoryController.DropClue();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!SlotController.IsClueInSlot()) return;
        NotebookManager.current.InventoryController.SelectClue(SlotController.GetClueDataInSlot());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopHover();
    }
}
