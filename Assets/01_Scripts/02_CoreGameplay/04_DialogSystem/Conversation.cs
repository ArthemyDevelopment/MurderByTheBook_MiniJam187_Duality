using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "NewConversation", menuName = "CustomSO/Conversation", order = 0)]
public class Conversation : SerializedScriptableObject
{
    public ConversationNpcs Npc;
    public List<Question> Questions;
}


