using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRangeController : MonoBehaviour
{
    public bool[] stageProgress;
    public bool[] stageDeleted;
    [SerializeField] public GameObject FirstStage;
    [SerializeField] public GameObject SecondStage;
    [SerializeField] public GameObject ThirdStage;
    [SerializeField] public StartingElevatorController controller;
    [SerializeField] private Animator myDoor;


    private void Start()
    {
        stageProgress = new bool[3];
        stageDeleted = new bool[3];
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(stageProgress[0]&& !stageDeleted[0])
        {
            stageDeleted[0] = true;
            Object.Destroy(FirstStage);
        }

        if (stageProgress[1] && !stageDeleted[1])
        {
            stageDeleted[1] = true;
            Object.Destroy(SecondStage);

            if (controller.isClosed)
            {
                controller.isClosed = false;
                myDoor.Play("OpenElevator");
            }
        }

        /*if (stageProgress[2] && !stageDeleted[2])
        {
            stageDeleted[2] = true;
            Object.Destroy(ThirdStage);
        }*/

    }
}
