using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraMover : MonoBehaviour
{
    public bool followPlayer;
    private Transform playerTransform;
    public Vector3 offset;
    public float leftBound = - 10;
    public float rightBound = 10;
    public float lowerBound = -0.3f;
    public float upperBound = 7.0f;

    [SerializeField]
    public float magnitude = 0.1f;

    [SerializeField]
    public bool isShaking = false;

    private void Start()
    {
        playerTransform = SceneManager.Instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer && playerTransform != null)
        {
            // limit the camera's position
            float newX = Mathf.Clamp(playerTransform.position.x, leftBound, rightBound);
            float newY = Mathf.Clamp(playerTransform.position.y, lowerBound, upperBound);
            if (isShaking)
            {
                float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
                float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;
                transform.position = new Vector3(newX + x, newY + y, -10) + offset;
            }
            else
            {
                transform.position = new Vector3(newX, newY, -10) + offset;
            }  
        }
        
    }
}
