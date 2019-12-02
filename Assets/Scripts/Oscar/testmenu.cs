using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testmenu : MonoBehaviour
{
    public void loadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Test Gameplay");
    }

    public void quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
