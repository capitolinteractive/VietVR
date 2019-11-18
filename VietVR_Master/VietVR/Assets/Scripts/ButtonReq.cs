using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReq : MonoBehaviour {

    public delegate void ClickAction();
    public event ClickAction Activated = () => { };
    public bool highlighted;
    public bool pressed;

    /*
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    */

    public void ButtonActivated()
    {
        Activated();
    }
}
