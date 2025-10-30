using System;
using UnityEngine;

public class MoveCameraTrigger : MonoBehaviour
{
    [SerializeField] private float moveVelocity;
    private bool canMove = true;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Mouse")&&canMove)
        {
            CamerasManager.current.ActiveCamera.SplineSettings.Position += moveVelocity;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("StopMovementCamera")) canMove = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("StopMovementCamera")) canMove = true;
    }
}
