using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    public AbstractTrigger button;
    public float openingSpeed = 10f;
    public float closingSpeed = 10f;
    public float closingDelay = 0.5f;
    private HyperSpeedManager hyperSpeedManager;
    

    private float closedY;
    private float openY;


    private float targetY;
    private float currentDoorSpeed;
    private float countDown = 0;
    
    private Transform doorTransform;
    private Image progressBar;


    void Start()
    {
        doorTransform = transform.Find("Door");
        progressBar = GetComponentInChildren<Image>();

        hyperSpeedManager = HyperSpeedManager.Instance;
        
        // save the closed and open y positions
        closedY = doorTransform.position.y;
        float height = doorTransform.GetComponent<Renderer>().bounds.extents.y * 2.0f; // gets height of door in world units
        openY = closedY + height * 0.8f;
        
        // subscribe to the button events
        if (button != null) button.OnActivate += SetToOpening;
        if (button != null) button.OnDeactivate += SetToClosing;

        targetY = closedY;
    }

    void Update()
    {
        float currentTimeSpeed = hyperSpeedManager.GetCurrentSpeed();
        
        // if we're currently on a timer delay, count down the timer
        if (countDown > 0)
        {
            countDown -= Time.deltaTime * currentTimeSpeed;
            
        // else if we haven't arrived at the target y position, move toward the target y
        } else if (Mathf.Abs(doorTransform.position.y - targetY) > 0.01)
        {
            Vector2 goalPos = doorTransform.position;
            goalPos.y = targetY;
            float speed = currentDoorSpeed * Time.deltaTime * currentTimeSpeed;
            doorTransform.position = Vector2.MoveTowards(doorTransform.position, goalPos, speed);
        }

        // Fill the UI bar based on stage in the countdown before closing
        float currentTimeRatio = 1 - countDown / closingDelay;
        progressBar.fillAmount = currentTimeRatio;
        
    }

    
    void SetToOpening()
    {
        countDown = 0;
        targetY = openY;
        currentDoorSpeed = openingSpeed;
    }
    
    void SetToClosing()
    { 
        countDown = closingDelay;
        targetY = closedY;
        currentDoorSpeed = closingSpeed;
    }

}
