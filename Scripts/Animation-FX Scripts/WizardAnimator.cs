using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAnimator : MonoBehaviour
{
    private Animator animator;

    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    

    public void flipX()
    {
        // flip the boolean on the sprite renderer determining whether or not 
        // to render the sprite with a flipped x
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
