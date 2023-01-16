using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseElevatorScript : MonoBehaviour
{
    [SerializeField] private Animator myDoor;
    [SerializeField] private GameObject controller;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!controller.GetComponent<StartingElevatorController>().isClosed)
            {
                controller.GetComponent<StartingElevatorController>().isClosed = true;
                myDoor.Play("DoorClosing");
            }
        }
    }
}
