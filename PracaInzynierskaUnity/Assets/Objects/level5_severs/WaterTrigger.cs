using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    [SerializeField] TravelTypeChecker travelTypeChecker;
    private void Start()
    {
        travelTypeChecker = GameObject.Find("Player Rig").GetComponent<TravelTypeChecker>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            travelTypeChecker.isSwiming = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            travelTypeChecker.isSwiming = false;
        }
    }
}
