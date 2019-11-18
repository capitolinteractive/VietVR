using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Oculus.Platform;

public class StaticHolder : MonoBehaviour {
    public static StaticHolder Current { get; private set; }
    //public int picIndex;
    public bool home;
    public bool saigon;
    //int maxPics = 5;

    public int TimelineState;
    public int InterviewState;

    public int GameVersion;

    public bool SideLoadTestBuild;
    public bool manualAppId;
    public bool Synchronous;


    private void Awake()
    {
        if(StaticHolder.Current != null)
        {
            if (StaticHolder.Current != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        DontDestroyOnLoad(this.gameObject);
        Current = this;

        ulong apId = 0;
        if (manualAppId)
        {
            if (GameVersion == 0)
            {
                apId = 2392788894113126;
            }
            else if (GameVersion == 1)
            {
                apId = 2925398060834283;
            }
            else if (GameVersion == 2)
            {
                apId = 2487172244639921;
            }
        }
        
        

        try
        {
            if (Synchronous)
            {
                if (manualAppId)
                {
                    Core.Initialize(apId.ToString());
                }
                else
                {
                    Core.Initialize();
                }
            }
            else
            {
                if (manualAppId)
                {
                    Core.AsyncInitialize(apId.ToString());
                }
                else
                {
                    Core.AsyncInitialize();
                }
            }

           
            
            Entitlements.IsUserEntitledToApplication().OnComplete(GetEntitlementCallback);
        }
       catch(UnityException e)
        {
            Debug.LogError("Platform failed to initialize due to exception");
            Debug.LogException(e);
            if (!SideLoadTestBuild)
            {
                UnityEngine.Application.Quit();
            }
            
        }
        
   

    }
    

	private void GetEntitlementCallback(Message msg)
    {
        if (msg.IsError)
        {
            Debug.LogError("You are Not entitled to use application");
            if (!SideLoadTestBuild)
            {
                UnityEngine.Application.Quit();
            }
        }
        else
        {
            Debug.Log("entitlement success");
        }
    }
    
}
