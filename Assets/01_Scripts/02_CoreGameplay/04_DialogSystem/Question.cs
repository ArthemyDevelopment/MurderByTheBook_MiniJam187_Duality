using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestion", menuName = "CustomSO/NpcQuestion")]
public class Question : SerializedScriptableObject
{
    [KeysPopUp]public string QuestionText;
    public List<ConversationDialog> Dialogs;
    public bool UnlockQuestion;
    public Question QuestionToUnlock;
}

[Serializable]
public class ConversationDialog
{
    public AnimationClip Pose;
    [KeysPopUp] public string DialogText;
}
