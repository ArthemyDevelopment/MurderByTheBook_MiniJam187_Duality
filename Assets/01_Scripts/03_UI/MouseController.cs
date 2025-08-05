using System;
using UnityEngine;

public class MouseController : MonoBehaviour
{

    private Action InteractionAction;
    [SerializeField] private Dialog InvalidItemUsage;


    private void Awake()
    {
        Cursor.visible = false;
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (HitboxRecognitionSystem.ColliderHaveInteraction(other))
        {
            InteractionAction = HitboxRecognitionSystem.GetInteraction(other);
        }
        
        if (InteractionsManager.current.isItemSelected())return;
        
        if(other.CompareTag("LookInteraction"))
        {
            InteractionsManager.current.SetLookMouse();
        }
        else if(other.CompareTag("SearchInteraction"))
        {
            InteractionsManager.current.SetSearchMouse();
        }
        else if(other.CompareTag("ChangeScreenLeftInteraction"))
        {
            InteractionsManager.current.SetChangeSceneLeftMouse();
        }
        else if(other.CompareTag("ChangeScreenRightInteraction"))
        {
            InteractionsManager.current.SetChangeSceneRightMouse();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (HitboxRecognitionSystem.ColliderHaveInteraction(other))
        {
            InteractionAction = null;
        }

        if (InteractionsManager.current.isItemSelected()) return;
        
        if (other.CompareTag("LookInteraction") ||
            other.CompareTag("SearchInteraction") ||
            other.CompareTag("ChangeScreenLeftInteraction") ||
            other.CompareTag("ChangeScreenRightInteraction"))
        {
            InteractionsManager.current.SetDefaultMouse();
        }
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&&InteractionAction!=null)
        {
            InteractionAction.Invoke();
        }
        else if (Input.GetMouseButtonDown(0) && InteractionAction == null)
        {
            if (InteractionsManager.current.isItemSelected())
            {
                InteractionsManager.current.DeselectItem();
                DialogManager.current.TriggerDialog(InvalidItemUsage);
            }
        }
        
        
    }

    private void LateUpdate()
    {
        var screenPoint = Input.mousePosition;
        screenPoint.z = 1.0f;
        transform.position = Camera.main.ScreenToWorldPoint(screenPoint);
    }
}
