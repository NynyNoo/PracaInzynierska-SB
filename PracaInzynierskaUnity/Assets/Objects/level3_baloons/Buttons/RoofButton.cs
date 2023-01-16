using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoofButton : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;

    public UnityEvent onPressed, onReleased;
    private bool _isPressed;
    private Vector3 _startPos;
    private ConfigurableJoint _joint;
    public Animator myDoorAnimator;
    public StartingElevatorController elevatorController;
    void Start()
    {
        _startPos = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();
    }
    void Update()
    {
        if (!_isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
        }
        if (_isPressed && GetValue() - threshold <= 0.4)
        {
            Released();
        }
    }

    private float GetValue()
    {
        var value = Vector3.Distance(_startPos, transform.localPosition) / _joint.linearLimit.limit;
        if (Math.Abs(value) < deadZone)
        {
            value = 0;
        }

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        myDoorAnimator.Play("OpenElevator");
        elevatorController.isClosed = false;
        _isPressed = true;
        onPressed.Invoke();
    }
    private void Released()
    {
        myDoorAnimator.Play("CloseElevator");
        elevatorController.isClosed = true;
        _isPressed = false;
        onReleased.Invoke();
    }
    
}
