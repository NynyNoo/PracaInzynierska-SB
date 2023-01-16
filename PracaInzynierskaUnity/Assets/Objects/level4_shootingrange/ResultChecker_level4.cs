using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultChecker_level4 : MonoBehaviour
{
    [SerializeField] private Animator myDoor;
    [SerializeField] private GameObject controller;
    public bool isFirstShootingRangeCompleted;
    void Start()
    {
        isFirstShootingRangeCompleted = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      if(isFirstShootingRangeCompleted)
            if (controller.GetComponent<EndElevatorController>().isClosed)
            {
                controller.GetComponent<EndElevatorController>().isClosed = false;
                myDoor.Play("Open");
            }
    }
}
