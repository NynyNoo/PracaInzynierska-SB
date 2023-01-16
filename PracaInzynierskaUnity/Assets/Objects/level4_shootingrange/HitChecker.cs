using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    [SerializeField] private ShootingRangeController shootingRangeControllerScript;
    private bool isFirstStageCompleted;
    private int collisions;
    public int stageNumber;
    int timer=0;
    private void Start()
    {
        isFirstStageCompleted = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "CubeToShoot")
            collisions++;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.name== "CubeToShoot")
        collisions--;
    }
    private void FixedUpdate()
    {
        if(!isFirstStageCompleted)
        {
            if (timer < 3)
                timer++;
            else
                if (collisions > 0)
                {

                }
                else
                {
                    shootingRangeControllerScript.stageProgress[stageNumber] = true;
                    isFirstStageCompleted = true;
                }
        }
    }
}
