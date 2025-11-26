using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class NotebookInformationController : MonoBehaviour
{
    [BoxGroup("Information Holders")]
    [BoxGroup("Information Holders/Parents")][SerializeField] private RectTransform SuspectParent;
    [BoxGroup("Information Holders/Parents")][SerializeField] private RectTransform LeadsParent;
    [BoxGroup("Information Holders/Parents")][SerializeField] private RectTransform FactsParent;
    [BoxGroup("Information Holders/Parents")][SerializeField] private RectTransform NotesParent;
    [BoxGroup("Information Holders/Templates")][SerializeField] private GameObject TextTemplate;
    [BoxGroup("Information Holders/Templates")][SerializeField] private GameObject CustomNoteTextTemplate;
    [BoxGroup("Information Holders/Templates")][SerializeField] private GameObject SuspectTemplate;
    [BoxGroup("Information Holders/SizeController")][SerializeField] private float PadingSize;
    [BoxGroup("Information Holders/SizeController")][SerializeField] private float TextTemplateSize;
    [BoxGroup("Information Holders/SizeController")][SerializeField] private float SuspectsTemplateSize;
    [BoxGroup("Player Custom Notes"), SerializeField] private GameObject CreateNotePopUp;
    [BoxGroup("Player Custom Notes"), SerializeField] private TMP_InputField CustomNoteInputField;
    [BoxGroup("Player Custom Notes"), SerializeField]private List<PlayerNotes> PlayerCustomNotes= new List<PlayerNotes>();
    
    private Dictionary<Information, GameObject> PrevInfo = new Dictionary<Information, GameObject>();


    private void OnEnable()
    {
        OrganizeInformation(); 
        CreateNotePopUp.SetActive(false);
    }

    public void OrganizeInformation()
    {


        List<Information> infoToRemove= new List<Information>();
        
        foreach (var key in PrevInfo.Keys)
        {
            if (!InformationManager.current.InformationList.Contains(key))
            {
                Destroy(PrevInfo[key]);
                infoToRemove.Add(key);
               
            }
        }

        foreach (var key in infoToRemove)
        {
            PrevInfo.Remove(key);
        }

        for (int i = 0; i < InformationManager.current.InformationList.Count; i++)
        {
            if(PrevInfo.ContainsKey(InformationManager.current.InformationList[i])) continue;
            
            switch (InformationManager.current.InformationList[i].type)
            {
                case InfoTypes.SUSPECTS:
                    SetUpNewInfo(SuspectTemplate,InformationManager.current.InformationList[i],SuspectParent);
                    break;
                case InfoTypes.LEADS:
                    SetUpNewInfo(TextTemplate,InformationManager.current.InformationList[i],LeadsParent);
                    break;
                case InfoTypes.FACTS:
                    SetUpNewInfo(TextTemplate,InformationManager.current.InformationList[i],FactsParent);
                    break;
            }
        }
        
        SetContainerHeight(SuspectParent, SuspectsTemplateSize);
        SetContainerHeight(LeadsParent, TextTemplateSize);
        SetContainerHeight(FactsParent, TextTemplateSize);
        
        
    }

    public void AddPlayerNote()
    {
        CreateNotePopUp.SetActive(true);
    }

    public void CancelPlayerNote()
    {
        CreateNotePopUp.SetActive(false);
        CustomNoteInputField.text = "";
    }

    public void SavePlayerNote()
    {
        if (CustomNoteInputField.text == "") return;
        CreateNotePopUp.SetActive(false);
        var temp = SetUpNewInfo(CustomNoteTextTemplate, CustomNoteInputField.text, null, NotesParent);
        PlayerCustomNotes.Add(new PlayerNotes(CustomNoteInputField.text,temp));
        SetContainerHeight(NotesParent, TextTemplateSize);
        CustomNoteInputField.text = "";
    }

    private void SetUpNewInfo(GameObject template,Information info, RectTransform parent)
    {
        var temp = SetUpNewInfo(template, info.informationText, info.icon, parent);
        PrevInfo.Add(info, temp);
    }
    
    private GameObject SetUpNewInfo(GameObject template,string informationText, Sprite icon, RectTransform parent)
    {
        var temp = Instantiate(template, parent).GetComponent<InfoTextController>();
        temp.SetInfo(informationText, icon);
        temp.gameObject.SetActive(true); 
        return temp.gameObject;
    }

    private void SetContainerHeight(RectTransform container, float templateSize)
    {
        var newHeight = (container.childCount * templateSize) + (container.childCount * PadingSize);
         
        container.sizeDelta = new Vector2(container.sizeDelta.x, newHeight);
    }

}
[Serializable]
public class PlayerNotes
{
    public string Text;
    public GameObject Object;

    public PlayerNotes(string _text, GameObject _gameObject)
    {
        Text = _text;
        Object = _gameObject;
    }
}
