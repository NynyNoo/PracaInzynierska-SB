using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoScaler : MonoBehaviour
{
    [SerializeField] private float defaultHeight = 1.8f;
    [SerializeField] private Camera camera;
    void Resize()
    {
        float headHeight = camera.transform.localPosition.y;
        float scale = defaultHeight / headHeight;
        transform.localScale = Vector3.one * scale;
    }

    // Update is called once per frame
    void OnEnable()
    {
        Resize();
    }
}
