using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStartingElevator : MonoBehaviour
{
    [SerializeField] private Animator myDoorAnimator = null;
    [SerializeField] private StartingElevatorController door;

    private void Start()
    {
        OpenDoor();
        gameObject.SetActive(false);
    }
    public void OpenDoor()
    {
        if (door.isClosed)
        {
            myDoorAnimator.Play("OpenElevator");
            door.isClosed = false;
        }
    }
}
