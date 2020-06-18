using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDestruction : MonoBehaviour
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform goal;

    private HyperSpeedManager hyperSpeedManager;

    // Start is called before the first frame update
    void Start()
    {
        hyperSpeedManager = HyperSpeedManager.Instance;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, goal.localPosition, speed * Time.deltaTime * HyperSpeedManager.Instance.GetCurrentSpeed());
    }
}
