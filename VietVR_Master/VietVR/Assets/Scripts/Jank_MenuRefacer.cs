using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jank_MenuRefacer : MonoBehaviour {
    public GameObject location;
    int count;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (count <= 0)
        {
            transform.position = location.transform.position;

            transform.LookAt(2 * transform.position - PlayerIndicator.Current.gameObject.transform.position);
            count = 10;
        }
        else count--;
        
	}
}
