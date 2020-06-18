using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeLevelChanger : MonoBehaviour
{
    public string levelName;

    void Awake()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
    }
}
