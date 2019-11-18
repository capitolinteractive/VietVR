using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour {
    bool flashed;
    Color myCol;
	// Use this for initialization
	void Start () {
        //particleSystem = GetComponent<particles>
        myCol = gameObject.GetComponent<Renderer>().material.GetColor("_TintColor");

        Destroy(gameObject.transform.parent.gameObject, 3f);
        Destroy(gameObject, 3f);
        //myCol.a = 0;
	}
	
	// Update is called once per frame
	void Update () {
        /*
        var main = ps.main;
        main.startColor = new Color(hSliderValueR, hSliderValueG, hSliderValueB, hSliderValueA);
        */
        
        
        myCol.a -= 2f * Time.deltaTime;
        GetComponent<Renderer>().material.SetColor("_TintColor", myCol);
    }
}
