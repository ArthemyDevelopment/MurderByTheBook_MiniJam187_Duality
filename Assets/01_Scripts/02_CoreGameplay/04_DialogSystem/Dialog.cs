using UnityEngine;

[CreateAssetMenu(fileName = "NewDialog", menuName = "CustomSO/Dialog", order = 2)]
public class Dialog : ScriptableObject
{
    public Sprite ChrIcon;
    [KeysPopUp]public string DialogText;
}
