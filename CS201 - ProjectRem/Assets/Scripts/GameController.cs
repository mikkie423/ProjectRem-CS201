using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI timerUI;
    public TextMeshProUGUI killsUI;
    public static GameObject[] plants;
    public static int startingPlantsNum;
    public float timeRemaining;
    public string timerText;
    private bool timerIsRunning = false;
    public static int kills;
    public HealthBar healthBar;

    [Header("Cameras")]
    public GameObject playerCamHolder;
    public GameObject mindMapCamHolder;

    [Header("Scripts")]
    public PlayerAttackController playerAttackController;
    public PlayerMovementController playerMovementController;


    // Start is called before the first frame update
    void Start()
    {
        playerCamHolder.SetActive(true);
        mindMapCamHolder.SetActive(false);

        timerIsRunning = true;
        plants = GameObject.FindGameObjectsWithTag("Plant");
        healthBar.setMaxHealth(plants.Length);
        startingPlantsNum = plants.Length;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseMenu();
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            SwapCams();
        }

        kills = playerAttackController.totalKills;


        timerUI.text = DisplayTimer(timeRemaining);
        killsUI.text = kills.ToString();

        plants = GameObject.FindGameObjectsWithTag("Plant");

        if (timerIsRunning)
        {
            if (plants.Length < 1) GameOver();
            else if(plants.Length < healthBar.currentHealth()) healthBar.setHealth(plants.Length);
            else if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else 
            {
                Debug.Log("Time has run out.");
                timerIsRunning = false;
                timeRemaining = 0;

                if (kills > 0)
                    WinGame();
                else if ( kills < 1)
                    GameOver();
            }
        }

    }

    private void GameOver()
    {
        Debug.Log("GAME OVER, YOU LOSE!");
        SceneManager.LoadScene(4);
    }

    private void WinGame()
    {
        kills = playerAttackController.totalKills;
        Debug.Log("YOU WIN, GOOD JOB! You got " + kills + " total kills.");
        SceneManager.LoadScene(3);
    }

    private string DisplayTimer(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        return timerText = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void SwapCams()
    {
        float tempWalkSpeed;
        tempWalkSpeed = playerMovementController.walkSpeed;

        if (playerCamHolder.activeInHierarchy)
        {
            playerCamHolder.SetActive(false);
            mindMapCamHolder.SetActive(true);
            playerMovementController.canMove = false;
        }
        else if (mindMapCamHolder.activeInHierarchy)
        {
            playerMovementController.canMove = true;
            mindMapCamHolder.SetActive(false);
            playerCamHolder.SetActive(true);

        }

    }

    private void PauseMenu()
    {
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync(2);
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
    }
}
