using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Sweeper") {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(Destruction());
        }
    }

    IEnumerator Destruction()
    {
        yield return new WaitForSeconds(1);
        Destroy(transform.gameObject);
    }
}
