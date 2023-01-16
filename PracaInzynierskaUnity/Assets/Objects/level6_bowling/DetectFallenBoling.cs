using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectFallenBoling : MonoBehaviour
{
    bool isFallen=false;
    float timer = 5;
    BowlingCatcher bowlingCatcher;
    private void Start()
    {
        bowlingCatcher = FindObjectOfType<BowlingCatcher>();
    }

    void FixedUpdate()
    {
        if (gameObject.transform.rotation.eulerAngles.x < -1
            || gameObject.transform.rotation.eulerAngles.x > 1
            || gameObject.transform.rotation.eulerAngles.z < -1
            || gameObject.transform.rotation.eulerAngles.z > 1)
        { 
            isFallen = true; 
        }
        else
            isFallen = false;
        if (isFallen)
            timer -= Time.deltaTime;
        if (timer <= 0)
                bowlingCatcher.OnTriggerEnter(gameObject.GetComponent<Collider>());
    }
}
