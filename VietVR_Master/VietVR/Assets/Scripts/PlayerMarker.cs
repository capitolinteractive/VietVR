using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMarker : MonoBehaviour {
    public static PlayerMarker Current { get; private set; }

    // Use this for initialization
    void Start () {
        Current = this;
	}
	
	
}
