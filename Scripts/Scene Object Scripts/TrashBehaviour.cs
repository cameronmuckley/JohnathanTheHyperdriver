using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBehaviour : MonoBehaviour
{

    [SerializeField] float speed;
    bool isFalling = true;
    Rigidbody2D rigidbody;
    [SerializeField]
    Sprite crushedSprite;

    public float yDestroyThreshold = -50;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        if (transform.position.y < yDestroyThreshold)
            Destroy(gameObject);
        
        float currentSpeed = HyperSpeedManager.Instance.GetCurrentSpeed();
//        if (isFalling) 
            rigidbody.transform.Translate(currentSpeed * Time.deltaTime * new Vector2(0, -speed), Space.World);
//            rigidbody.rotation = Time.time*100;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        isFalling = false;
        if (col.gameObject.name == "Floor")
        {
            //Can be changed to whatever else, most importantly isFalling is now assigned to false
            StartCoroutine(Destruction());
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        isFalling = false;
        if (col.gameObject.CompareTag("Sawblade"))
        {
            Destroy(gameObject);
        }
        if (col.gameObject.name == "PressHead")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = crushedSprite;
        }
    }

    IEnumerator Destruction()
    {
        yield return new WaitForSeconds(1);
        Destroy(transform.gameObject);
    }
}
