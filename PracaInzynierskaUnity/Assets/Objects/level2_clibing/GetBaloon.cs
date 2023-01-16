using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBaloon : MonoBehaviour
{
    public GameObject balloon = null;
    private Hand leftHandController;
    private Hand rightHandController;

    public void Start()
    {
        leftHandController = GameObject.Find("LeftHand").GetComponent<Hand>();
        rightHandController = GameObject.Find("RightHand").GetComponent<Hand>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Balloon")
        {
            if (leftHandController.nameOfHoldingObject == "Balloon")
                leftHandController.ForceRelease();
            if (rightHandController.nameOfHoldingObject == "Balloon")
                rightHandController.ForceRelease();
            balloon = other.gameObject;
            balloon.GetComponent<Rigidbody>().velocity = Vector3.zero;
            balloon.transform.position = transform.position;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.name == "Balloon")
        {
            balloon = null;
        }
    }
}
