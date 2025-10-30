using System;
using Sirenix.OdinInspector;
using UnityEngine;

//[RequireComponent(typeof(Collider))]
public class BaseAction : SerializedMonoBehaviour
{
    [BoxGroup("Base Action properties")][SerializeField] private float EnergyCost;
    [BoxGroup("Base Action properties")][SerializeField] protected Dialog InvalidAction;
    [BoxGroup("Base Action properties")][SerializeField] private bool hasCollider = true;
    private Collider hitbox;
    


    protected virtual void OnEnable()
    {
        if(hasCollider)hitbox = GetComponent<Collider>();
            if(hitbox!=null) HitboxRecognitionSystem.AddInteractableObject(transform, TriggerAction);
    }

    private void OnDisable()
    {
        if(hitbox!=null) HitboxRecognitionSystem.RemoveInteratableObject(transform);
    }

    public virtual void TriggerAction()
    {
        EnergyManager.current.SpendEnergy(EnergyCost);
    
    }

    public virtual void OnHover()
    {
        
    }

    public virtual void StopHover()
    {
        
    }
    
    
}
