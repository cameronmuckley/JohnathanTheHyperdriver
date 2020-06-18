using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    public float spinRate = 2;

    private HyperSpeedManager hyperSpeedManager;
    // Start is called before the first frame update
    void Start()
    {
        hyperSpeedManager = HyperSpeedManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, spinRate * Time.deltaTime * hyperSpeedManager.GetCurrentSpeed());
    }
}
