using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterArea : MonoBehaviour {
    public GameObject player;
    public float minDist;
	// Use this for initialization
	void Start () {
        if(PlayerIndicator.Current != null)
        {
            player = PlayerIndicator.Current.gameObject;
        }
       
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerIndicator.Current != null && player == null)
        {
            player = PlayerIndicator.Current.gameObject;
        }
        if(player != null)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            if (dist < minDist)
            {
                if (Home_control.Current != null)
                {
                    Home_control.Current.gameState++;
                }
                if (SaigonControl.Current != null)
                {
                    SaigonControl.Current.gameState++;
                }
                this.gameObject.SetActive(false);
            }
        }
        
    }
}
