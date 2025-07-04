using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "NewInformation", menuName = "CustomSO/Information", order = 1)]
public class Information : SerializedScriptableObject
{
    public InfoTypes type;
    public Sprite icon; 
    [KeysPopUp]public string informationText;
    [SerializeField] public List<Information> OverrideInfo;
    [SerializeField] public List<Information> BlockedByInfo;
    
}

public enum InfoTypes
{
    SUSPECTS,
    LEADS,
    FACTS,
}