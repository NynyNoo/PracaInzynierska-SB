using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorHandler : MonoBehaviour
{
    public int selectedLevel = 0;
    [SerializeField] private Animator myDoorAnimator = null;
    [SerializeField] private StartingElevatorController door;
    public GameObject player;
    public TextMeshPro text;
    private bool changeLevelSequence = false;
    private float secondsToChangeLevel;
    void Start()
    {
        text = GetComponentInChildren<TextMeshPro>();
        secondsToChangeLevel= 5;
        player = GameObject.Find("Player Rig");
    }
    public void ChangeLevel(int targetLevel)
    {
        if (selectedLevel == targetLevel && changeLevelSequence == true)
            return;
        if(targetLevel==8)
        {
            text.text = "Na który poziom chcesz się przenieść?";
            secondsToChangeLevel = 5;
            myDoorAnimator.Play("OpenElevator");
            door.isClosed = false;
            changeLevelSequence = false;
            return;
        }
        secondsToChangeLevel = 5;
        selectedLevel = targetLevel;
        myDoorAnimator.Play("CloseElevator");
        door.isClosed = true;
        changeLevelSequence = true;
    }
    void FixedUpdate()
    {
        if (changeLevelSequence)
        {
            secondsToChangeLevel -= Time.deltaTime;
            text.text = $"Zmieniam poziom na {selectedLevel} odliczanie {secondsToChangeLevel.ToString("0")}s ";
            if (secondsToChangeLevel <= 0)
            {
                changeLevelSequence = false;
                player = GameObject.Find("Player Rig");
                player.transform.position = new Vector3(5, 0, 1.3f);
                SceneManager.LoadScene(selectedLevel);
            }
        }

    }
}
