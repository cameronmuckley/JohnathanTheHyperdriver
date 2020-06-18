using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonHandler : AbstractTrigger
{
    private new BoxCollider2D collider;

    Transform buttonGraphicTransform;

    private void Start()
    {
        buttonGraphicTransform = transform.Find("Button"); // finds the button in current children
        collider = GetComponent<BoxCollider2D>();
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Activate();
            // move the button graphic down a bit
            buttonGraphicTransform.Translate(buttonGraphicTransform.localScale.y * 0.3f * Vector3.down);
        }
    }
    
    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Deactivate();
            // move the button graphic up a bit
            buttonGraphicTransform.Translate(buttonGraphicTransform.localScale.y * 0.3f * Vector3.up);

        }
    }
}
