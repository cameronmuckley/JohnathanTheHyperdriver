using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentParticles : MonoBehaviour
{


    private HyperSpeedManager hyperSpeedManager;
    private new ParticleSystem particleSystem;
    
    // Store default sim speed to preserve sim tweaks in the inspector
    private float defaultSimSpeed;
    

    private void Start()
    {
        hyperSpeedManager = HyperSpeedManager.Instance;
        particleSystem = GetComponent<ParticleSystem>();
        defaultSimSpeed = particleSystem.main.simulationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
        float currentSpeed = hyperSpeedManager.GetCurrentSpeed();
        
        var particleSystemMain = particleSystem.main;
        particleSystemMain.simulationSpeed = defaultSimSpeed * currentSpeed;
        
        
    }
}
