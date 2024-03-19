using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputController : MonoBehaviour
{
    public Vector3 inputDirection = Vector3.zero;

    [SerializeField] MovementController movementController;

    [SerializeField]  WeaponController weaponController;

    public void OnMove(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();


        movementController.Direction = inputDirection;
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            weaponController.Shoot();
        }

    }
}
