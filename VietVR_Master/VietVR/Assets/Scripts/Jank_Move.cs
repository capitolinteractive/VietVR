using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jank_Move : MonoBehaviour {
    public GameObject MoveMe;
    public float speed = 3f;

    // Use this for initialization
    void Start () {

       
	}

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0) || OVRInput.Get(OVRInput.Button.DpadDown) || OVRInput.Get(OVRInput.Button.DpadLeft) || OVRInput.Get(OVRInput.Button.DpadRight) || OVRInput.Get(OVRInput.Button.DpadUp) || OVRInput.Get(OVRInput.Button.One))
        {
            //MoveMe.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }
}
