using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanelButtonsConnector : MonoBehaviour
{
    [SerializeField] List<GameObject> buttons;
    private List<PhysicsButton> butonsScripts=new List<PhysicsButton>();
    private ElevatorHandler elevatorHandler;
    private void Start()
    {
        foreach (var item in buttons)
        {
            butonsScripts.Add(item.GetComponentInChildren<PhysicsButton>());
        }
        elevatorHandler = gameObject.GetComponent<ElevatorHandler>();
    }
    public void ResetButtonsColors(int index)
    {
        foreach (var item in butonsScripts)
            if (item.levelIndex != index)
                item.ResetColor();
        elevatorHandler.ChangeLevel(index);
    }
}
