using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessController : MonoBehaviour
{
    private HyperSpeedManager hyperSpeedManager;

    private PostProcessVolume postProcessVolume;
    
    void Start()
    {
        hyperSpeedManager = HyperSpeedManager.Instance;
        postProcessVolume = GetComponent<PostProcessVolume>();
        
    }

    void Update()
    {
        // 0 when current speed is 1 and 1 when current speed is 0
        postProcessVolume.weight = 1 - Mathf.Pow(hyperSpeedManager.GetCurrentSpeed(), 2);
    }
}
