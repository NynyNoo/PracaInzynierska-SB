using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ClimbingInteractible : MonoBehaviour
{
    public bool isRightHandClimbing;
    public bool isLeftHandClimbing;
    public Rigidbody playerRigRB;

    void Start()
    {
        isRightHandClimbing = false;
        isLeftHandClimbing = false;
        playerRigRB = GameObject.Find("Player Rig").GetComponent<Rigidbody>();
    }
    public bool isClimbing()
    {
        if (isRightHandClimbing == false && isLeftHandClimbing == false)
            return false;
        else
            return true;
    }
    public void setClimbing(string name)
    {
        if (name == "RightHand")
            isRightHandClimbing = true;
        if (name == "LeftHand")
            isLeftHandClimbing = true; 
    }
    public void stopClimbing(string name)
    {
        if (name == "RightHand")
            isRightHandClimbing = false;
        if (name == "LeftHand")
            isLeftHandClimbing = false;
    }

}
