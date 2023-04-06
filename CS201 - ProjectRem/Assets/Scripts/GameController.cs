using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI timerUI;
    public TextMeshProUGUI pointsUI;
    public GameObject[] plants;
    public float timeRemaining = 300;
    public string timerText;
    private bool timerIsRunning = false;
    private int points;
    public HealthBar healthBar;

    [Header("Cameras")]
    public GameObject playerCamHolder;
    public GameObject mindMapCamHolder;


    // Start is called before the first frame update
    void Start()
    {
        playerCamHolder.SetActive(true);
        mindMapCamHolder.SetActive(false);

        timerIsRunning = true;
        plants = GameObject.FindGameObjectsWithTag("Plant");
        healthBar.setMaxHealth(plants.Length);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            SwapCams();
        }

        TryGetComponent<PlayerAttackController>(out var playerAttackController);
        points = playerAttackController.totalKills;


        timerUI.text = DisplayTimer(timeRemaining);
        pointsUI.text = points.ToString();

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

    private void SwapCams()
    {
        TryGetComponent<PlayerMovementController>(out var movementController);
        float tempWalkSpeed;
        tempWalkSpeed = movementController.walkSpeed;

        if (playerCamHolder.activeInHierarchy)
        {
            Debug.Log("Tab has been pressed and playerCam is active, time to change to mindmapCam.");
            playerCamHolder.SetActive(false);
            mindMapCamHolder.SetActive(true);
            Debug.Log("P2M - Cameras have been changed");
            movementController.canMove = false;
        }
        else if (mindMapCamHolder.activeInHierarchy)
        {
            movementController.canMove = true;
            Debug.Log("Tab has been pressed and mindmapCam is active, time to change to playerCam.");
            mindMapCamHolder.SetActive(false);
            playerCamHolder.SetActive(true);
            Debug.Log("M2P - Cameras have been changed");

        }
        else
            Debug.Log("Tab has been pressed and no cam is active...WHHHHAAATTTTT??!?!?!?!?");

    }
}
