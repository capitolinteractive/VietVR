using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSwapper : MonoBehaviour {

    public Material normal;
    //public Material highlight;
    //public Material press;

    public bool pressed;
    public bool highlighted;

    public ButtonReq bReq;
    Renderer rend;

    Vector3 initialScale;

    public bool Swapped;
    bool IsPlaying;

    public Material Altnormal;
    //public Material Althighlight;
    //public Material Altpress;

    bool hasDrawn;

    bool hasAudio;

    // Use this for initialization
    void Start()
    {
        hasDrawn = true;
        bReq = this.GetComponent<ButtonReq>();
        rend = this.GetComponent<Renderer>();
        initialScale = transform.localScale;

        if (GetComponent<AudioSource>())
        {
            hasAudio = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (bReq.highlighted)
        {
            if(!highlighted)
            {
                highlighted = true;
                hasDrawn = false;
            }
            
        }
        else
        {
            if (highlighted)
            {
                highlighted = false;
                hasDrawn = false;
            }
        }

        if (bReq.pressed)
        {
            if (!pressed)
            {
                pressed = true;
                hasDrawn = false;
            }
            
        }
        else
        {
            if (pressed)
            {
                pressed = false;
                hasDrawn = false;
            }
            
        }

        if(!hasDrawn)
        {
            if (!Swapped)
            {
                if (rend.material != normal)
                {
                    rend.material = normal;
                }

                if (highlighted && !pressed)
                {
                    hasDrawn = true;
                    transform.localScale = initialScale * 1.2f;

                    /*
                    if (rend.material != highlight)
                    {
                        hasDrawn = true;
                        transform.localScale = initialScale * 1.2f;
                        rend.material = highlight;
                    }
                    */
                }
                else if (!highlighted && !pressed)
                {
                    hasDrawn = true;
                    transform.localScale = initialScale;
                    /*
                    if (rend.material != normal)
                    {
                        hasDrawn = true;
                        transform.localScale = initialScale;
                        rend.material = normal;
                    }
                    */
                }
                else if ((highlighted && pressed) || (!highlighted && pressed))
                {
                    hasDrawn = true;
                    transform.localScale = initialScale * 1.15f;
                    /*
                    if (rend.material != press)
                    {
                        hasDrawn = true;
                        transform.localScale = initialScale * 1.15f;
                        rend.material = press;
                    }
                    */
                }
            }
            else if (Swapped)
            {
                if (rend.material != Altnormal)
                {
                    rend.material = Altnormal;
                }

                if (highlighted && !pressed)
                {
                    hasDrawn = true;
                    transform.localScale = initialScale * 1.2f;
                    /*
                    if (rend.material != Althighlight)
                    {
                        hasDrawn = true;
                        transform.localScale = initialScale * 1.2f;
                        rend.material = Althighlight;
                    }
                    */
                }
                else if (!highlighted && !pressed)
                {
                    hasDrawn = true;
                    transform.localScale = initialScale;
                    /*
                    if (rend.material != Altnormal)
                    {
                        hasDrawn = true;
                        transform.localScale = initialScale;
                        rend.material = Altnormal;
                    }
                    */
                }
                else if ((highlighted && pressed) || (!highlighted && pressed))
                {
                    hasDrawn = true;
                    transform.localScale = initialScale * 1.2f;
                    /*
                    if (rend.material != Altpress)
                    {
                        hasDrawn = true;
                        transform.localScale = initialScale * 1.2f;
                        rend.material = Altpress;
                    }
                    */
                }
            }
        }
        


        if (hasAudio)
        {
            if (GetComponent<AudioSource>().isPlaying)
            {
                if (!Swapped)
                {
                    Swapped = true;
                    hasDrawn = false;
                }
            }
            else if (GetComponent<AudioSource>().isPlaying == false)
            {
                if (Swapped)
                {
                    Swapped = false;
                    hasDrawn = false;
                }
            }
        }


    }//end update
}
