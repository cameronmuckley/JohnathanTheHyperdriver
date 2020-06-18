using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillOnTouch : MonoBehaviour
{

    public GameObject deathEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            //Stop music and play death sound
            //            Vector2 location = other.GetContact(0).point; // contact point
            try
            {
                FindObjectOfType<AudioManager>().Stop("song");
                FindObjectOfType<AudioManager>().Play("death");
            }
            catch (Exception e)
            {
                Debug.Log("No Audio Objects Found " + e);
            }
            finally
            {
                Vector2 location = other.transform.position;
                Instantiate(deathEffect, location, Quaternion.identity);
                Destroy(other.gameObject);
                SceneManager.Instance.playerDeath(1.0f);
            }
        }
    }
}
