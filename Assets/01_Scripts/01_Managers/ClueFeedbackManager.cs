using System;
using System.Collections.Generic;
using ExternPropertyAttributes;
using UnityEngine;
using ArthemyDev.ScriptsTools;
using ArthemyDevelopment.Localization;
using Unity.VisualScripting;

public class ClueFeedbackManager : SingletonManager<ClueFeedbackManager>
{
    [BoxGroup("References"), SerializeField] private GameObject ClueFeedbackCanvas;
    [BoxGroup("References"), SerializeField] private Dictionary<Clue, GameObject> ShowObjects;
    [BoxGroup("References"), SerializeField] private Animator Anim;
    [BoxGroup("References"), SerializeField] private LocalizationObject ClueName;
    [BoxGroup("References"), SerializeField] private float HideRoationAnimationDuration;
    private Clue currentClue;
    
    

    public void ShowFeedback(Clue _clue)
    {
        DialogManager.current.OnCloseDialog += CloseFeedback;
        ClueName.SetLocalizedObject(_clue.GetName());
        
        ShowObjects[_clue].SetActive(true);
        
        ClueFeedbackCanvas.SetActive(true);
        
        Anim.Play("ShowRotation");
        currentClue = _clue;
    }


    public void CloseFeedback()
    {
        Anim.Play("HideRotation");
        Debug.Log("Play close feedback");
        ScriptsTools.DelayAction(TurnOffFeedback, HideRoationAnimationDuration);


    }

    private void TurnOffFeedback()
    {
        Debug.Log("Turn Off Feedback");
        ClueFeedbackCanvas.SetActive(false);
        ShowObjects[currentClue].SetActive(false);
        DialogManager.current.OnCloseDialog -= CloseFeedback;
    }
}
