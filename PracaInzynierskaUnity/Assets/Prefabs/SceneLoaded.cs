using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaded : MonoBehaviour
{
    private void OnLevelWasLoaded(int level)
    {
        gameObject.transform.position = 
            new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);
        if(level!=0)
        {
            Debug.Log(level);
            GameObject.Find("OpenTrigger").GetComponent<OpenStartingElevator>().OpenDoor();
        }
    }
}
