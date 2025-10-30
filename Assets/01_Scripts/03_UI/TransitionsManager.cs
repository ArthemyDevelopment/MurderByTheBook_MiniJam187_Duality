using ArthemyDev.ScriptsTools;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class TransitionsManager : SingletonManager<TransitionsManager>
{
    [SerializeField] private Animator anim;
    private CamerasLocations tpCamerTarget;
    private Scenes sceneTarget;

    public void TransitionLocation(CamerasLocations target, float targetPosition = 0.5f)
    {
        tpCamerTarget = target;
        CamerasManager.current.SetCameraPosition(tpCamerTarget, targetPosition);
        anim.SetTrigger("short");
    }

    public void TpCamera()
    {
        CamerasManager.current.ChangeCameras(tpCamerTarget);
    }

    public void ChangeScene(Scenes scene)
    {
        
        sceneTarget = scene;
        
        anim.SetTrigger("long");
    }

    public void LoadScene()
    {
        SceneManager.LoadScene((int)sceneTarget);
    }
}
