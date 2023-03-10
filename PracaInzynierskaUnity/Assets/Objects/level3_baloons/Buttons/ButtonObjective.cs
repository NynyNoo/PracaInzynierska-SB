using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonObjective: MonoBehaviour
{
    [SerializeField]private float threshold = 0.1f;
    [SerializeField]private float deadZone = 0.025f;
    [SerializeField] public int levelIndex; 
    [SerializeField] public Material normalMaterial;
    [SerializeField] public Material clickedMaterial;
    [SerializeField] public ElevatorPanelButtonsConnector connector;

    public UnityEvent onPressed, onReleased;
    private bool _isPressed;
    private Vector3 _startPos;
    private ConfigurableJoint _joint;
    void Start()
    {
       _startPos = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();
    }
    void Update()
    {
        if(!_isPressed&& GetValue()+threshold>=1)
        {
            Pressed();
        }
        if(_isPressed && GetValue()-threshold<=0)
        {
            Released();
        }
    }

    private float GetValue()
    {
        var value = Vector3.Distance(_startPos, transform.localPosition) / _joint.linearLimit.limit;
        if (Math.Abs(value) < deadZone)
            value = 0;
        return Mathf.Clamp(value,-1f,1f);

    }

    private void Pressed()
    {
        _isPressed = true;
        onPressed.Invoke();
        
        ResetButtonsColor();
    }
    private void Released()
    {
        _isPressed = false;
        onReleased.Invoke();
       
    }
    public void ResetButtonsColor()
    {
        connector.ResetButtonsColors(levelIndex);
        gameObject.GetComponentInChildren<Renderer>().material = clickedMaterial;
    }
    public void ResetColor()
    {
        gameObject.GetComponentInChildren<Renderer>().material = normalMaterial;
    }
}
