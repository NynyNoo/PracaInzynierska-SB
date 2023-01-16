using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public Animator myDoorAnimator;
    public StartingElevatorController elevatorController;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (elevatorController.isClosed)
            {
                myDoorAnimator.Play("OpenElevator");
                elevatorController.isClosed = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!elevatorController.isClosed)
            {
                myDoorAnimator.Play("CloseElevator");
                elevatorController.isClosed = true;
            }
        }
    }
}

