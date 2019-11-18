using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabable : MonoBehaviour {

    public bool grabbed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (grabbed)
        {
            transform.parent = VRpointer.Current.gameObject.transform;
        }
        else
        {
            transform.parent = null;
        }
	}
}
