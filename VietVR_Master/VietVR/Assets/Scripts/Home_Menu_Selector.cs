using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home_Menu_Selector : MonoBehaviour {
    ButtonReq butReq;
    public Vector3 initialScale;
    bool highlighted;
    bool pressed;

    public int show;
    //0 = timeline, 1 = interview, 2 = iwasthere

    public string LoadScene;

    public GameObject MyMenu;


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

        if (show == 0)
        {
            if(StaticHolder.Current.GameVersion == 0)
            {
                Home_Menu_Control.Current.Time.SetActive(true);
               
            }
            else
            {
                Home_Menu_Control.Current.TimeDeephold.SetActive(true);
            }

            // Normal Menus
            Home_Menu_Control.Current.ThroughEyes.SetActive(false);
            Home_Menu_Control.Current.Inter.SetActive(false);

            //Deeplink Menus
            Home_Menu_Control.Current.InterviewDeepHold.SetActive(false);
            Home_Menu_Control.Current.ThroughDeepHold.SetActive(false);

            //SelectionOutline
            Home_Menu_Control.Current.InterviewSel.SetActive(false);
            Home_Menu_Control.Current.ThroughSel.SetActive(false);
            Home_Menu_Control.Current.TimeSel.SetActive(true);

            //transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (show == 1)
        {
            if (StaticHolder.Current.GameVersion == 1)
            {
                Home_Menu_Control.Current.Inter.SetActive(true);
            }
            else
            {
                Home_Menu_Control.Current.InterviewDeepHold.SetActive(true);
            }

            Home_Menu_Control.Current.Time.SetActive(false);
            Home_Menu_Control.Current.ThroughEyes.SetActive(false);

            Home_Menu_Control.Current.TimeDeephold.SetActive(false);
            Home_Menu_Control.Current.ThroughDeepHold.SetActive(false);

            Home_Menu_Control.Current.InterviewSel.SetActive(true);
            Home_Menu_Control.Current.ThroughSel.SetActive(false);
            Home_Menu_Control.Current.TimeSel.SetActive(false);
        }
        else if (show == 2)
        {
            if (StaticHolder.Current.GameVersion == 2)
            {
                Home_Menu_Control.Current.ThroughEyes.SetActive(true);
            }
            else
            {
                Home_Menu_Control.Current.ThroughDeepHold.SetActive(true);
            }

            Home_Menu_Control.Current.Time.SetActive(false);
            Home_Menu_Control.Current.Inter.SetActive(false);

            Home_Menu_Control.Current.InterviewDeepHold.SetActive(false);
            Home_Menu_Control.Current.TimeDeephold.SetActive(false);

            Home_Menu_Control.Current.InterviewSel.SetActive(false);
            Home_Menu_Control.Current.ThroughSel.SetActive(true);
            Home_Menu_Control.Current.TimeSel.SetActive(false);
        }

        if(1 == 2)
        {
            if (StaticHolder.Current.GameVersion == show)
            {
                //Turnmyself Off
                //gameObject.transform.parent.gameObject.SetActive(false);

                //MyMenu.gameObject.SetActive(true);
                //transform.GetChild(0).gameObject.SetActive(true);

                /*
                if (StaticHolder.Current.GameVersion == 0)
                {
                    Home_Menu_Control.Current.TimeSel.SetActive(true);
                }
                else if (StaticHolder.Current.GameVersion == 1)
                {
                    Home_Menu_Control.Current.InterviewSel.SetActive(true);
                }
                else if (StaticHolder.Current.GameVersion == 2)
                {
                    Home_Menu_Control.Current.ThroughSel.SetActive(true);
                }
                */

                //ENABLE IN CASE OF 1968 Crashes
                /*
                if(StaticHolder.Current.GameVersion != 0)
                {
                    gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
                    MyMenu.gameObject.SetActive(true);
                }
                else
                {
                    StaticHolder.Current.TimelineState = 2;
                    VRpointer.Current.SceneSwap("main_scene");
                }
                */

            }
            else
            {
                /*
                if(StaticHolder.Current.GameVersion == 0)
                {

                    //Oculus.Platform.Application.LaunchOtherApp(appID);
                    //Also, you can get the app_id from the Oculus Dashboard of your app.
                }
                else if (StaticHolder.Current.GameVersion == 1)
                {
                    //Oculus.Platform.Application.LaunchOtherApp(appID);

                }
                else if (StaticHolder.Current.GameVersion == 2)
                {
                    //Oculus.Platform.Application.LaunchOtherApp(appID);

                }
                */

                if (show == 0) //timeline
                {
                    Oculus.Platform.Application.LaunchOtherApp(2392788894113126);
                }
                else if (show == 1) //interview
                {
                    Oculus.Platform.Application.LaunchOtherApp(2925398060834283);
                }
                else if (show == 2) //story
                {
                    Oculus.Platform.Application.LaunchOtherApp(2487172244639921);
                }
            }
        

        }

        
    }

}
