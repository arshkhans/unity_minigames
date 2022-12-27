using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Buttons : MonoBehaviour
{
    static public string GameMode = "Host";
    static public string PlayerName = "Admin";
    [SerializeField] private TMP_InputField InputName;
    [SerializeField] private GameObject ConfirmMenu;

    private void StartScene()
    {
        SceneManager.LoadScene("main");
    }

    public void StartGame()
    {
        GameMode = "Default";
        StartScene();
    }

    public void QuitGame()
    {
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
        Debug.Log("Exit");
    }

    public void StartHost()
    {
        GameMode = "Host";
        ConfirmMenu.SetActive(true);
    }

    public void StartClient()
    {
        GameMode = "Client";
        ConfirmMenu.SetActive(true);
    }

    public void GoBack()
    {
        ConfirmMenu.SetActive(false);
    }

    public void Confirm()
    {
        PlayerName = InputName.text;
        StartScene();
    }
    

}
