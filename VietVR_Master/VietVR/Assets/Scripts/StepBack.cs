using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepBack : MonoBehaviour {
    public bool go;
    public float pos;
    public float xpos;
    public float slideTime;
	// Use this for initialization
	void Start () {
        pos = transform.position.x;

        slideTime = 0.15f;
	}
	
	// Update is called once per frame
	void Update () {
        xpos = transform.position.x;
        if (go)
        {
            if (slideTime > 0)
            {
                slideTime -= Time.deltaTime;
                transform.Translate(Vector3.forward * 1.8f * Time.deltaTime);
            }
            else
            {
                go = false;
            }
            
        }
	}
}
