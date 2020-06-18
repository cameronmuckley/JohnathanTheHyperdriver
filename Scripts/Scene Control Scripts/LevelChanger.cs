using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    public string newLevelName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController.checkpointPosition = Vector3.zero;
            UnityEngine.SceneManagement.SceneManager.LoadScene(newLevelName);
        }
    }
}
