using UnityEngine;

public class SceneManager : MonoBehaviour
{

    void Start()
    {
        
    }

    private void ShowScene (string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void ShowMainMenuScene()
    {
        // ShowScene("MainMenu");
    }

    public void ShowGameScene()
    {
        // ShowScene("MainMenu");
    }

    public void ShowCreditsScene()
    {
        // ShowScene("MainMenu");
    }

    public void ShowOptionsScene()
    {
        // ShowScene("MainMenu");
    }

}
