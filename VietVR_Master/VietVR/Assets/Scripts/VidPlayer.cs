using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using System.IO;

public class VidPlayer : MonoBehaviour {

    public static VidPlayer Current { get; private set; }

    public VideoClip curClip;
    public string VidResource;
    public VideoClip loadedClip;
   // public Texture curImage;
    public Material curImage;

    public VideoPlayer vplayer;

    public string speakerName;
    public string desc;

    public GameObject VideoTimeDispay;

    public GameObject speakTM;
    public GameObject descTM;
    public GameObject speakerPic;

    public GameObject loadingPic;
    public bool retry;

    Vector3 initPos;

    public bool setup;

    int curMin;
    int curSec;
    int remMin;
    int remSec;

    float loadFakeTime;

    // the place for still images when not playing
    public GameObject stillQuad;

   

	// Use this for initialization
	void Start () {
        Current = this;
        vplayer = GetComponent<VideoPlayer>();
        initPos = transform.position;

        Hide();
        /*
        vplayer.time = 0.1f;
        vplayer.Pause();
        */

        //VidResource = UImanager.Current.Speakers[UImanager.Current.speakerState].Questions[0].vidResource;
        loadedClip = Resources.Load(UImanager.Current.Speakers[UImanager.Current.speakerState].Questions[0].vidResource) as VideoClip;
        vplayer.clip = loadedClip;

        /*
        var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "myassetBundle"));
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
        
        var prefab = myLoadedAssetBundle.LoadAsset<VideoClip>(UImanager.Current.Speakers[UImanager.Current.speakerState].Questions[0].vidResource) as VideoClip;
        vplayer.clip = prefab;
        */
    }

    // Update is called once per frame
    void Update () {

        if (retry)
        {
            if(loadFakeTime > 0)
            {
                loadFakeTime -= Time.deltaTime;
            }
            else
            {
                vplayer.Play();
            }
            
            //vplayer.Play();
            Unhide();
            //retry = false;
        }

        //vplayer.prepareCompleted += FinallyLoaded();

        if(!setup)
        {
            if(vplayer.time > 0.1f)
            {
                vplayer.time = 0;
                vplayer.Pause();
                setup = true;
            }
        }

        curMin = Mathf.FloorToInt((float)vplayer.time / 60);
        curSec = Mathf.FloorToInt((float)vplayer.time - curMin * 60);
        remMin = Mathf.FloorToInt((float)vplayer.clip.length / 60);
        remSec = Mathf.FloorToInt((float)vplayer.clip.length - remMin * 60);

        VideoTimeDispay.GetComponent<TextMeshPro>().SetText(string.Format("{0:00}:{1:00}", curMin, curSec) + "/" + string.Format("{0:00}:{1:00}", remMin, remSec));

        //speakTM.GetComponent<TextMeshPro>().SetText(speakerName);
        
        //when the video ends do end reached
        vplayer.loopPointReached += EndReached;

    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vplayer.time = 0;
        vplayer.Pause();
        setup = true;
        //stillQuad.SetActive(true);
        //vplayer.enabled = false;
        //speakTM.SetActive(true);
    }

    public void LoadNewVid(string x)
    {
        Resources.UnloadAsset(loadedClip);
        loadedClip = Resources.Load(x) as VideoClip;
        vplayer.clip = loadedClip;
    }


    public void PlayButton()
    {
        loadFakeTime = 3;
        stillQuad.SetActive(false)
;       //vplayer.enabled = true;
        if (curClip != null || VidResource != null)
        {

            //vplayer.Play();
            vplayer.Prepare();
            //vplayer.Pause();
        }
        Unhide();
    }
    public void PauseButton()
    {
        if (vplayer.isPlaying)
        {
            vplayer.Pause();
        }
    }
    public void RewindButton()
    {
        
            vplayer.time = vplayer.time -10;
        
        
    }
    public void Unhide()
    {
        if (vplayer.isPlaying)
        {
            speakerPic.SetActive(false);
            loadingPic.SetActive(false);
            transform.position = initPos;
            //vplayer.Play();
            retry = false;
        }
        else if (!vplayer.isPlaying)
        {
            retry = true;
            if (!loadingPic.activeSelf)
            {
                loadingPic.SetActive(true);
            }
           
           
        }
        
    }
    public void Hide()
    {
        loadingPic.SetActive(false);
        speakerPic.SetActive(true);
        transform.Translate(new Vector3(transform.position.x, transform.position.y - 50, transform.position.z));
    }

   

}
