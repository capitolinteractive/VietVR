using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;


public class Home_Menu_Buttons : MonoBehaviour {
    ButtonReq butReq;
    public Vector3 initialScale;
    bool highlighted;
    bool pressed;

    public string LoadScene;
    //public bool overLoad;
    public int TimelinePos;
    public int Speaker;
    public bool Home2;
    public bool Saigon2;

    public GameObject loaderOverwrite;
    public int GameVer;
    

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
        //Home_Menu_Control.Current.Loading.gameObject.SetActive(true);

        if(!Home_Menu_Control.Current.loadSceneActivated)
        {
            Home_Menu_Control.Current.loadSceneActivated = true;
            Home_Menu_Control.Current.loadSceneTimer = 1.5f;


            if (GameVer != StaticHolder.Current.GameVersion)
            {
                if (GameVer == 0) //timeline
                {
                    print("loading timeline");
                    var options = new ApplicationOptions(); options.SetDeeplinkMessage("deep_timeline");
                    try
                    {
                        Oculus.Platform.Application.LaunchOtherApp(2392788894113126, options);
                    }
                    catch (UnityException e)
                    {
                        Debug.LogError("Failed to Launch Deep link");
                        Debug.LogException(e);
                    }


                }
                else if (GameVer == 1) //interview
                {
                    print("loading interview");
                    var options = new ApplicationOptions(); options.SetDeeplinkMessage("deep_interview");
                    try
                    {
                        Oculus.Platform.Application.LaunchOtherApp(2925398060834283, options);
                    }
                    catch (UnityException e)
                    {
                        Debug.LogError("Failed to Launch Deep link");
                        Debug.LogException(e);
                    }
                }
                else if (GameVer == 2) //story
                {
                    print("loading tte");
                    var options = new ApplicationOptions(); options.SetDeeplinkMessage("deep_througheyes");
                    
                    try
                    {
                    Oculus.Platform.Application.LaunchOtherApp(2487172244639921, options);
                    }
                    catch (UnityException e)
                    {
                        Debug.LogError("Failed to Launch Deep link");
                        Debug.LogException(e);
                    }
                }
            }
            else
            {
                if (TimelinePos > 0)
                {
                    StaticHolder.Current.TimelineState = TimelinePos;
                }
                else
                {
                    StaticHolder.Current.TimelineState = 0;
                }
                if (Speaker > 0)
                {
                    StaticHolder.Current.InterviewState = Speaker;
                }
                else
                {
                    StaticHolder.Current.InterviewState = 0;
                }
                if (Home2)
                {
                    StaticHolder.Current.home = true;
                }
                else
                {
                    StaticHolder.Current.home = false;
                }
                if (Saigon2)
                {
                    StaticHolder.Current.saigon = true;
                }
                else
                {
                    StaticHolder.Current.saigon = false;
                }

                if (loaderOverwrite != null)
                {
                    VRpointer.Current.fadepref = loaderOverwrite;
                }

                //Home_Menu_Control.Current.LoadViz();
                
                VRpointer.Current.SceneSwap(LoadScene);
                Debug.Log(LoadScene + " load scene");
            }


            
        }
        else
        {
            print("Waiting to process, reset in " + Home_Menu_Control.Current.loadSceneTimer);
        }

        
    }

}
