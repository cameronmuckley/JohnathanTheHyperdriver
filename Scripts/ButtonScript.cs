using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public void GoToLevel(string levelname)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelname);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
