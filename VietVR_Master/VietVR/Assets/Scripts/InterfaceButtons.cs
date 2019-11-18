using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;
using TMPro;


public class InterfaceButtons : MonoBehaviour {
    public bool home;
    public bool help;
    public bool nextSpeaker;
    public bool nextPage;
    public bool prevPage;
    public bool story;
    public bool question;
    public bool play;
    public bool pause;
    public bool rwd;


    public bool highlighted;
    public bool pressed;

   
    public GameObject buttonSetHolder;
    public GameObject orange1, orange2;

    private ButtonReq butReq = null;

    Vector3 initialScale;

    public Material Image;
    public Material ImagePres;
    public Material ImageHigh;

    public Material secImage;
    public Material secImagePres;
    public Material secImageHigh;

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

        if (play)
        {
            if (VidPlayer.Current.vplayer.isPlaying)
            {
                play = false;
                pause = true;
            }
        }
        if (pause)
        {
            if (!VidPlayer.Current.vplayer.isPlaying)
            {
                play = true;
                pause = false;
            }
        }

        /*
        if (story)
        {
            if(UImanager.Current.isStory)
            {
                //GetComponentInChildren<TextMeshPro>().SetText("Questions");
            }
            else
            {
                //GetComponentInChildren<TextMeshPro>().SetText("Stories");
            }
            
        }
        */


            if (highlighted && !pressed)
        {
            transform.localScale = initialScale * 1.3f;
            if(!pause)
            {
                GetComponent<Renderer>().material = ImagePres;
            }
            else
            {
                GetComponent<Renderer>().material = secImagePres;
            }
           
            //GetComponent<Material>().color += Color.white;
           //GetComponent<ProceduralImage>().color = Color.blue;
        }
        else if (!highlighted && !pressed)
        {
            transform.localScale = initialScale;
            if (!pause)
            {
                GetComponent<Renderer>().material = Image;
            }
            else
            {
                GetComponent<Renderer>().material = secImage;
            }
            
            //GetComponent<ProceduralImage>().color = Color.white;
        }
        else if (pressed)
        {
            transform.localScale = initialScale * 1.3f;
            if (!pause)
            {
                GetComponent<Renderer>().material = ImageHigh;
            }
           else
            {
                GetComponent<Renderer>().material = secImageHigh;
            }
            //GetComponent<ProceduralImage>().color = Color.red;
        }
        if (prevPage || nextPage) {
            if (UImanager.Current.isStory)
            {
                GetComponent<Renderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
            }
            else
            {
                GetComponent<Renderer>().enabled = true;
                GetComponent<Collider>().enabled = true;
            }
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

    void ButtonClick()
    {
        if (nextSpeaker)
        {
            if(buttonSetHolder.activeSelf == true)
            {
                buttonSetHolder.SetActive(false);
            }
            else
            {
                buttonSetHolder.SetActive(true);
            }


            /*
            VidPlayer.Current.setup = false;
            if(UImanager.Current.speakerState + 1 != 5)
            {
                UImanager.Current.speakerState += 1;
            }
            else
            {
                UImanager.Current.speakerState = 0;
            }

            //VidPlayer.Current.curClip = UImanager.Current.Speakers[UImanager.Current.speakerState].Questions[0].clip;
            //VidPlayer.Current.VidResource = UImanager.Current.Speakers[UImanager.Current.speakerState].Questions[0].vidResource;
            VidPlayer.Current.speakTM.SetActive(true);
           VidPlayer.Current.speakTM.GetComponent<TextMeshPro>().SetText(UImanager.Current.Speakers[UImanager.Current.speakerState].Name);
            VidPlayer.Current.LoadNewVid(UImanager.Current.Speakers[UImanager.Current.speakerState].Questions[0].vidResource);
            //VidPlayer.Current.PlayButton();
            */
        }
        else if (nextPage)
        {
            UImanager.Current.speakerPage += 1;
        }
        else if (prevPage)
        {
            UImanager.Current.speakerPage -= 1;
        }
        else if (story)
        {
            UImanager.Current.isStory = true;
            orange2.SetActive(true);
            orange1.SetActive(false);

        }
        else if (question)
        {

            UImanager.Current.isStory = false;
            orange1.SetActive(true);
            orange2.SetActive(false);

        }
        else if (play)
        {
            VidPlayer.Current.PlayButton();
        }
        else if (pause)
        {
            VidPlayer.Current.PauseButton();
        }
        else if (rwd)
        {
            VidPlayer.Current.RewindButton();
        }
        else if (home)
        {
            if (UImanager.Current.backMenu)
            {
                BackMenu.Current.Close();
                UImanager.Current.backMenu = false;

            }
            else if (!UImanager.Current.backMenu)
            {
                UImanager.Current.backMenu = true;
                //BackMenu.Current.Open();
                BackMenu.Current.gameObject.SetActive(true);
            }
        }
        else if (help)
        {
            if (UImanager.Current.backMenu)
            {
                BackMenu.Current.Close();
                UImanager.Current.backMenu = false;

            }


            Hint.Current.index = 1;
            Hint.Current.gameObject.SetActive(true);
        }
        else
        {
            print("interface button not set");
        }

    }

}
