using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Camera cam;
    public RaycastHit hit;
    public KeyCode interactKey = KeyCode.Mouse0;


    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetKeyDown(interactKey))
            {
                if (hit.transform.name == "NewGame") NewGame();
                if (hit.transform.name == "ResumeGame") Resume();
                if (hit.transform.name == "HighScores") HighScores();
                if (hit.transform.name == "QuitGame") Quit();
            }
            
        }
        else
            print("I'm looking at nothing!");

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Resume();
        }
    }

    private void NewGame()
    {
        print("Clicked on New Game");
        SceneManager.LoadScene(1);
    }

    private void Resume()
    {
        print("Clicked on Resume Game");
        SceneManager.UnloadSceneAsync(2);
        Time.timeScale = 1;
    }

    private void HighScores()
    {
        print("Clicked on HighScores");
    }

    private void Quit()
    {
        print("Clicked on Quit");
        Application.Quit();
    }
}
