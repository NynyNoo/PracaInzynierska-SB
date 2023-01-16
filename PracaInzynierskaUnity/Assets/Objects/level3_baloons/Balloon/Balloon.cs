using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public bool isFlying = false;
    private bool isBouncy = true;
    SphereCollider[] coliders;
    void Start()
    {
        if(isFlying)
        GetComponent<Rigidbody>().AddForce(0.0f, transform.localScale.x * 70, 0.0f);
        coliders = gameObject.GetComponents<SphereCollider>();
    }

    void FixedUpdate()
    {
        if (isFlying)
        {
            if (isBouncy)
            {
                foreach (var item in coliders)
                {
                    item.material.bounciness = 0;
                }
                isBouncy = false;
            }
            
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().AddForce(0.0f, transform.localScale.x * 120, 0.0f);
        }
    }
}
