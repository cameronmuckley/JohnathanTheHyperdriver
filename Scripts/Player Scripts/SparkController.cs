using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkController : MonoBehaviour
{
    private HyperSpeedManager hyperSpeedManager;
    private new ParticleSystem particleSystem;
    private float defaultSimSpeed;
    private float defaultEmissionRate;
    

    private void Start()
    {
        hyperSpeedManager = HyperSpeedManager.Instance;
        particleSystem = GetComponent<ParticleSystem>();
        
        // Store default sim speed to preserve sim tweaks in the inspector
        defaultSimSpeed = particleSystem.main.simulationSpeed;
        defaultEmissionRate = particleSystem.emission.rateOverTimeMultiplier;
    }

    void Update()
    {
        
        float currentSpeed = hyperSpeedManager.GetCurrentSpeed();
        
        // set the simulation speed to the currentSpeed
        var particleSystemMain = particleSystem.main;
        particleSystemMain.simulationSpeed = defaultSimSpeed * currentSpeed;
        
        // Only emit if the player is on the ground
        var particleSystemEmission = particleSystem.emission;
        if (!SceneManager.Instance.player.onGround)
        {
            particleSystemEmission.rateOverTimeMultiplier = 0;
        }
        else
        {
            // increase the emission rate proportionally to the sim speed decrease,
            // but map the emission rate to be 0 if the current speed is 1
            particleSystemEmission.rateOverTimeMultiplier = defaultEmissionRate * (1/(currentSpeed*currentSpeed)) * (1 - currentSpeed);
        }
    }
}
