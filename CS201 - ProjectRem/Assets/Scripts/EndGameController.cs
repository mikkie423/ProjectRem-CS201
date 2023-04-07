using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGameController : MonoBehaviour
{
    public TextMeshProUGUI timerUI;
    public TextMeshProUGUI pointsUI;
    public GameObject[] gardens;
    public float timeRemaining = 300;
    public string timerText;
    private bool timerIsRunning = false;
    private int points;


    // Start is called before the first frame update
    void Start()
    {
        timerIsRunning = true;
        gardens = GameObject.FindGameObjectsWithTag("Farm");
    }

    // Update is called once per frame
    void Update()
    {
        TryGetComponent<PlayerAttackController>(out var playerAttackController);
        points = playerAttackController.totalKills;


        timerUI.text = DisplayTimer(timeRemaining);
        pointsUI.text = points.ToString();

        gardens = GameObject.FindGameObjectsWithTag("Farm");

        if (timerIsRunning)
        {
            if (gardens.Length < 1) GameOver();
            else if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else 
            {
                Debug.Log("Time has run out.");
                timerIsRunning = false;
                timeRemaining = 0;

                if (points > 0)
                    WinGame();
                else if ( points < 1)
                    GameOver();
            }
        }

    }

    private void GameOver()
    {
        Debug.Log("GAME OVER, YOU LOSE!");
        SceneManager.LoadScene(2);
    }

    private void WinGame()
    {
        TryGetComponent<PlayerAttackController>(out var PlayerAttackController);
        points = PlayerAttackController.totalKills;
        Debug.Log("YOU WIN, GOOD JOB! You got " + points + " total points.");
        SceneManager.LoadScene(1);
    }

    private string DisplayTimer(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        return timerText = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
