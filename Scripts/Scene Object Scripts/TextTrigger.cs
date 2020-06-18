using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    private TextboxManager textboxManager;
    public TextAsset textFile;
    public float lineDelay = 2f;
    public float charDelay = 0.02f;
    private string[] textLines;
    
    // Start is called before the first frame update
    void Start()
    {
        textboxManager = FindObjectOfType<TextboxManager>();
       
        // load in the text file and split it into an array of strings based on the lines in the file
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // interrupt and change the current text in the textboxManager and play it at a new speed.
            textboxManager.playNewText(textLines, charDelay, lineDelay);
            // destroy this trigger so it cant be triggered again.
            Destroy(gameObject);
        }
    }
}
