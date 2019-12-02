using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backtomenu : MonoBehaviour
{
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Test Main Menu");
        }
    }
}