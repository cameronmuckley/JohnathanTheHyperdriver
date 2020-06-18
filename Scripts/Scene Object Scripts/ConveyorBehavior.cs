using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBehavior : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    [SerializeField] private bool movesRight;
    void OnCollisionStay2D(Collision2D collision)
    {
        Vector2 direction = movesRight ? Vector2.right : Vector2.left;
        float currentSpeed = HyperSpeedManager.Instance.GetCurrentSpeed();
        collision.gameObject.GetComponent<Rigidbody2D>().transform.Translate(speed * Time.deltaTime * currentSpeed * direction, Space.World);
    }
}
