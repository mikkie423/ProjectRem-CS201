using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{
    public GameObject[] gardens;
    public float timeRemaining = 300;
    private int points;

    // Start is called before the first frame update
    void Start()
    {
        gardens = GameObject.FindGameObjectsWithTag("Farm");
    }

    // Update is called once per frame
    void Update()
    {
        gardens = GameObject.FindGameObjectsWithTag("Farm");

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else if(timeRemaining <= 0 && points > 0)
        {
            WinGame();
        }

        else if (gardens.Length == 0 || timeRemaining <= 0 && points < 1) GameOver();
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
}
