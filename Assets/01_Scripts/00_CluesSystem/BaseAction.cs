using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class BaseAction : SerializedMonoBehaviour
{
    [BoxGroup("Base Action properties")][SerializeField] private float EnergyCost;
    [BoxGroup("Base Action properties")][SerializeField] private bool hasCollider = true;
    [BoxGroup("Base Action properties")][SerializeField] protected Dialog InvalidAction;
    [BoxGroup("Base Action properties")][HideIf("@this.hasCollider == false")][SerializeField] private Collider2D hitbox;


    private void OnEnable()
    {
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
