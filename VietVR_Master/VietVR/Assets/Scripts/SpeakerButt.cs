using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeakerButt : MonoBehaviour {


    public int SpeakerChange;

    public bool highlighted;
    public bool pressed;

    private ButtonReq butReq = null;

    Vector3 initialScale;

    // Use this for initialization
    void Start () {
        
        butReq = GetComponent<ButtonReq>();
        if (butReq != null)
        {
            butReq.Activated += this.ButtonClick;
        }

        initialScale = transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        highlighted = GetComponent<ButtonReq>().highlighted;
        pressed = GetComponent<ButtonReq>().pressed;

        if (highlighted && !pressed)
        {
            transform.localScale = initialScale * 1.3f;
           
        }
        else if (!highlighted && !pressed)
        {
            transform.localScale = initialScale;
           
        }
        else if (pressed)
        {
            transform.localScale = initialScale * 1.3f;
           
        }

    }
    void OnDestroy()
    {
        if (butReq != null)
        {
            butReq.Activated -= this.ButtonClick;
            butReq = null;
        }
    }
    public void ButtonClick()
    {
        VidPlayer.Current.setup = false;
       
            UImanager.Current.speakerState = SpeakerChange;
       

        //VidPlayer.Current.curClip = UImanager.Current.Speakers[UImanager.Current.speakerState].Questions[0].clip;
        //VidPlayer.Current.VidResource = UImanager.Current.Speakers[UImanager.Current.speakerState].Questions[0].vidResource;
        //VidPlayer.Current.speakTM.SetActive(true);
        //VidPlayer.Current.speakTM.GetComponent<TextMeshPro>().SetText(UImanager.Current.Speakers[UImanager.Current.speakerState].Name);
        VidPlayer.Current.speakerPic.GetComponent<Renderer>().material = UImanager.Current.Speakers[UImanager.Current.speakerState].stillFrame;
        VidPlayer.Current.LoadNewVid(UImanager.Current.Speakers[UImanager.Current.speakerState].Questions[0].vidResource);
        //VidPlayer.Current.PlayButton();
        VidPlayer.Current.Hide();
        transform.parent.gameObject.SetActive(false);
    }
}
