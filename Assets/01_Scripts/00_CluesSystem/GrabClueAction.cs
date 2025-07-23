using Sirenix.OdinInspector;
using UnityEngine;

public class GrabClueAction : BaseAction
{
    [BoxGroup("GrabClueProperties")][SerializeField] private Clue clue;
    [BoxGroup("GrabClueProperties")][SerializeField] private Dialog DialogFeedback;
    [BoxGroup("GrabClueProperties")][SerializeField] private bool deactivateOnGrab;
    [BoxGroup("GrabClueProperties")][SerializeField] private bool clueGrabed;
    public bool isClueGrabed() { return clueGrabed; }
    
    public override void TriggerAction()
    {
        if (InteractionsManager.current.isItemSelected())
        {
            InteractionsManager.current.DeselectItem();
            DialogManager.current.TriggerDialog(InvalidAction);
            return;
        }
            
        if (clueGrabed) return;
        
        base.TriggerAction();
        AddClueToInventory();
    }

    public void AddClueToInventory()
    {

        InventoryManager.current.StoreClue(clue);
        clueGrabed = true;
        DialogManager.current.TriggerDialog(DialogFeedback);
        if (deactivateOnGrab) gameObject.SetActive(false);
        else
        {
            InteractionsManager.current.SetLookMouse();
            gameObject.tag = "LookInteraction";
        }
    }

    
}
