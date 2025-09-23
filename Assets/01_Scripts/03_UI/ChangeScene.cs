using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]private Scenes _scene;
    
    public void changeScene()
    {
        SceneManager.LoadScene((int)_scene);
    }
}
