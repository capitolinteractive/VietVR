using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class TimelinePanelButton : MonoBehaviour
{

    [System.Serializable]
    public struct ClipDef
    {
        public VideoClip clip;
        public Material pic;
        public string ResourceName;
        public string MatResourceName;
        // Re-Enable if Text is ever added
        //public string text;
    }

    private VideoClip loadedClip;
    private AudioClip loadedAudio;

    public bool highlighted;
    public bool pressed;

    public GameObject mainBut;
    public GameObject diagBox;
    public GameObject audioBut;
    public GameObject Vplayer;
    public GameObject bBut;
    public GameObject fBut;

    public Material videoDefault;

    public GameObject TMtxt;

    private ButtonReq butReq = null;
    private ButtonReq mainButReq = null;
    private ButtonReq audioReq = null;
    private ButtonReq nexReq = null;
    private ButtonReq prevReq = null;
    private ButtonReq vidReq = null;

    //public AudioClip audioTex;
    public string audioResourceName;

    public ClipDef[] Media;
    public int mediaX;

    public int lenth;

    bool setup;
    public bool open;
    public bool opened;

    public Vector3 OriginPos;

    //public GameObject node;
    //GameObject GlobeLockPoint;

    public string LocationName;
    public GameObject LocationNode;

    // Use this for initialization
    void Start()
    {
        OriginPos = GetComponentInParent<Transform>().transform.localPosition;
        //GlobeLockPoint = Instantiate(node,new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        //GlobeLockPoint.transform.position += GlobeLockPoint.transform.right * -1.08f;

        butReq = GetComponent<ButtonReq>();
        if (butReq != null)
        {
            butReq.Activated += this.ButtonClick;
        }

        mainButReq = mainBut.GetComponent<ButtonReq>();
        if (mainButReq != null)
        {
            mainButReq.Activated += this.ButtonClick;
        }

        audioReq = audioBut.GetComponent<ButtonReq>();
        if (audioReq != null)
        {
            audioReq.Activated += this.PlayAudioTrack;
        }
        vidReq = Vplayer.GetComponent<ButtonReq>();
        if (vidReq != null)
        {
            vidReq.Activated += this.VidButtonClick;
        }


        nexReq = fBut.GetComponent<ButtonReq>();
        if (nexReq != null)
        {
            nexReq.Activated += this.Nex;
        }
        prevReq = bBut.GetComponent<ButtonReq>();
        if (prevReq != null)
        {
            prevReq.Activated += this.Prev;
        }

        mediaX = 0;

        lenth = Media.Length;

        if (lenth == 0)
        {
            Vplayer.SetActive(false);
        }
        else if (lenth == 1)
        {
            bBut.SetActive(false);
            fBut.SetActive(false);
        }

        setup = true;
    }

    // Update is called once per frame
    void Update()
    {
        


        if (open && !opened)
        {
            opened = true;
            GetComponentInParent<Transform>().transform.localPosition = OriginPos + new Vector3(0, 0, -.3f);
        }
        else if((!open && opened))
        {
            opened = false;
            GetComponentInParent<Transform>().transform.localPosition = OriginPos;
        }


        if (!setup)
        {
            if(Media.Length != 0)
            {
                if (Media[mediaX].clip != null)
                {
                    Vplayer.GetComponent<VideoPlayer>().clip = Media[mediaX].clip;
                    Vplayer.GetComponent<Renderer>().material = videoDefault;
                }
                else if (Media[mediaX].pic != null)
                {
                    Vplayer.GetComponent<VideoPlayer>().clip = null;
                    Vplayer.GetComponent<Renderer>().material = Media[mediaX].pic;
                }
                else if (Media[mediaX].ResourceName != "")
                {
                    loadedClip = Resources.Load(Media[mediaX].ResourceName) as VideoClip;
                    Vplayer.GetComponent<VideoPlayer>().clip =loadedClip;
                    Vplayer.GetComponent<Renderer>().material = videoDefault;

                    // stop audio
                    if(audioBut.GetComponent<AudioSource>().clip != null)
                    {
                        audioBut.GetComponent<AudioSource>().Stop();
                    }
                    
                }
                else if (Media[mediaX].MatResourceName != "")
                {
                    Vplayer.GetComponent<VideoPlayer>().clip = null;
                    Vplayer.GetComponent<Renderer>().material = Resources.Load(Media[mediaX].MatResourceName) as Material;
                }

                //Re-Enable if Text is ever added
                //TMtxt.GetComponentInChildren<TextMeshPro>().SetText(Media[mediaX].text);

            }



            setup = true;
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
        if (open)
        {
            Close();
        }
        else if (!open)
        {
            //For Globelockpoint via spawned points
            //GlobeController.Current.target = GlobeLockPoint;

            //For rotated globe lol
            GlobeController.Current.target = this.gameObject;

            //GlobeController.Current.parentTarget = this.gameObject.transform.parent.transform.parent.gameObject;

            //GlobeController.Current.moveCommand = true;
            GlobeController.Current.StartLerping();
            TimelineController.Current.Selection();
            open = true;
            diagBox.SetActive(true);

            //GlobeRotateToCountry
            WPM.GlobeEventHandler.Current.GoTo(1, LocationName, LocationNode);
           
            setup = false;

        }
        
    }

    public void Close()
    {
        if (loadedAudio != null)
        {
            if(GetComponentInChildren<AudioSource>().clip != null)
            {
                GetComponentInChildren<AudioSource>().clip = null;
            }
            
            Resources.UnloadAsset(loadedAudio);
            loadedAudio = null;
        }

        if (loadedClip != null)
        {
            Vplayer.GetComponent<VideoPlayer>().clip = null;
            Resources.UnloadAsset(loadedClip);
            loadedClip = null;
        }
        open = false;
        diagBox.SetActive(false);
    }

    public void PlayAudioTrack()
    {
        if (audioResourceName != null && loadedAudio == null )
        {
            loadedAudio = Resources.Load(audioResourceName) as AudioClip;
            GetComponentInChildren<AudioSource>().clip = loadedAudio;
        }

        if (!audioBut.GetComponent<AudioSource>().isPlaying)
        {
            audioBut.GetComponent<AudioSource>().Play();
        }
        else
        {
            audioBut.GetComponent<AudioSource>().Stop();
        }

        if (Vplayer.GetComponent<VideoPlayer>().isPlaying)
        {
            Vplayer.GetComponent<VideoPlayer>().Pause();
        }
        
    }
    public void VidButtonClick()
    {
        if(!Vplayer.GetComponent<VideoPlayer>().isPlaying)
        {
            Vplayer.GetComponent<VideoPlayer>().Play();
        }
        else
        {
            Vplayer.GetComponent<VideoPlayer>().Pause();
        }

        if (audioBut.GetComponent<AudioSource>().isPlaying)
        {
            audioBut.GetComponent<AudioSource>().Pause();
        }

    }



    public void Nex()
    {

        /*
        if(Media[mediaX].clip != null && Media[mediaX].pic != null)
        {
            mediaX = 0;
        }
        */

        if (loadedClip != null)
        {
            Resources.UnloadAsset(loadedClip);
            Vplayer.GetComponent<VideoPlayer>().clip = null;
            loadedClip = null;
        }


        if (mediaX  ==  Media.Length -1)
        {
            mediaX = 0;
           
        }
        else
        {
            mediaX++;
        }

        setup = false;
    }
    public void Prev()
    {

        if (loadedClip != null)
        {
            Resources.UnloadAsset(loadedClip);
            Vplayer.GetComponent<VideoPlayer>().clip = null;
            loadedClip = null;
        }
        

        if (mediaX == 0)
        {
            mediaX = Media.Length -1;
           
        }
        else if(mediaX != 0)
        {
            mediaX--;
        }

        setup = false;
    }

}
