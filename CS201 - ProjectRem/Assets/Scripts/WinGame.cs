using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinGame : MonoBehaviour
{
    public TextMeshProUGUI pointsUI;
    public GameObject highScoreUI;
    public TextMeshProUGUI scoresUI;
    private int plantsRemaining;
    private int kills;
    private int points;
    private int startingPlantsNum;
    private string scoresMessage;

    // Start is called before the first frame update
    void Start()
    {
        startingPlantsNum = GameController.startingPlantsNum;
        plantsRemaining = GameController.plants.Length;
        kills = GameController.kills;
        scoresMessage = kills.ToString();
        scoresMessage += " Rats Killed\n";
        scoresMessage += plantsRemaining.ToString();
        scoresMessage += " Plants Left";
        scoresUI.text = scoresMessage;
        highScoreUI.SetActive(false);
        CalculatePoints();
        DisplayHighScoreMessage();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) Continue();
    }

    private void CalculatePoints()
    {
        points = kills + plantsRemaining;
    }

    private void Continue()
    {
        Debug.Log("Continue");
        SceneManager.LoadScene(0);
    }

    private void DisplayHighScoreMessage()
    {
        Debug.Log("Displaying HighScore of " + points.ToString());
        highScoreUI.SetActive(true);
    }
}
