using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class ButtonScript : MonoBehaviour
{

    public VideoClip clip;
    public string VidResources;
    public string text;
    public bool isQuestion;
    public Material[] iconPic;
    public GameObject icon;
    public GameObject bigIcon;
    public int iconType;
    public GameObject Tmpro;
    public Material bigIconMat;
    public Material bigIconMatPres;
    public Material bigIconMatHigh;

    public Material buttonImg;
    public Material buttonImgPres;
    public Material buttonImgHigh;

    public bool highlighted;
    public bool pressed;

    private ButtonReq butReq = null;

    Vector3 initialScale;

    // Use this for initialization
    void Start()
    {
        initialScale = transform.localScale;

        butReq = GetComponent<ButtonReq>();
        if (butReq != null)
        {
            butReq.Activated += this.ButtonClick;
        }

        //GetComponentInChildren<TextMeshPro>().SetText(text);
        
            icon.GetComponent<Renderer>().material = iconPic[iconType];
        
        if (!isQuestion)
        {
            icon.SetActive(false);
            Tmpro.SetActive(false);
            bigIcon.SetActive(true);
            bigIcon.GetComponent<Renderer>().material = bigIconMat;
            GetComponent<Renderer>().enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //check button req
        highlighted = GetComponent<ButtonReq>().highlighted;
        pressed = GetComponent<ButtonReq>().pressed;

        if (highlighted && !pressed)
        {
            transform.localScale = initialScale * 1.2f;
            if (isQuestion)
            {
               // GetComponent<Renderer>().material = buttonImgPres;
            }
            else
            {
               // bigIcon.GetComponent<Renderer>().material = bigIconMatPres;
            }

            //GetComponent<Material>().color += Color.white;
            //GetComponent<ProceduralImage>().color = Color.blue;
        }
        else if (!highlighted && !pressed)
        {
            transform.localScale = initialScale;
            if (isQuestion)
            {
                //GetComponent<Renderer>().material = buttonImg;
            }
            else
            {
                //bigIcon.GetComponent<Renderer>().material = bigIconMat;
            }

            //GetComponent<ProceduralImage>().color = Color.white;
        }
        else if (pressed)
        {
            transform.localScale = initialScale * 1.2f;
            if (isQuestion)
            {
                //GetComponent<Renderer>().material = buttonImgHigh;
            }
            else
            {
                //bigIcon.GetComponent<Renderer>().material = bigIconMatHigh;
            }
            //GetComponent<ProceduralImage>().color = Color.red;
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
        //VidPlayer.Current.curClip = clip;
        //VidPlayer.Current.VidResource = VidResources;
        VidPlayer.Current.LoadNewVid(VidResources);
        VidPlayer.Current.PlayButton();
    }


}
