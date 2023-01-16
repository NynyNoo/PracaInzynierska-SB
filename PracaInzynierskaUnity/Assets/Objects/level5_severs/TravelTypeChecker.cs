using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TravelTypeChecker : MonoBehaviour
{
    [SerializeField] GameObject locomotionSystem;
    [SerializeField] SwimmingScript swimmingScript;
    public bool isSwiming=false;

    void Start()
    {
        locomotionSystem = GameObject.Find("Locomotion System");
        swimmingScript = GetComponent<SwimmingScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSwiming == true)
        {
            locomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
            swimmingScript.StartSwimming();
            
            //swimmingScript.enabled = true;
        }
        else
        {
            locomotionSystem.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
            swimmingScript.StopSwimming();
            
            //swimmingScript.enabled = false;
        }
    }
}
