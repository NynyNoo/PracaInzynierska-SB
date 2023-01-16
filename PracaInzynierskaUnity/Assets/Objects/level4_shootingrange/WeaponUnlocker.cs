using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnlocker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<Collectibles>().weapon = true;
            gameObject.SetActive(false);
        }
    }
}
