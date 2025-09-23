using System;
using System.Collections;
using ArthemyDev.ScriptsTools;
using ArthemyDevelopment.Localization;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : SingletonManager<DialogManager>
{
    [BoxGroup("UIElements")][SerializeField]private GameObject Mouse;
    [BoxGroup("UIElements")][SerializeField]private GameObject OpenNotebook;

    [BoxGroup("Dialog properties")][SerializeField]private GameObject DialogBox;
    [BoxGroup("Dialog properties")][SerializeField]private AudioSource TextSFX;
    [BoxGroup("Dialog properties")][SerializeField]private Image DialogChrIcon;
    [BoxGroup("Dialog properties")][SerializeField]private TMP_Text DialogTextArea;
    [BoxGroup("Dialog properties")][SerializeField]private LocalizationObject DialogLocalization;
    [BoxGroup("Dialog properties/Sizes")][SerializeField]private float DialogTextAreaSize_Witness;
    [BoxGroup("Dialog properties/Sizes")][SerializeField]private float DialogTextAreaSize_Player;
    [BoxGroup("Dialog properties")][SerializeField]private float delayTextChar;
    private Coroutine ShowTextCoroutine;
    private bool showingText;
    private bool isTextComplete;

    
    public void TriggerDialog(Dialog dialog)
    {
        if (dialog == null) return;
        InitDialogBox(dialog.ChrIcon);
        isTextComplete = false;
        DialogTextArea.maxVisibleCharacters = 0;
        ShowTextCoroutine = StartCoroutine(ShowText(dialog.DialogText));
    }

    private void InitDialogBox(Sprite sprite)
    {
        //showingText = true;
        Mouse.SetActive(false);
        OpenNotebook.SetActive(false);

        if (sprite != null)
        {
            SetWitnessDialog();
            DialogChrIcon.sprite = sprite;
        }
        else SetPlayerDialog();
        
        DialogBox.SetActive(true);
        
    }

    private void SetWitnessDialog()
    {
        DialogChrIcon.gameObject.SetActive(true);
        DialogTextArea.rectTransform.sizeDelta = new Vector2(DialogTextAreaSize_Witness, DialogTextArea.rectTransform.sizeDelta.y);
    }
    
    private void SetPlayerDialog()
    {
        DialogChrIcon.gameObject.SetActive(false);
        DialogTextArea.rectTransform.sizeDelta = new Vector2(DialogTextAreaSize_Player, DialogTextArea.rectTransform.sizeDelta.y);
    }

    private IEnumerator ShowText(string text)
    {
        DialogLocalization.SetLocalizedObject(text);
        for (int i = 0; i < DialogTextArea.text.Length; i++)
        {
            DialogTextArea.maxVisibleCharacters = i + 1;
            TextSFX.Play();
            yield return ScriptsTools.GetWait(delayTextChar);
            showingText = true;
        }

        isTextComplete = true;
    }
    

    private void CloseDialogBox()
    {
        DialogBox.SetActive(false);
        Mouse.SetActive(true);
        OpenNotebook.SetActive(true);
        showingText = false;

    }

    private void Update()
    {
        if (!showingText) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (!isTextComplete)
            {
                isTextComplete = true;
                StopCoroutine(ShowTextCoroutine);
                DialogTextArea.maxVisibleCharacters = DialogTextArea.text.Length + 1;
            }
            else
            {
                CloseDialogBox();    
            }
            
            
        }
        
    }
}
