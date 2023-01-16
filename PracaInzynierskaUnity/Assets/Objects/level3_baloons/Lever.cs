using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Animator myDoorAnimator;
    public StartingElevatorController elevatorController;
    void Update()
    {
        if (gameObject.transform.rotation.eulerAngles.z < 40)
        {
            if (elevatorController.isClosed)
            {
                myDoorAnimator.Play("OpenElevator");
                elevatorController.isClosed = false;
            }
        }
        else
        if (!elevatorController.isClosed)
        {
            myDoorAnimator.Play("CloseElevator");
            elevatorController.isClosed = true;
        }
    }
    void Start()
    {
    }
}
