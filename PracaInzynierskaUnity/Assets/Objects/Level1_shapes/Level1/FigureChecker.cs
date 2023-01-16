using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureChecker : MonoBehaviour
{
    public bool triangle = false;
    public bool cube = false;
    public bool  cylinder = false;
    [SerializeField] private Animator myDoorAnimator = null;
    [SerializeField] private StartingElevatorController door;

    void OnTriggerEnter(Collider other)
    {
        switch(other.name)
        {
            case "prism":
                triangle = true;
                CheckResult();
                break;
            case "cube":
                cube = true;
                CheckResult();
                break;
            case "cylinder":
                cylinder = true;
                CheckResult();
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        switch (other.name)
        {
            case "prism":
                triangle = false;
                break;
            case "cube":
                cube = false;
                break;
            case "cylinder":
                cylinder = false;
                break;
        }
    }
    void CheckResult()
    {
        if(triangle==true && cube == true && cylinder == true)
        {
                myDoorAnimator.Play("OpenElevator");
                door.isClosed=false;
        }
    }


}
