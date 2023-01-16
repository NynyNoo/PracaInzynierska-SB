using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject, 2f);
    }
}
