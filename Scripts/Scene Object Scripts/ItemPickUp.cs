using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Vector3 offset;
    public float delay = 1.0f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LightZip lz = other.gameObject.GetComponent<LightZip>();
            lz.zipDist = 10;

            // set my parent to be the player and move myself to above the players head
            transform.parent = other.transform;
            transform.position += offset;
            
            // hang there for a while before destroying myself
            StartCoroutine(DelayedDestroy());
        }
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
