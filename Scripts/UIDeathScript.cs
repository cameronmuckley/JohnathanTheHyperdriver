using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDeathScript : MonoBehaviour
{
    bool isEntering = false;
    Color color;

    // Start is called before the first frame update
    void Awake()
    {
        isEntering = true;
        Color tempCol = gameObject.GetComponent<UnityEngine.UI.Image>().color;
        tempCol.a = 0f;
        gameObject.GetComponent<UnityEngine.UI.Image>().color = tempCol;

    }

    // Update is called once per frame
    void Update()
    {
        if (isEntering)
        {
            Color tempCol = gameObject.GetComponent<UnityEngine.UI.Image>().color;
            tempCol.a += 0.02f;
            if (tempCol.a >= 1f)
            {
                isEntering = false;
            }
            gameObject.GetComponent<UnityEngine.UI.Image>().color = tempCol;
        }
    }
}
