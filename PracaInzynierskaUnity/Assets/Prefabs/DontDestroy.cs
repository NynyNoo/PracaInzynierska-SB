using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    private static GameObject instance;
    void Start()
    {
        if (instance == null)
            instance = gameObject;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
