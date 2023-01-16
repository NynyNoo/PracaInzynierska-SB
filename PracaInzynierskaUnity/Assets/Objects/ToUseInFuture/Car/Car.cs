using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backLeft;
    [SerializeField] WheelCollider backRight;
    public float acceleration = 25f;

    void Start()
    {
        
    }
    void FixedUpdate()
    {
        frontRight.motorTorque = acceleration;
        frontLeft.motorTorque = acceleration;
        frontLeft.brakeTorque = 0;
        frontRight.brakeTorque = 0;
        backRight.brakeTorque = 0;
        backLeft.brakeTorque = 0;

    }
}
