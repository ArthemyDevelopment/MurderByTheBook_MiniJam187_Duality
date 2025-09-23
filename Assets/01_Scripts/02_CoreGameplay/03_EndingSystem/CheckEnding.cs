using UnityEngine;

public class CheckEnding : MonoBehaviour
{
    public NotebookSolveCrimeController SolveCrimeController;
    public SolutionClue CorrectMotive;
    public SolutionClue CorrectWeapon;
    public SolutionClue CorrectOportunity;


    public void DefineEnding()
    {
        if (SolveCrimeController.CheckCorrectSolution(CorrectMotive, CorrectWeapon, CorrectOportunity))
        {
            if (EnergyManager.current.IsSolutionOnTime())
            {
                TransitionsManager.current.ChangeScene(Scenes.GoodEnding);
            }
            else
            {
                TransitionsManager.current.ChangeScene(Scenes.NeutralEnding);
            }
        }
        else
        {
            TransitionsManager.current.ChangeScene(Scenes.BadEnding);
        }
    }


}
