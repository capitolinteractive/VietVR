using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour {
    bool highlighted;
    bool hasDrawn;
    Vector3 initialScale;
    ButtonReq bReq;
    // Use this for initialization
    void Start () {
        bReq = this.GetComponent<ButtonReq>();
        initialScale = transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        if (bReq.highlighted)
        {
            if (!highlighted)
            {
                highlighted = true;
                hasDrawn = false;
            }

        }
        else
        {
            if (highlighted)
            {
                highlighted = false;
                hasDrawn = false;
            }
        }

        if (!hasDrawn)
        {
            if (highlighted)
            {
                hasDrawn = true;
                transform.localScale = initialScale * 1.2f;
            }
            else
            {
                hasDrawn = true;
                transform.localScale = initialScale;
            }
        }


        if (bReq.pressed)
        {
            Application.Quit();

        }
    }
}
