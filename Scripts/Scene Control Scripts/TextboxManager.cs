using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextboxManager : MonoBehaviour
{

    private HyperSpeedManager hyperSpeedManager;

    private Text textBox;
    public TextAsset textFile;
    public float lineDelay = 2f;
    public float charDelay = 0.02f;
    private string[] textLines;

    private Coroutine runningCoroutine;
    
    
    void Start()
    {
        hyperSpeedManager = HyperSpeedManager.Instance;
        textBox = gameObject.GetComponentInChildren<Text>();
        
        // load in the text file and split it into an array of strings based on the lines in the file
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        runningCoroutine = StartCoroutine(playText());
        
    }

    public void playNewText(string[] newTextLines, float newCharDelay, float newLineDelay)
    {
        // turn on the text box, interrupt the current text, and change what text is being displayed
        // as well as the speeds at which it displays  
        gameObject.SetActive(true);
        StopCoroutine(runningCoroutine);
        charDelay = newCharDelay;
        lineDelay = newLineDelay;
        textLines = newTextLines;
        runningCoroutine = StartCoroutine(playText());
        
    }
    
    
    IEnumerator playText()
    {
        // Play every line
        for (int i = 0; i < textLines.Length; i++)
        {
            string currentLine = textLines[i];
            
            // Write the characters of the current line one by one
            for (int j = 0; j < currentLine.Length; j++)
            {
                textBox.text = currentLine.Substring(0, j+1);
                
                // Delay before the next character based on what the current character is, scaled by current speed
                if (currentLine[j] == '.' || currentLine[j] == '!' || currentLine[j] == '?' || currentLine[j] == ':')
                {
                    yield return new WaitForSeconds(charDelay*20 / hyperSpeedManager.GetCurrentSpeed());
                } else if (currentLine[j] == ',')
                {
                    yield return new WaitForSeconds(charDelay*10 / hyperSpeedManager.GetCurrentSpeed());
                }
                else
                {
                    yield return new WaitForSeconds(charDelay / hyperSpeedManager.GetCurrentSpeed());
                }

            }
            
            // wait before the displaying the next line
            yield return new WaitForSeconds(lineDelay);
        }
        
        // once all text has been displayed, turn off the text box
        gameObject.SetActive(false);
    }
    
}
