using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home_Menu_Control : MonoBehaviour {

    public static Home_Menu_Control Current { get; private set; }

    public GameObject Loading;

    public GameObject Inter;
    public GameObject Time;
    public GameObject ThroughEyes;

    public GameObject InterviewSel;
    public GameObject TimeSel;
    public GameObject ThroughSel;

    public GameObject InterviewDeepHold;
    public GameObject TimeDeephold;
    public GameObject ThroughDeepHold;

    public GameObject InterviewNotice;
    public GameObject TimeNotice;
    public GameObject TTENotice;

    public GameObject interPopup;
    public GameObject timePopup;
    public GameObject ttePopup;

    public GameObject Main;

    public GameObject TimelineLoader;
    public GameObject InterviewLoader;
    public GameObject ThroughEyesLoader;

    public GameObject sai1Loader;
    public GameObject LzLoader;
    public GameObject sai2Loader;
    public GameObject officeLoader;
    public GameObject home2Loader;

    bool Ding;

    public bool loadSceneActivated;
    public float loadSceneTimer;

    private void Awake()
    {
        Current = this;
    }
    private void Start()
    {

        if (StaticHolder.Current.GameVersion == 0)
        {
            /*
            TimeSel.SetActive(true);
            InterviewSel.SetActive(false);
            ThroughSel.SetActive(false);
            */

            TimeNotice.SetActive(false);
            timePopup.SetActive(false);

            //VRpointer.Current.fadepref = TimelineLoader;
        }
        else if (StaticHolder.Current.GameVersion == 1)
        {
            /*
            TimeSel.SetActive(false);
            InterviewSel.SetActive(true);
            ThroughSel.SetActive(false);
            */

            InterviewNotice.SetActive(false);
            interPopup.SetActive(false);

            //VRpointer.Current.fadepref = InterviewLoader;
        }
        else if (StaticHolder.Current.GameVersion == 2)
        {
            /*
            TimeSel.SetActive(false);
            InterviewSel.SetActive(false);
            ThroughSel.SetActive(true);
            */

            TTENotice.SetActive(false);
            ttePopup.SetActive(false);

            //VRpointer.Current.fadepref = ThroughEyesLoader;
        }

    }

    public void Update()
    {
        if(VRpointer.Current != null && !Ding)
        {
            if (StaticHolder.Current.GameVersion == 0)
            {
                VRpointer.Current.fadepref = TimelineLoader;
                Ding = true;
            }
            else if (StaticHolder.Current.GameVersion == 1)
            {
                VRpointer.Current.fadepref = InterviewLoader;
                Ding = true;
            }
            else if (StaticHolder.Current.GameVersion == 2)
            {
                VRpointer.Current.fadepref = ThroughEyesLoader;
                Ding = true;
            }
        }

        if(loadSceneTimer > 0)
        {
            loadSceneTimer -= UnityEngine.Time.deltaTime;
        }
        else
        {
            loadSceneActivated = false;
        }


    }

    public void Revert()
    {
        Inter.SetActive(false);
        Time.SetActive(false);
        ThroughEyes.SetActive(false);
        Main.SetActive(true);
    }

    public void LoadViz()
    {
        Loading.gameObject.SetActive(true);
    }
}
