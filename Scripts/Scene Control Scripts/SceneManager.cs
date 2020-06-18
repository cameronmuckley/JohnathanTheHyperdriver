using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    [HideInInspector] public PlayerController player;

    [HideInInspector] public GameObject[] focalPoints;

    public GameObject deathUI;

    public static SceneManager Instance { get; private set; } // static singleton

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Cache references to the player for quick access by other scripts.
        player = FindObjectOfType<PlayerController>();
        focalPoints = GameObject.FindGameObjectsWithTag("FocalPoint");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        } else if (Input.GetButtonDown("Restart"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(gameObject.scene.name);
        } 
    }

    public void playerDeath(float delay)
    {
        /**
         * Stub for Death animation or death text or 'press x to continue' functionality
         */
        Instantiate(deathUI, Vector2.zero, Quaternion.identity);
        StartCoroutine(WaitForKeyPress());
        StartCoroutine(resetlLevel(delay));
    }

    IEnumerator resetlLevel(float delay)
    {
        yield return WaitForKeyPress();
    }

    IEnumerator WaitForKeyPress()
    {
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(gameObject.scene.name);
    }
}