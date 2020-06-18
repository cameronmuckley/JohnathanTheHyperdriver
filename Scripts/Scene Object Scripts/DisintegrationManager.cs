using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisintegrationManager : MonoBehaviour
{
    
    public float disintergrationTime = 0.3f;
    private bool disintegrating;
    private float disintergrationTimer;

    private HyperSpeedManager hyperSpeedManager;

    void Start()
    {
        hyperSpeedManager = HyperSpeedManager.Instance;
    }

    
    void Update()
    {
        if (disintegrating)
        {
            disintergrationTimer += 1 / disintergrationTime * Time.deltaTime * hyperSpeedManager.GetCurrentSpeed();
        }
        else
        {
            disintergrationTimer -= 1 / disintergrationTime * Time.deltaTime * hyperSpeedManager.GetCurrentSpeed();
        }
        
        if (disintergrationTimer >= 1)
        {
            Destroy(GameObject.Find("Player"));
            SceneManager.Instance.playerDeath(0.5f);
            disintergrationTimer = 1;
        }

        disintergrationTimer = Mathf.Clamp(disintergrationTimer, 0, 1);
        
        // fade the player sprite to black and to transparent
        var spriteRendererColor = SceneManager.Instance.player.spriteRenderer.color;
        spriteRendererColor.a = 1 - disintergrationTimer;
        spriteRendererColor.r = 1 - disintergrationTimer;
        spriteRendererColor.g = 1 - disintergrationTimer;
        spriteRendererColor.b = 1 - disintergrationTimer;
        SceneManager.Instance.player.spriteRenderer.color = spriteRendererColor;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            disintegrating = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            disintegrating = false;
        }
    }
}
