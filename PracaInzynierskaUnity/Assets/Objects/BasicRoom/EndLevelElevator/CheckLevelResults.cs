using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLevelResults : MonoBehaviour
{
    [SerializeField] private Animator myDoor;
    [SerializeField] private GameObject controller;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (controller.GetComponent<EndElevatorController>().isClosed)
            {
                controller.GetComponent<EndElevatorController>().isClosed = false;
                myDoor.Play("Open");
            }
        }
    }
}
