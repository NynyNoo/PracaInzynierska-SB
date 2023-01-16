using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseEndElevatorScript : MonoBehaviour
{
    [SerializeField] private Animator myDoor;
    [SerializeField] private GameObject controller;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!controller.GetComponent<EndElevatorController>().isClosed)
            {
                controller.GetComponent<EndElevatorController>().isClosed = true;
                myDoor.Play("Close");
            }
        }
    }
}
