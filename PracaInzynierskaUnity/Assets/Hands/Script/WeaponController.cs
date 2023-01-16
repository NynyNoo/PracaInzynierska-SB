using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    public InputActionReference controller=null;
    public Hand hand;

    // Update is called once per frame
    void Update()
    {
        //hand.IsHidden(controller.action.ReadValue<bool>());
        controller.action.started += test;
        controller.action.canceled -= test;
    }
    void test(InputAction.CallbackContext context)
    {
        Debug.Log("test");
    }
}
