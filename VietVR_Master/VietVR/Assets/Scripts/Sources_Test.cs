using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class Sources_Test : MonoBehaviour {

    public static Sources_Test Current { get; private set; }

    [System.Serializable]
    public struct SourcesDef
    {
        //public string story;
        //public MovieTexture clip;
        public Material image;
        public VideoClip clip;
        //public string vidResource;
        public AudioClip aclip;
        public float volume;
        public int type;
        public string desc;
        public bool PrimarySource;
    }
    public SourcesDef[] Sources;
    public int index;
    public GameObject[] objects;
    public GameObject AudioObj;
    public GameObject VideoObj;
    public GameObject PhotoObj;
    public GameObject DescText;

    public GameObject QuestionNum;

    public bool answerable;
    public bool hasBeenClick;

    ushort VtrackIndex;

    // Use this for initialization
    void Start() {
        Current = this;
        index = 0;
       
    }

    // Update is called once per frame
    void Update() {

        if(OfficeControl.Current.gameState == 7)
        {
            if (GetComponent<ButtonReq>().pressed)
            {
                if (Sources[index].type == 0)
                {
                    objects[0].SetActive(true);
                    PhotoObj.transform.GetChild(0).GetComponent<Renderer>().material = Sources[index].image;
                }
                else if (Sources[index].type == 1)
                {
                    objects[1].SetActive(true);
                    
                    VideoObj.GetComponentInChildren<VideoPlayer>().clip = Sources[index].clip;
                    VideoObj.GetComponentInChildren<VideoPlayer>().GetDirectAudioVolume(VtrackIndex);
                    VideoObj.GetComponentInChildren<VideoPlayer>().SetDirectAudioVolume(VtrackIndex, Sources[index].volume);

                }
                else if (Sources[index].type == 2)
                {
                    objects[2].SetActive(true);
                    AudioObj.GetComponent<AudioSource>().clip = Sources[index].aclip;
                }
                hasBeenClick = true;
                DescText.GetComponent<TextMeshPro>().SetText(Sources[index].desc);
                QuestionNum.GetComponent<TextMeshPro>().SetText((index + 1) + "/" + Sources.Length);
            }
        }
       
    }

    public void TurnIn()
    {
        if(index < 10)
        {
            index++;
            PhotoObj.transform.GetChild(0).gameObject.SetActive(false);
            VideoObj.GetComponentInChildren<VideoPlayer>().clip = null;
            AudioObj.GetComponent<AudioSource>().clip = null;
            DescText.GetComponent<TextMeshPro>().SetText("");
            answerable = false;
        }
        else
        {
            QuestionNum.SetActive(false);
            OfficeControl.Current.gameState++;
        }
       
    }

}
