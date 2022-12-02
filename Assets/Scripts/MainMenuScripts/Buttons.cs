using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("main");
        Debug.Log("Start");
    }

    public void QuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
        Debug.Log("Exit");
    }

}
