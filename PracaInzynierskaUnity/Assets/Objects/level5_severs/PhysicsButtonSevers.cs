using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButtonSevers : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;
    [SerializeField] public int levelIndex;
    [SerializeField] public GameObject main;

    private ElevatorPanelButtonsConnector connector;
    public UnityEvent onPressed, onReleased;
    private bool _isPressed;
    private Vector3 _startPos;
    private ConfigurableJoint _joint;
    private bool isTriggered = false;
    [SerializeField] private Animator wallAnimator = null;
    [SerializeField] private Animator myDoorAnimator = null;
    [SerializeField] private StartingElevatorController door;
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
        if (_isPressed && GetValue() - threshold <= 0)
        {
            Released();
        }
    }

    private float GetValue()
    {
        var value = Vector3.Distance(_startPos, transform.localPosition) / _joint.linearLimit.limit;
        if (Math.Abs(value) < deadZone)
            value = 0;
        return Mathf.Clamp(value, -1f, 1f);

    }

    private void Pressed()
    {
        if(!isTriggered)
        {
            wallAnimator.Play("WallUp");
            myDoorAnimator.Play("OpenElevator");
            isTriggered = true;
            _isPressed = true;
            onPressed.Invoke();
        }
    }
    private void Released()
    {
        _isPressed = false;
        onReleased.Invoke();

    }
}
