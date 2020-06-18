using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressScript : MonoBehaviour
{
    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nextPos;

    [SerializeField]
    private float speed;
    [SerializeField] [Range(0, 2)]
    private float phase;
    [SerializeField]
    private Transform child;
    [SerializeField]
    private Transform transformB;

    private HyperSpeedManager hyperSpeedManager;

    void Start()
    {
        posA = child.localPosition;
        posB = transformB.localPosition;
        nextPos = posB;
        hyperSpeedManager = HyperSpeedManager.Instance;
        if (phase < 1)
            child.localPosition= Vector3.Lerp(posA, posB, phase);
        else 
            child.localPosition = Vector3.Lerp(posB, posA, phase-1);

    }

    void Update()
    {
        float currentTimeSpeed = hyperSpeedManager.GetCurrentSpeed();

        if (Vector3.Distance(child.localPosition, nextPos) <= 0.01)
        {
            if (nextPos == posA)
            {
                nextPos = posB;
            }
            else
            {
                nextPos = posA;
            }
        }
        else
        {
            child.localPosition = Vector3.MoveTowards(child.localPosition, nextPos, speed * Time.deltaTime * HyperSpeedManager.Instance.GetCurrentSpeed());
        }
    }
}
