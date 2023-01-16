using UnityEngine;

public class Pump : MonoBehaviour
{
    [SerializeField] public GetBaloon getBallon;
    public bool pumpedRecently;
    public GameObject balloon;

    void Start()
    {
        pumpedRecently = false;
    }
    private void FixedUpdate()
    {
        if (getBallon.balloon != null)
            balloon = getBallon.balloon;
        else
            balloon = null;
        if (pumpedRecently && transform.localPosition.y >= 1.19)
            pumpedRecently = false;
        if (transform.localPosition.y <= 0.55 && pumpedRecently == false && balloon != null)
        {
            balloon.transform.localScale += new Vector3((float)0.2, (float)0.2, (float)0.2);
            if (balloon.transform.localScale.z >= 1)
                balloon.GetComponent<Balloon>().isFlying = true;
            pumpedRecently = true;
        }
        
    }
}
