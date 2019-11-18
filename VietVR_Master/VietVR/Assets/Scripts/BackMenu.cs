using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackMenu : MonoBehaviour {

    public static BackMenu Current { get; private set; }
    bool bMenu;
    
    int i;
    public GameObject[] menuButton;

    public ButtonReq[] butReq;
    public string[] sceneName;

    public GameObject backBut;
    private ButtonReq backReq;

    public GameObject homeBut;
    private ButtonReq homeReq;

    public GameObject hintBut;
    private ButtonReq hintReq;

    public GameObject quitBut;
    private ButtonReq quitReq;

    private bool initialSetup;

    public GameObject loadOverwrite;

    private void Awake()
    {
        Current = this;
    }

    // Use this for initialization
    void Start () {
       
        
        SetupReq();
        gameObject.SetActive(false);

        initialSetup = true;

    }

    void SetupReq()
    {
        //foreach(GameObject but in menuButton)
        /*
        for (int i = 0; i < menuButton.Length; i++)
        {
            butReq[i] = menuButton[i].GetComponent<ButtonReq>();
            if (butReq[i] != null)
            {
                
                butReq[i].Activated += NewScene;
            }
        }
        */
        i = 0;

        butReq[i] = menuButton[i].GetComponent<ButtonReq>();
        if (butReq[i] != null)
        {
            butReq[i].Activated += NewScene0;
        }
        i++;

        butReq[i] = menuButton[i].GetComponent<ButtonReq>();
        if (butReq[i] != null)
        {
            butReq[i].Activated += NewScene1;
        }

        i++;
        butReq[i] = menuButton[i].GetComponent<ButtonReq>();
        if (butReq[i] != null)
        {
            butReq[i].Activated += NewScene2;
        }

        backReq = backBut.GetComponent<ButtonReq>();
        if (backReq != null)
        {

            backReq.Activated += Close;
        }

        homeReq = homeBut.GetComponent<ButtonReq>();
        if(homeReq != null)
        {
            homeReq.Activated += Home;
        }

        hintReq = hintBut.GetComponent<ButtonReq>();
        if(hintReq != null)
        {
            hintReq.Activated += HintOpen;
        }

        quitReq = quitBut.GetComponent<ButtonReq>();
        if (quitReq != null)
        {
            quitReq.Activated += QuitGame;
        }

    }

    private void OnEnable()
    {
        if(initialSetup)
        {
            transform.rotation = PlayerIndicator.Current.gameObject.transform.rotation;

            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            //transform.rotation = PlayerIndicator.Current.gameObject.transform.rotation;
            if (VRpointer.Current.moveable)
            {
                transform.position = PlayerIndicator.Current.gameObject.transform.position;
            }
        }
        
    }

    // Update is called once per frame
    void Update () {
      
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        if (UImanager.Current != null)
        {

            UImanager.Current.backMenu = false;

        }
        else if (TimelineController.Current != null)
        {

            TimelineController.Current.backMenu = false;

        }
    }

    public void NewScene0()
    {

        print("Load Interview");

        if(StaticHolder.Current!= null)
        {
            if(StaticHolder.Current.GameVersion == 1)
            {
                if(loadOverwrite != null)
                {
                    VRpointer.Current.fadepref = loadOverwrite;
                }
                VRpointer.Current.SceneSwap(sceneName[0]);
            }
            else
            {
                Oculus.Platform.Application.LaunchOtherApp(2925398060834283);
            }
        }


        
        //SceneManager.LoadScene(sceneName[0]);
    }
    public void NewScene1()
    {
        print("Load Timeline"); 

        if (StaticHolder.Current != null)
        {
            if (StaticHolder.Current.GameVersion == 0)
            {
                if (loadOverwrite != null)
                {
                    VRpointer.Current.fadepref = loadOverwrite;
                }
                VRpointer.Current.SceneSwap(sceneName[1]);
            }
            else
            {
                Oculus.Platform.Application.LaunchOtherApp(2392788894113126);
            }
        }


        
        //SceneManager.LoadScene(sceneName[1]);
    }
    public void NewScene2()
    {
        print("Load Through the Eyes"); 

        if (StaticHolder.Current != null)
        {
            if (StaticHolder.Current.GameVersion == 2)
            {
                if (loadOverwrite != null)
                {
                    VRpointer.Current.fadepref = loadOverwrite;
                }
                StaticHolder.Current.home = false;
                StaticHolder.Current.saigon = false;
                VRpointer.Current.SceneSwap(sceneName[2]);
            }
            else
            {
                Oculus.Platform.Application.LaunchOtherApp(2487172244639921);
            }
        }

        //SceneManager.LoadScene(sceneName[2]);
    }
    public void Home()
    {
        if (loadOverwrite != null)
        {
            VRpointer.Current.fadepref = loadOverwrite;
        }
        VRpointer.Current.SceneSwap("HomeMenu_Rainforest");

        //depricated
        //SceneManager.LoadScene(sceneName[2]);
    }

    public void HintOpen()
    {
        
        Hint.Current.index = 1;
        Hint.Current.gameObject.SetActive(true);
        Close();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
