using System;
using System.Collections.Generic;
using UnityEngine;

public class InitianGameConfigManager : MonoBehaviour
{
    [SerializeField]private CamerasLocations StartLocation;
    [SerializeField]private List<Clue> StartingItems;
    [SerializeField]private List<Information> StartingInfo;

    private void Start()
    {
         CamerasManager.current.ChangeCameras(StartLocation);
         foreach (var clue in StartingItems)
         {
            InventoryManager.current.StoreClue(clue);    
         }

         foreach (var info in StartingInfo)
         {
             InformationManager.current.AddInformation(info);
         }
         
    }
}
