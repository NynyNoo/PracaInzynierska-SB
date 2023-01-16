using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingCatcher : MonoBehaviour
{
    [SerializeField] int firstAlley;
    [SerializeField] int secondAlley;
    [SerializeField] int thirdAlley;
    [SerializeField] GameObject TV1;
    [SerializeField] GameObject TV2;
    [SerializeField] GameObject TV3;
    [SerializeField] Material neutral;
    [SerializeField] Material green;
    [SerializeField] Material red;
    [SerializeField] private Animator myDoorAnimator = null;
    [SerializeField] private StartingElevatorController door;
    public bool isCompleted = false;

    public void OnTriggerEnter(Collider other)
    {
        switch (other.name)
        {
            case "Bowling1":
                other.gameObject.SetActive(false);
                firstAlley++;
                SetTVColor(firstAlley, TV1);
                break;
            case "Bowling2":
                other.gameObject.SetActive(false);
                secondAlley++;
                SetTVColor(secondAlley, TV2);
                break;
            case "Bowling3":
                other.gameObject.SetActive(false);
                thirdAlley++;
                SetTVColor(thirdAlley, TV3);
                break;
        }
        
    }
    private void SetTVColor(int counter,GameObject tv)
    {
        if (counter >= 10)
        {
            tv.GetComponentInChildren<Renderer>().material = green;
            if (door.isClosed)
            {
                myDoorAnimator.Play("OpenElevator");
                door.isClosed = false;
            }
        }
        else
            tv.GetComponentInChildren<Renderer>().material = red;
    }
}
