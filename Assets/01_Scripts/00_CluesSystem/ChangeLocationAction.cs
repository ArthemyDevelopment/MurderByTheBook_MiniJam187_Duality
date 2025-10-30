using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class ChangeLocationAction : BaseAction
{
    [BoxGroup("ChangeLocationActionProperties")][SerializeField] private CamerasLocations LocationTarget;
    [BoxGroup("ChangeLocationActionProperties")][SerializeField] private float targetPosition = 0.5f;
    [BoxGroup("ChangeLocationActionProperties")][SerializeField] private GameObject GoToText;
    private AudioSource SFX;

    protected override void OnEnable()
    {
        base.OnEnable();
        SFX = GetComponent<AudioSource>();
    }

    public override void OnHover()
    {
        base.OnHover();
        GoToText.SetActive(true);
    }

    public override void StopHover()
    {
        base.StopHover();
        GoToText.SetActive(false);
    }

    
    public override void TriggerAction()
    {
        base.TriggerAction();
        TPCamera();
        if(SFX!=null)SFX.Play();
    }

    public void TPCamera()
    {
        TransitionsManager.current.TransitionLocation(LocationTarget, targetPosition);
        
    }

    public void HardTPCamera()
    {
        CamerasManager.current.ChangeCameras(LocationTarget);
    }
}
