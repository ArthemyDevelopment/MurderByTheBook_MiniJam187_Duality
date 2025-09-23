using ArthemyDev.ScriptsTools;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class EnergyManager : SingletonManager<EnergyManager>
{
    [SerializeField]private float minimunEnergyRequired;
    private float halfEnergyThreshold;
    [SerializeField] private float TotalEnergy;
    [SerializeField] private Image EnergyBar;

    protected override void Awake()
    {
        base.Awake();
        halfEnergyThreshold = (float)(minimunEnergyRequired * 1.5);
        TotalEnergy = ( halfEnergyThreshold* 2);
        CurrentEnergy = TotalEnergy;
    }


    private float _currentEnergy;
    
    private float CurrentEnergy
    {
        get
        {
            return _currentEnergy;
        }
        set
        {
            _currentEnergy = value;
            CheckEnergyRemaining();
        }
    }

    [Button]
    public void DEBUG_SetEnergyLow() { CurrentEnergy = 1;}
    
    
    private void CheckEnergyRemaining()
    {
        EnergyBar.fillAmount = ScriptsTools.MapValues(_currentEnergy, 0, TotalEnergy, 0, 1);
        if(_currentEnergy<= halfEnergyThreshold/3)
        {
            MusicManager.current.SetMusicAlmostOver();   
        }
        else if (_currentEnergy <= halfEnergyThreshold)
        {
            MusicManager.current.SetMusicHalfThreshold();
        }
        
        if (_currentEnergy <= 0)
        {
            TransitionsManager.current.ChangeScene(Scenes.OutOfTimeEnding);
        }
    }

    public bool IsSolutionOnTime()
    {
        if (CurrentEnergy >= halfEnergyThreshold) return true;
        else return false;
    }

    public void SpendEnergy(float cost)
    {
        CurrentEnergy -= cost;
    }
    
    
}
