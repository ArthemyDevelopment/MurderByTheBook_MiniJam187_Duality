using System;
using UnityEngine;

public class MouseController : MonoBehaviour
{

    private Action InteractionAction;
    [SerializeField] private Dialog InvalidItemUsage;
    private bool isUIOpen=false;
    private RaycastHit hit;
    private Ray ray;
    private BaseAction HoverAction;
    private void Awake()
    {
        Cursor.visible = false;
        
    }

    public void SetIsUi(bool state)
    {
        isUIOpen = state;
    }
    


    private void Update()
    {
        if (!isUIOpen)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (HitboxRecognitionSystem.ColliderHaveInteraction(hit.transform))
                {
                    var temp = hit.transform.GetComponent<BaseAction>();

                    if (temp != HoverAction)
                    {
                        HoverAction?.StopHover();
                        HoverAction = temp;
                        HoverAction.OnHover();
                    }
                    
                    InteractionAction = HitboxRecognitionSystem.GetInteraction(hit.transform);
                }

                if(!InteractionsManager.current.isItemSelected())
                    switch (hit.transform.tag)
                    {
                        case "LookInteraction":
                            InteractionsManager.current.SetLookMouse();
                            break;
                        case "SearchInteraction":
                            InteractionsManager.current.SetSearchMouse();
                            break;
                        case "ChangeScreenLeftInteraction":
                            InteractionsManager.current.SetChangeSceneLeftMouse();
                            break;
                        case "ChangeScreenRightInteraction":
                            InteractionsManager.current.SetChangeSceneRightMouse();
                            break;
                        default:
                            InteractionsManager.current.SetDefaultMouse();
                            break;
                    }
            }
            else
            {
                if(!InteractionsManager.current.isItemSelected())InteractionsManager.current.SetDefaultMouse();
                InteractionAction = null;
                if (HoverAction != null)
                {
                    HoverAction.StopHover();
                    HoverAction = null;
                }
            }
        }
        
        if (Input.GetMouseButtonUp(0)&&InteractionAction!=null)
        {
            InteractionAction.Invoke();
        }
        else if (Input.GetMouseButtonUp(0) && InteractionAction == null)
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
        transform.position = Input.mousePosition;
    }
}
