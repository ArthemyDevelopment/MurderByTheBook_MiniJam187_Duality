using System.Collections.Generic;
using ArthemyDev.ScriptsTools;
using Sirenix.OdinInspector;
using Unity.Cinemachine;
using UnityEngine;


public enum CamerasLocations
{
    Office,
    Hall,
    Kitchen,
    Bedroom,
    Outside,
}

public class CamerasManager : SingletonManager<CamerasManager>
{
    [BoxGroup("Cameras Config"), SerializeField] private Dictionary<CamerasLocations, CinemachineSplineDolly> CinemachineCameras;
    [BoxGroup("Cameras Config")] public CinemachineSplineDolly ActiveCamera { get; private set; }


    public void ChangeCameras(CamerasLocations target)
    {
        if(ActiveCamera!=null) ActiveCamera.gameObject.SetActive(false);

        ActiveCamera = CinemachineCameras[target];
        ActiveCamera.gameObject.SetActive(true);
    }

    public void SetCameraPosition(CamerasLocations target, float position)
    {
        CinemachineCameras[target].SplineSettings.Position = position;
    }

    [Button("Change Active Camera")]
    public void DEBUG_ChangeActiveCamera(CamerasLocations target)
    {
        ChangeCameras(target);
    }
    
}
