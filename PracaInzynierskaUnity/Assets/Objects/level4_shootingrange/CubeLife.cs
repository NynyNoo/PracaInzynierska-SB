using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLife : MonoBehaviour
{
    Renderer rendererLocal;
    public int life=3;
    Color green = new Color(0, 1, 0, 1);
    Color yellow = new Color(1, 0.92f, 0.016f, 1);
    Color red = new Color(1, 0, 0, 1);

    // Start is called before the first frame update
    void Start()
    {
        rendererLocal = gameObject.GetComponent<Renderer>();
        rendererLocal.material.SetColor("_Color", green);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="bullet")
        { 
            life--;
            switch (life)
            {
                case 2:
                    rendererLocal.material.SetColor("_Color", yellow);
                    break;
                case 1:
                    rendererLocal.material.SetColor("_Color", red);
                    break;
                case 0:
                    GameObject.Destroy(gameObject);
                    break;
            }
           
        }
    }
}
