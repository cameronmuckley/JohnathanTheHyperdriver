using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperSpeedManager : MonoBehaviour
{


    public float timeSlowdownRate = 1f;
    public float timeSpeedupRate = 1f;
    public float minSpeed = 0.1f;
    public float maxSpeed = 1;
    private float currentSpeed = 1;

    [HideInInspector] public bool blocked = false;

    public float airSlowDown;
    
    public static HyperSpeedManager Instance { get; private set; } // static singleton

    private void Awake()
    {
        // If there are no instances of HyperSpeedManager, set this to be the instance
        if (Instance == null)
        {
            Instance = this;
        }
        // else destroy this instance, so that can only be one instances
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (blocked)
        {
            currentSpeed += timeSpeedupRate * Time.deltaTime * 10;

        } else if (Input.GetButton("Hyperspeed"))
        {
            currentSpeed -= timeSlowdownRate * Time.deltaTime;
        }
        else
        {
            currentSpeed += timeSpeedupRate * Time.deltaTime;
        }

        // clamp to 0 and 1
        currentSpeed = Math.Min(maxSpeed, Math.Max(minSpeed, currentSpeed));
    }


    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

}
