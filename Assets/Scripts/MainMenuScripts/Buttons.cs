using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    static public string mode = "Host";

    public void StartGame()
    {
        mode = "Default";
        SceneManager.LoadScene("main");
        Debug.Log("Start");
    }

    public void QuitGame()
    {
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
        Debug.Log("Exit");
    }

    public void StartHost()
    {
        mode = "Host";
        SceneManager.LoadScene("main");
    }

    public void StartClient()
    {
        mode = "Client";
        SceneManager.LoadScene("main");
    }

}
