using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRotation : MonoBehaviour {
    public float value;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion target = Quaternion.identity;
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * value);
        transform.localEulerAngles = new Vector3(0, 90, 0);
    }
}
