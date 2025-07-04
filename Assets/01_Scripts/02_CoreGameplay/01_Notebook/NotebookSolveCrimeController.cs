using Sirenix.OdinInspector;
using UnityEngine;

public class NotebookSolveCrimeController : MonoBehaviour
{
    [BoxGroup("Solutions")]
    [BoxGroup("Solutions")][SerializeField] private GameObject SolveCrimeButton;
    [BoxGroup("Solutions")][SerializeField] private GameObject TextsCluesTypes;
    [BoxGroup("Solutions/Slots")][SerializeField] private InventorySlotController WeaponSlot;
    [BoxGroup("Solutions/Slots")][SerializeField] private InventorySlotController MotiveSlot;
    [BoxGroup("Solutions/Slots")][SerializeField] private InventorySlotController OpportunitySlot;
    [BoxGroup("Solutions/CorrectClues")][SerializeField] private SolutionClue WeaponSolutionClue;
    [BoxGroup("Solutions/CorrectClues")][SerializeField] private SolutionClue MotiveSolutionClue;
    [BoxGroup("Solutions/CorrectClues")][SerializeField] private SolutionClue OpportunitySolutionClue;



    public bool CheckCorrectSolution(SolutionClue motive, SolutionClue weapon, SolutionClue oportunity)
    {
        return (motive == MotiveSolutionClue &&
                weapon == WeaponSolutionClue &&
                oportunity == OpportunitySolutionClue);
    }

    private void OnEnable()
    {
        SolveCrimeButton.SetActive(false);
        TextsCluesTypes.SetActive(true);
        CheckAllEvidence();
    }

    public void SetSolutionWeapon(Clue clue)
    {

        if (clue.GetType() != typeof(SolutionClue)) return;

        if ((clue as SolutionClue).GetType() != SolutionTypes.WEAPON) return;
        
        WeaponSolutionClue = clue as SolutionClue;
        WeaponSlot.StoreClue(clue);
        
        CheckAllEvidence();
        
    }
    
    public void SetSolutionMotive(Clue clue)
    {
        if (clue.GetType() != typeof(SolutionClue)) return;
        if ((clue as SolutionClue).GetType() != SolutionTypes.MOTIVE) return;
        
        MotiveSolutionClue = clue as SolutionClue;
        MotiveSlot.StoreClue(clue);
        
        CheckAllEvidence();
        
    }
    
    public void SetSolutionOpportunity(Clue clue)
    {
        if (clue.GetType() != typeof(SolutionClue)) return;
        if ((clue as SolutionClue).GetType() != SolutionTypes.OPPORTUNITY) return;
        
        OpportunitySolutionClue = clue as SolutionClue;
        OpportunitySlot.StoreClue(clue);
        
        CheckAllEvidence();
        
    }

    private void CheckAllEvidence()
    {
        if (WeaponSlot.IsClueInSlot() && MotiveSlot.IsClueInSlot() && OpportunitySlot.IsClueInSlot())
        {
            SolveCrimeButton.SetActive(true);
            TextsCluesTypes.SetActive(false);
        }
    }
    
}
