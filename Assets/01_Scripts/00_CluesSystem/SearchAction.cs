using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class SearchAction : BaseAction
{
    [BoxGroup("SearchProperties")][SerializeField] private Dialog DialogFeedback;
    [BoxGroup("SearchProperties")][SerializeField] private bool SingleTrigger;
    [BoxGroup("SearchProperties")][SerializeField] private UnityEvent OnSearch;
    bool alreadyTrigger;
    
    public override void TriggerAction()
    {
        if (SingleTrigger && alreadyTrigger) return;
        base.TriggerAction();
        OnSearch.Invoke();
        DialogManager.current.TriggerDialog(DialogFeedback);
        alreadyTrigger = true;
    }
}
