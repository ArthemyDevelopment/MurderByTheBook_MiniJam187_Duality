using System;
using ArthemyDev.ScriptsTools;
using UnityEngine;

public class MenuParallaxEffect : MonoBehaviour
{
    
    [Serializable]
    enum ValueToChange
    {
        Position,
        Rotation,
    }

    [SerializeField] private ValueToChange ParallaxEffect;
    [SerializeField] private Vector2 ValueRange;
    private Vector3 prevValue;
    
    void Update()
    {
        
        switch (ParallaxEffect)
        {
            case ValueToChange.Position:
                prevValue = transform.position;
                prevValue.x = ScriptsTools.MapValues(Input.mousePosition.x, 0, Screen.width, ValueRange.x, ValueRange.y);
                transform.position = prevValue;
                break;
            case ValueToChange.Rotation:
                prevValue = transform.eulerAngles;
                prevValue.y = ScriptsTools.MapValues(Input.mousePosition.x, 0, Screen.width, ValueRange.x, ValueRange.y);
                transform.eulerAngles = prevValue;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }        
    }
}
