using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutBall : MonoBehaviour
{
    [SerializeField]
    public GameObject BallHolder;
    private Collider ball;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "BowlingBall")
        {
        ball = other;
        Vector3 BallPosition = ball.gameObject.transform.position;
        Vector3 vector = BallPosition - BallHolder.transform.position;
        ball.gameObject.GetComponent<Rigidbody>().velocity = -vector;
        }
    }
}
