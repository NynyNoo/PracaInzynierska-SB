using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenElevatorScript : MonoBehaviour
{
    [SerializeField] private Animator myDoor = null;
    [SerializeField] private GameObject controller;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag== "Player")
        {
            if (controller.GetComponent<StartingElevatorController>().isClosed)
            {
                controller.GetComponent<StartingElevatorController>().isClosed = false;
                myDoor.Play("OpenElevator");
            }
        }
    }
}
