using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LzControl : MonoBehaviour
{

    public static LzControl Current { get; private set; }
    //public AudioSource aud;
    public float gameState;
    public bool start;

    public RogoDigital.Lipsync.LipSync pOneLip;
    public RogoDigital.Lipsync.LipSyncData[] pOneLipClip;

    Animator pOne_Animator;
    public GameObject pilOne;

    public RogoDigital.Lipsync.LipSync pTwoLip;
    public RogoDigital.Lipsync.LipSyncData[] pTwoLipClip;

    Animator pTwo_Animator;
    public GameObject pilTwo;

    public AudioClip[] carterClip;
    public AudioSource carterAudio;
    public AudioSource envAudio;

    public VideoPlayer skyVid;
    bool setup;

    public bool backMenu;

    public GameObject skyplace;


    // Use this for initialization
    void Start()
    {
        Current = this;
        carterAudio = GetComponent<AudioSource>();
        skyVid.Prepare();

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!setup)
        {
            if (skyVid.time > 3f && !start)
            {
                skyVid.time = 0;
                skyVid.Pause();
                setup = true;
            }
            else if(start)
            {

                setup = true;
            }
        }
        */
        
        /*
        if(skyVid.isPrepared && skyplace.activeSelf)
        {
            skyplace.SetActive(false);
            skyVid.Play();
            skyVid.Pause(); 
            skyVid.time = 0;
            
        }
        */

        if (!start)
        {
            start = true;

        }
        else if (start && gameState == 0)
        {
            Hint.Current.index = 6;
            Hint.Current.gameObject.SetActive(true);
            gameState++;
        }


        if (start && gameState == 2)
        {
            StartCoroutine(Startset());
            gameState++;
        }

        

    }

    public IEnumerator Startset()
    {
        

        
        //skyVid.time = 0;
        skyVid.Play();
        skyplace.gameObject.SetActive(false);

        pOneLip.Play(pOneLipClip[0], 0f);
        pTwoLip.Play(pTwoLipClip[0], 0f);
        carterAudio.clip = carterClip[0];
        carterAudio.Play();
        envAudio.Play();
        yield return new WaitForSeconds(pOneLipClip[0].length);
        yield return new WaitForSeconds(1);
        //GO TO NEXT SCENE
        VRpointer.Current.SceneSwap("Saigon_GearVR");
    }

}
