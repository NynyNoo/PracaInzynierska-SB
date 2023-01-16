using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    public bool oppened;
    public void Start()
    {
        oppened = false;
    }
    public void PlayAnimation()
    {
        if (oppened == false)
        {
            GetComponent<Animation>().Play("TrapDoorAnimation");
            oppened = true;
        }

    }
}
