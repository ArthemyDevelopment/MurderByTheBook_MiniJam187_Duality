using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "NewClue", menuName = "CustomSO/Clue", order = 0)]
public class Clue : SerializedScriptableObject
{
    [SerializeField] private bool Reusable;
    [SerializeField] private Sprite Icon;
    [SerializeField] [KeysPopUp]private string Name;
    [SerializeField] [KeysPopUp]private string Description;
    [SerializeField] private Dictionary<Clue,Clue> Combinations = new Dictionary<Clue, Clue>();

    [SerializeField] private bool GiveInformation;
    [HideIf("@this.GiveInformation == false")] [SerializeField] private bool multipleInformation;
    [HideIf("@this.GiveInformation == false || multipleInformation== true")][SerializeField] private Information ClueInformation;
    [HideIf("@this.GiveInformation == false|| multipleInformation== false")][SerializeField] private List<Information> MultiInformation= new List<Information>();

    [SerializeField] private bool ForceReplaceClue;

    [HideIf("@this.ForceReplaceClue == false")] public Clue clueToReplace;
    
    public Sprite GetIcon() { return Icon; }
    public string GetName() { return Name; }
    public string GetDesc(){return Description;}
    public bool IsReusable() { return Reusable;}
    public bool GetForceToReplace() { return ForceReplaceClue;}

    public bool ClueGiveInformation() { return GiveInformation; }

    public bool ClueMultipleInformation() { return multipleInformation; }

    public Information GetInformation() { return ClueInformation;}
    public List<Information> GetMultiInformation() { return MultiInformation;}

    public Clue()
    {
        
    }
    
    public Clue(ClueData data)
    {
        Icon = data.clueIcon;
        Name = data.clueName;
        Description = data.clueDesc;
        Combinations = data.clueCombinations;
    }

    public void SetClueData(ClueData data)
    {
        Icon = data.clueIcon;
        Name = data.clueName;
        Description = data.clueDesc;
        Combinations = data.clueCombinations;
    }

    public ClueData GetClueInfo()
    {
        ClueData tempData;
        tempData.clueIcon = Icon;
        tempData.clueName = Name;
        tempData.clueDesc = Description;
        tempData.clueCombinations = Combinations;

        return tempData;
    }

    public Clue GetCombination(Clue clue)
    {
        if (Combinations.ContainsKey(clue))
            return Combinations[clue];
        else
            return null;
    }

}

[Serializable]
public struct ClueData
{
    public Sprite clueIcon;
    public string clueName;
    public string clueDesc;
    public Dictionary<Clue, Clue> clueCombinations;
}
