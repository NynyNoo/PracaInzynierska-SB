using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResults : MonoBehaviour
{
    [SerializeField] private GameObject Elevator;
    private Animator myDoorAnimator;
    private StartingElevatorController endElevatorController;
    public bool isLevelCompleted;
    private bool[] buttons;
    private void Start()
    {
        myDoorAnimator = Elevator.GetComponent<Animator>();
        endElevatorController = Elevator.GetComponent<StartingElevatorController>();
        isLevelCompleted = false;
    } 
    
    void CheckButtons()
    {
        foreach (var item in buttons)
            if (!item)
                return;
        isLevelCompleted = true;
        endElevatorController.isClosed = false;
        myDoorAnimator.Play("OpenDoor");
    }
    public void PressedButton(int index, bool status)
    {
        buttons[index] = status;
        if (status)
            CheckButtons();
    }
}
