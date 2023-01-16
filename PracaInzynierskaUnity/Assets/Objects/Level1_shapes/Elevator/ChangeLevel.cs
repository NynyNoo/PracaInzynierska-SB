using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    private int nextSceneToLoad = 1;
    public GameObject player;
    public bool transfered = false;
    private int counter = 0;
    private void Start()
    {
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player=GameObject.Find("Player Rig");
            
            player.transform.position=new Vector3(5,1,1.3f);
            transfered = true;
            Debug.Log(transfered);
            //SceneManager.LoadScene(nextSceneToLoad);
        }
    }
    void Update()
    {
        if (transfered == true)
        {
            counter++;
            if (counter > 5)
            {
                transfered = false;
                SceneManager.LoadScene(nextSceneToLoad);
            }
        }
    }
}
