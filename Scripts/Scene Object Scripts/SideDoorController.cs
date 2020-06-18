using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideDoorController : MonoBehaviour
{
    public AbstractTrigger button;
    public float openingSpeed = 10f;
    public float closingSpeed = 10f;
    public float closingDelay = 0.5f;
    
    private HyperSpeedManager hyperSpeedManager;


    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nextPos;


    private float currentDoorSpeed;
    private float countDown = 0;

    [SerializeField]
    private Transform child;
    [SerializeField]
    private Transform transformB;

    private Image progressBar;


    void Start()
    {
        posA = child.localPosition;
        posB = transformB.localPosition;
        nextPos = posB;
        progressBar = GetComponentInChildren<Image>();

        hyperSpeedManager = HyperSpeedManager.Instance;

        // subscribe to the button events
        button.OnActivate += SetToOpening;
        button.OnDeactivate += SetToClosing;
    }

    void Update()
    {
        float currentTimeSpeed = hyperSpeedManager.GetCurrentSpeed();

        // if we're currently on a timer delay, count down the timer
        if (countDown > 0)
        {
            countDown -= Time.deltaTime * currentTimeSpeed;

            // else if we haven't arrived at the target y position, move toward the target y
        }
        else if (Vector3.Distance(child.localPosition, nextPos) > 0.01)
        {
            float speed = currentDoorSpeed * Time.deltaTime * currentTimeSpeed;
            child.localPosition = Vector3.MoveTowards(child.localPosition, nextPos, speed * Time.deltaTime * HyperSpeedManager.Instance.GetCurrentSpeed());
        }

        // Fill the UI bar based on stage in the countdown before closing
        float currentTimeRatio = 1 - countDown / closingDelay;
        progressBar.fillAmount = currentTimeRatio;

    }


    void SetToOpening()
    {
        countDown = 0;
        nextPos = posB;
        currentDoorSpeed = openingSpeed;
    }

    void SetToClosing()
    {
        countDown = closingDelay;
        nextPos = posA;
        currentDoorSpeed = closingSpeed;
    }

}
