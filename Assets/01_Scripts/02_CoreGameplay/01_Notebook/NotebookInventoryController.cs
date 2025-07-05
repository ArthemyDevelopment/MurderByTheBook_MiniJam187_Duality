using System;
using ArthemyDev.ScriptsTools;
using ArthemyDevelopment.Localization;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class NotebookInventoryController : MonoBehaviour
{
    [SerializeField] private InventorySlotController[] NotebookSlots= new InventorySlotController[14];
    [BoxGroup("Clue management")][SerializeField]private Clue HoveringClue;
    [BoxGroup("Clue management")][SerializeField]private Clue DragedClue;
    [BoxGroup("Clue management")][SerializeField]private ClueData SelectedClue;

    [BoxGroup("Clue management")] [SerializeField] private NotebookMouse notebookMouse;

    [BoxGroup("Selected Clue")] [SerializeField] private GameObject ClueInfo; 
    [BoxGroup("Selected Clue")][SerializeField] private Image SelectedClueIcon; 
    [BoxGroup("Selected Clue")][SerializeField] private LocalizationObject SelectedClueName; 
    [BoxGroup("Selected Clue")][SerializeField] private LocalizationObject SelectedClueDesc;

    [BoxGroup("Clue Combination")] [SerializeField] private InventorySlotController CombinationSlot_1;
    [BoxGroup("Clue Combination")] [SerializeField] private InventorySlotController CombinationSlot_2;
    [BoxGroup("Clue Combination")] [SerializeField] private GameObject CombineButton;
    [BoxGroup("Clue Combination")] [SerializeField] private CombineCluesAction combinationAction;

    


    private void OnEnable()
    {
        UpdateSlots();
        
    }
    
    private void UpdateSlots()
    {
        for (int i = 0; i < NotebookSlots.Length; i++)
        {
            NotebookSlots[i].StoreClue(InventoryManager.current.InventorySlots[i].GetClueInSlot());
        }
        ClueInfo.SetActive(false);
        CleanCombinationSlots();
    }

    private void CleanCombinationSlots()
    {
        CombineButton.SetActive(false);
        CombinationSlot_1.RemoveClue();
        CombinationSlot_2.RemoveClue();
        combinationAction.ResetClues();
    }


    public void HoverSlot(Clue clue)
    {
        HoveringClue = clue;
    }

    public void SelectClue(ClueData clueData)
    {
        SelectedClue = clueData;
        SetSelectedClueData();
    }

    public void DragClue(Clue clue)
    {
        DragedClue = clue;
        notebookMouse.DragClue(DragedClue.GetIcon());
    }

    public void DropClue()
    {
        if (notebookMouse.GetCombineSlotHover() != CombineSlot.NULL)
        {
            switch (notebookMouse.GetCombineSlotHover())
            {
                case CombineSlot.SLOT1:
                    combinationAction.SetFirstClue(DragedClue);
                    CombinationSlot_1.StoreClue(DragedClue);
                    break;
                case CombineSlot.SLOT2:
                    combinationAction.SetSecondClue(DragedClue);
                    CombinationSlot_2.StoreClue(DragedClue);
                    break;
            }
        }
        else if (notebookMouse.GetSolutionSlotHover() != SolutionSlot.NULL)
        {
            switch (notebookMouse.GetSolutionSlotHover())
            {
                case SolutionSlot.MOTIVE:
                    NotebookManager.current.SolveCrimeController.SetSolutionMotive(DragedClue);
                    break;
                case SolutionSlot.WEAPON:
                    NotebookManager.current.SolveCrimeController.SetSolutionWeapon(DragedClue);
                    break;
                case SolutionSlot.OPORTUNITY:
                    NotebookManager.current.SolveCrimeController.SetSolutionOpportunity(DragedClue);
                    break;
            }
        }
        
        CheckCombinationSlots();
        DragedClue = null;
        notebookMouse.DropClue();
        
    }

    private void CheckCombinationSlots()
    {
        if (CombinationSlot_1.IsClueInSlot()&& CombinationSlot_2.IsClueInSlot()) CombineButton.SetActive(true);
        else CombineButton.SetActive(false);
    }
    
    public void TryCombinationClue()
    {
        Clue result = combinationAction.CluesCombination();
        if (result != null)
        {
            if(!CombinationSlot_1.GetClueInSlot().IsReusable())InventoryManager.current.RemoveClue(CombinationSlot_1.GetClueInSlot());
            if(!CombinationSlot_2.GetClueInSlot().IsReusable())InventoryManager.current.RemoveClue(CombinationSlot_2.GetClueInSlot());
            InventoryManager.current.StoreClue(result);
            UpdateSlots();
        }
        else
        {
            CleanCombinationSlots();
        }
    }

    void SetSelectedClueData()
    {
        SelectedClueIcon.sprite = SelectedClue.clueIcon;
        SelectedClueName.SetLocalizedObject(SelectedClue.clueName);
        SelectedClueDesc.SetLocalizedObject(SelectedClue.clueDesc);
        ClueInfo.SetActive(true);
    }
}
