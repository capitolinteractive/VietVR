using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {
    public float xrot;
    public float yrot;
    public float zrot;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * Time.deltaTime * zrot);
        transform.Rotate(Vector3.forward * Time.deltaTime * xrot);
        transform.Rotate(Vector3.left * Time.deltaTime * yrot);
    }
}
