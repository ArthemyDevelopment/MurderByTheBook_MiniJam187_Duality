using UnityEngine;

public class ManualTriggerClueFeedback : MonoBehaviour
{
    [SerializeField] private Clue clueToTrigger;


    public void TriggerClueFeedback()
    {
        ClueFeedbackManager.current.ShowFeedback(clueToTrigger);
    }
}
