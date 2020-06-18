using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostingControl : MonoBehaviour
{
    private SpriteRenderer sprite;
    private float timer = 0.15f;
    private static int count;
    
    private void Start()
    {
        count++;

        sprite = GetComponent<SpriteRenderer>();
        var playerTransform = SceneManager.Instance.player.transform;
        transform.position = playerTransform.position;
        transform.localScale = playerTransform.localScale;

        sprite.sprite = SceneManager.Instance.player.spriteRenderer.sprite;
        
        sprite.sortingOrder = -count - 200;
        

    }

    void Update()
    {
        timer -= Time.deltaTime * HyperSpeedManager.Instance.GetCurrentSpeed();

        if (timer <= 0)
        {
            count--;
            Destroy(gameObject);
        }
    }

}
