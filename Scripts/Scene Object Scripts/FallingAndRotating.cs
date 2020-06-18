using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAndRotating : MonoBehaviour
{

    [SerializeField] float speed = 8;
    [SerializeField] float rotationSpeed = 100;
    [SerializeField] float startingRot = 78;

    private float currentRot = 0;

    bool isFalling = true;
    Rigidbody2D rigidbody;
    [SerializeField]
    public float yDestroyThreshold = -50;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        currentRot = startingRot;
        rigidbody.rotation = startingRot;
    }
    
    private void FixedUpdate()
    {
        
//        if (transform.position.y < yDestroyThreshold)
//            Destroy(gameObject);
//        
//        float currentSpeed = HyperSpeedManager.Instance.GetCurrentSpeed();
//        currentRot += Time.deltaTime * currentSpeed;
//        rigidbody.transform.Translate(currentSpeed * Time.deltaTime * new Vector2(0, -speed), Space.World);
//        rigidbody.rotation = currentRot*rotationSpeed;
    }


}
