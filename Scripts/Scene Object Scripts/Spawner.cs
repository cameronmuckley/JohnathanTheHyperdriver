using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // If it's constant then it cant be edited in the inspector
    [SerializeField] private float spawnDelay = 2f;
    [SerializeField] private float cubeScale = 1.0f;
    public GameObject trashCube;
    private float spawnTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(trashCube, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime * HyperSpeedManager.Instance.GetCurrentSpeed();
        if (spawnTimer >= spawnDelay)
        {
            spawnTimer -= spawnDelay;
            GameObject newCube = Instantiate(trashCube, transform.position, transform.rotation);
            // Capture spawned cube and scale it
            newCube.transform.localScale *= cubeScale;
        }
    }
}
