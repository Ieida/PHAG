using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnMenuStart()
    {
        SceneManager.LoadScene("StoryScene", LoadSceneMode.Single);
    }
    
    public void OnStart()
    {
        SceneManager.LoadScene("01", LoadSceneMode.Single);
    }

    public void OnControls()
    {
        SceneManager.LoadScene("MenuControls", LoadSceneMode.Single);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnCredits()
    {
        SceneManager.LoadScene("MenuCredits", LoadSceneMode.Single);
    }

    public void OnBack()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
