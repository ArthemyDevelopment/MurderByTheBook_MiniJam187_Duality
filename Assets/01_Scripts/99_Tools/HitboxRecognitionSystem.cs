using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HitboxRecognitionSystem
{
    private static Dictionary<Transform, Action> _InteractionsDic= new Dictionary<Transform, Action>();
    private static Dictionary<Collider2D, Action> _UIInteractionsDic= new Dictionary<Collider2D, Action>();

    public static void AddInteractableObject(Transform col, Action method)
    {
        _InteractionsDic.Add(col, method);
    }

    public static void RemoveInteratableObject(Transform col)
    {
        _InteractionsDic.Remove(col);
    }

    public static bool ColliderHaveInteraction(Transform col)
    {
        return _InteractionsDic.ContainsKey(col);
    }

    public static Action GetInteraction(Transform col)
    {
        if(_InteractionsDic.ContainsKey(col))
            return _InteractionsDic[col];
        else
        {
            Debug.LogError("Collider not found in dictionary: Please check the OnEnable method of the target", col.gameObject);
            return null;
        }
        
    }
    
    public static void TriggerInteraction(Transform col)
    {
        if(_InteractionsDic.ContainsKey(col))
            _InteractionsDic[col].Invoke();
        else
            Debug.LogError("Collider not found in dictionary: Please check the OnEnable method of the target", col.gameObject);
    }
    
}
