using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour
{
    
    [BoxGroup("Default Slot")] [SerializeField] private Image IconImage;
    [BoxGroup("Clue data")][SerializeField] private Clue currentClue;
    [BoxGroup("Clue data")][SerializeField] private ClueData currentClueData;
    [BoxGroup("Clue data")][SerializeField] private bool hasCustomBehaviours = false;

    [BoxGroup("Custom Behaviours"),SerializeField, HideIf("@this.hasCustomBehaviours==false")] private UnityEvent OnStore; 
    [BoxGroup("Custom Behaviours"),SerializeField, HideIf("@this.hasCustomBehaviours==false")] private UnityEvent OnRemove; 

    


    public bool IsClueInSlot() { return currentClue != null; }


    public void StoreClue(Clue clue)
    {
        if (clue == null)
        {
            RemoveClue();
            return;
        }
        OnStore?.Invoke();
        IconImage.gameObject.SetActive(true);
        currentClue = clue;
        ClueData data = clue.GetClueInfo();
        currentClueData = data;
        IconImage.sprite = data.clueIcon;
    }

    public Clue GetClueInSlot()
    {
        return currentClue;
    }

    public ClueData GetClueDataInSlot()
    {
        return currentClueData;
    }

    public void RemoveClue()
    {
        OnRemove?.Invoke();
        IconImage.gameObject.SetActive(false);
        currentClue = null;
        currentClueData = new ClueData();
    }
    
}
