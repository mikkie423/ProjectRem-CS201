using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGame : MonoBehaviour
{    

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) Continue();
    }

    private void Continue()
    {
        Debug.Log("Continue");
        SceneManager.LoadScene(0);
    }

}
