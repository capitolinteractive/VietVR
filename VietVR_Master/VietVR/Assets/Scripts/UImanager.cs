using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class UImanager : MonoBehaviour
{

    [System.Serializable]
    public struct QuestionDef
    {
        public string question;
        //public MovieTexture clip;
        public VideoClip clip;
        public string vidResource;
        public AudioClip aclip;
        //public Material image;
        public int iconNum;
    }

    [System.Serializable]
    public struct StoriesDef
    {
        //public string story;
        //public MovieTexture clip;
        public Material image;
        public VideoClip clip;
        public string vidResource;
        public AudioClip aclip;

    }

    [System.Serializable]
    public struct SpeakerDef
    {
        public string Name;
        public Material stillFrame;

        public QuestionDef[] Questions;
        public StoriesDef[] Stories;

        //public Collider IconPrefab;
        //public Texture2D[] Images;
    }

    public static UImanager Current { get; private set; }

    public SpeakerDef[] Speakers;


    public GameObject Screen;

    public int speakerState;
    public int speakerPage;
    int currentState;
    int currentPage;

    public GameObject buttonPref;

    public bool isStory;
    bool curIsStory;
    bool menuActive;


    public bool backMenu;

    void Awake()
    {
        Current = this;
    }

    // Use this for initialization
    void Start()
    {
        if(StaticHolder.Current != null)
        {
            speakerState = StaticHolder.Current.InterviewState;
            VidPlayer.Current.speakTM.GetComponent<TextMeshPro>().SetText(UImanager.Current.Speakers[UImanager.Current.speakerState].Name);
            VidPlayer.Current.speakerPic.GetComponent<Renderer>().material = UImanager.Current.Speakers[UImanager.Current.speakerState].stillFrame;
            VidPlayer.Current.LoadNewVid(UImanager.Current.Speakers[UImanager.Current.speakerState].Questions[0].vidResource);
        }
        else
        {
            speakerState = 0;
        }
        

        currentState = -1;
        //speakerPage = Speakers[speakerState].Questions.Length;

    }

    // Update is called once per frame
    void Update()
    {



        if (currentState != speakerState || currentPage != speakerPage || curIsStory != isStory)
        {
            menuActive = false;
            currentState = speakerState;
            currentPage = speakerPage;
            curIsStory = isStory;
            if (!menuActive)
            {
                menuActive = true;
                UnMakeButtons();
                MakeButtons();
            }
        }


    }

    public void MakeButtons()
    {
        if(Speakers[speakerState].Questions.Length < 10 && speakerPage == 2)
        {
            speakerPage = 0;
        }
        if (Speakers[speakerState].Questions.Length < 10 && speakerPage < 0)
        {
            speakerPage = 1;
        }

        if (speakerPage > 2)
        {
            speakerPage = 0;
            
        }
        else if (speakerPage < 0)
        {
            speakerPage = 2;

        }

        if (!isStory)
        {
            if (speakerPage == 0)
            {

                for (int i = 0; i < Speakers[speakerState].Questions.Length && i != 5; i++)
                {

                    GameObject button;

                    button = Instantiate(buttonPref, new Vector3(transform.position.x, transform.position.y + i * -0.45f, transform.position.z), transform.rotation);
                    button.transform.parent = gameObject.transform;
                    if (Speakers[speakerState].Questions[i].clip != null || Speakers[speakerState].Questions[i].vidResource != null)
                    {
                        //button.GetComponent<ButtonScript>().clip = Speakers[speakerState].Questions[i].clip;
                        button.GetComponent<ButtonScript>().VidResources = Speakers[speakerState].Questions[i].vidResource;
                    }
                    if (Speakers[speakerState].Questions[i].question != null)
                    {
                        button.GetComponentInChildren<TextMeshPro>().SetText(Speakers[speakerState].Questions[i].question.ToString());
                    }
                    /*
                    if (Speakers[speakerState].Questions[i].image != null)
                    {
                        button.GetComponentInChildren<ButtonScript>().pic = (Speakers[speakerState].Questions[i].image);
                    }
                    */
                    button.GetComponent<ButtonScript>().isQuestion = true;
                    button.GetComponent<ButtonScript>().iconType = Speakers[speakerState].Questions[i].iconNum;
                }

            }


            else if (speakerPage == 1)
            {
                for (int i = 5; i < Speakers[speakerState].Questions.Length && i != 10; i++)
                {

                    GameObject button;

                    button = Instantiate(buttonPref, new Vector3(transform.position.x, transform.position.y + (i - 5) * -0.45f, transform.position.z), transform.rotation);
                    button.transform.parent = gameObject.transform;
                    if (Speakers[speakerState].Questions[i].clip != null || Speakers[speakerState].Questions[i].vidResource != null)
                    {
                        //button.GetComponent<ButtonScript>().clip = Speakers[speakerState].Questions[i].clip;
                        button.GetComponent<ButtonScript>().VidResources = Speakers[speakerState].Questions[i].vidResource;
                    }
                    if (Speakers[speakerState].Questions[i].question != null)
                    {
                        button.GetComponentInChildren<TextMeshPro>().SetText(Speakers[speakerState].Questions[i].question.ToString());
                    }
                    /*
                    if (Speakers[speakerState].Questions[i].image != null)
                    {
                        button.GetComponentInChildren<ButtonScript>().pic = (Speakers[speakerState].Questions[i].image);
                    }
                    */
                    button.GetComponent<ButtonScript>().isQuestion = true;
                    button.GetComponent<ButtonScript>().iconType = Speakers[speakerState].Questions[i].iconNum;
                }
            }

            else if (speakerPage == 2)
            {
                for (int i = 10; i < Speakers[speakerState].Questions.Length; i++)
                {

                    GameObject button;

                    button = Instantiate(buttonPref, new Vector3(transform.position.x, transform.position.y + (i - 10) * -0.45f, transform.position.z), transform.rotation);
                    button.transform.parent = gameObject.transform;
                    if (Speakers[speakerState].Questions[i].clip != null || Speakers[speakerState].Questions[i].vidResource != null) 
                    {
                        //button.GetComponent<ButtonScript>().clip = Speakers[speakerState].Questions[i].clip;
                        button.GetComponent<ButtonScript>().VidResources = Speakers[speakerState].Questions[i].vidResource;
                    }
                    if (Speakers[speakerState].Questions[i].question != null)
                    {
                        button.GetComponentInChildren<TextMeshPro>().SetText(Speakers[speakerState].Questions[i].question.ToString());
                    }
                    /*
                    if (Speakers[speakerState].Questions[i].image != null)
                    {
                        button.GetComponentInChildren<ButtonScript>().pic = (Speakers[speakerState].Questions[i].image);
                    }
                    */
                    button.GetComponent<ButtonScript>().isQuestion = true;
                    button.GetComponent<ButtonScript>().iconType = Speakers[speakerState].Questions[i].iconNum;
                }
            }
        }
        else
        // IF STORY
        {
            for (int i = 0; i < Speakers[speakerState].Stories.Length; i++)
            {

                GameObject button;

                button = Instantiate(buttonPref, new Vector3(transform.position.x, transform.position.y + i * -0.8f, transform.position.z), transform.rotation);
                button.transform.parent = gameObject.transform;
                if (Speakers[speakerState].Stories[i].clip != null || Speakers[speakerState].Stories[i].vidResource!=null)
                {
                    //button.GetComponent<ButtonScript>().clip = Speakers[speakerState].Stories[i].clip;
                    button.GetComponent<ButtonScript>().VidResources = Speakers[speakerState].Stories[i].vidResource;
                }
                /*
                if (Speakers[speakerState].Stories[i].question != null)
                {
                    button.GetComponentInChildren<TextMeshPro>().SetText(Speakers[speakerState].Questions[i].question.ToString());
                }
                */
                if (Speakers[speakerState].Stories[i].image != null)
                {
                   button.GetComponentInChildren<ButtonScript>().bigIconMat = (Speakers[speakerState].Stories[i].image);
                }
            }
        }


    }
    public void UnMakeButtons()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

}
