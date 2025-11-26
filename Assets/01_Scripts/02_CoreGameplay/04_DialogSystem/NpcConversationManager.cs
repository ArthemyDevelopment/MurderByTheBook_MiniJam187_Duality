using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class NpcConversationManager : SerializedMonoBehaviour
{


    public Dictionary<ConversationNpcs, NpcComponents> Npcs;
    
    
}

[Serializable]
public class NpcComponents
{
    public GameObject Model;
    public Animation Anim;

    public void SetNpc(AnimationClip pose)
    {
        Model.SetActive(true);
        Anim.clip = pose;
        Anim.Play();
    }

    public void DeactiveNpc()
    {
        Model.SetActive(false);
    }
}
