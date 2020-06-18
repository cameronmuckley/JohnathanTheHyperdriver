using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField]
    public float magnitude = 0.1f;
    Vector3 originPos;
    
    // Start is called before the first frame update
    void Awake()
    {
        originPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
        float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;
        transform.position = new Vector3(originPos.x + x, originPos.y + y, -10);
    }
}
