using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityParticles : MonoBehaviour
{
    private new ParticleSystem particleSystem;
    

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {

        // Only emit if the player's local scale is == -1
        
        var particleSystemEmission = particleSystem.emission;
        // written with a tiny tolerance in case of floating point error
        if (Math.Abs(SceneManager.Instance.player.transform.localScale.y - (-1)) < 0.01)
        {
            particleSystemEmission.enabled = true;
        }
        else
        {
            particleSystemEmission.enabled = false;

        }

    }
}
