using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript : MonoBehaviour {

    public static BGMScript Current { get; private set; }

    public bool fading;
    public AudioSource aud;


	// Use this for initialization
	void Start () {
        Current = this;
        aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (fading)
        {
            aud.volume -= Time.deltaTime * 0.25f;
            if (aud.volume <= 0)
            {
                fading = false;
                aud.Stop();
            }
        }
    }
}
