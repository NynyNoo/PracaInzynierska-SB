using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTeleport : MonoBehaviour
{
    public Transform BallHolder1;
    public Transform BallHolder2;
    public Transform BallHolder3;
    public Transform BallHolder4;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BowlingBall")
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            switch(other.gameObject.name)
            {
                case "BowlingBall1":
                    other.transform.position = BallHolder1.position;
                    break;
                case "BowlingBall2":
                    other.transform.position = BallHolder2.position;
                    break;
                case "BowlingBall3":
                    other.transform.position = BallHolder3.position;
                    break;
                case "BowlingBall4":
                    other.transform.position = BallHolder3.position;
                    break;
            }
        }
    }
}
