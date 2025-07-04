using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class UseClueAction : BaseAction
{
    [BoxGroup("UseClueProperties")][SerializeField] private Clue requiredClue;
    [BoxGroup("UseClueProperties")][SerializeField] private Dialog DialogFeedbackCorrectItem;
    [BoxGroup("UseClueProperties")][SerializeField] private Dialog DialogFeedbackWrongItem;
    [BoxGroup("UseClueProperties")][SerializeField] private UnityEvent OnUse;

    private bool isUsed;
    
    public override void TriggerAction()
    {
        if (isUsed)
        {
            InteractionsManager.current.DeselectItem();
            return;
        }
        base.TriggerAction();
        CheckClue();
    }

    private void CheckClue()
    {
        if(InteractionsManager.current.GetSelectedItem() == requiredClue)
        { 
            
            if(!requiredClue.IsReusable()) InventoryManager.current.RemoveClue(requiredClue);
            OnUse.Invoke();
            DialogManager.current.TriggerDialog(DialogFeedbackCorrectItem);
            isUsed = true;
        }
        else DialogManager.current.TriggerDialog(DialogFeedbackWrongItem);
        
        InteractionsManager.current.DeselectItem();
        
    }
}
