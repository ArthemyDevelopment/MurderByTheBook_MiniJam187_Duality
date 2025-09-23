using System;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BaseAction : SerializedMonoBehaviour
{
    [BoxGroup("Base Action properties")][SerializeField] private float EnergyCost;
    [BoxGroup("Base Action properties")][SerializeField] protected Dialog InvalidAction;
    [BoxGroup("Base Action properties")][SerializeField] private bool hasCollider = true;
    private Collider2D hitbox;
    


    protected virtual void OnEnable()
    {
        if(hasCollider)hitbox = GetComponent<Collider2D>();
            if(hitbox!=null) HitboxRecognitionSystem.AddInteractableObject(hitbox, TriggerAction);
    }

    private void OnDisable()
    {
        if(hitbox!=null) HitboxRecognitionSystem.RemoveInteratableObject(hitbox);
    }

    public virtual void TriggerAction()
    {
        EnergyManager.current.SpendEnergy(EnergyCost);
    
    }
}
