using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    [SerializeField]
    Sprite onSprite;
    [SerializeField]
    Sprite offSprite;

    bool sound = true;

    public void toggleSound()
    {
        if (sound)
        {
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = offSprite;
            try
            {
                FindObjectOfType<AudioManager>().toggleMute(true);
                FindObjectOfType<AudioManager>().Stop("song");
            }
            catch (Exception e)
            {
                Debug.Log("No Sound to be Stopped " + e);
            }
            finally {
                sound = false;
            }          
        }
        else
        {
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = onSprite;
            try
            {
                FindObjectOfType<AudioManager>().toggleMute(false);
                FindObjectOfType<AudioManager>().Play("song");
            }
            catch (Exception e)
            {
                Debug.Log("No Sound to be Played " + e);
            }
            finally
            {
                sound = true;
            }
        }
    }
}
