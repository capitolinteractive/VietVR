using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicQuad : MonoBehaviour {
    public static PicQuad Current { get; private set; }

    void Awake()
    {
        Current = this;
    }


    private void Update()
    {
        if (PlayerIndicator.Current != null)
        {
            transform.position = new Vector3(PlayerIndicator.Current.transform.position.x, PlayerIndicator.Current.transform.position.y + 1, PlayerIndicator.Current.transform.position.z);
        }
       
    }

}
