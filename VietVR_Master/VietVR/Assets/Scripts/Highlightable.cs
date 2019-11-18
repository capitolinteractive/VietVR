using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Highlightable : MonoBehaviour
{
    ButtonReq ButReq;
    public bool glowMode;
    bool glowPong;
    bool initglow;

    //Note only one of the public bools should be set
    public bool door;
    public bool shelf;
    public bool collectable;
    public int collectableID;

    public bool phone;
    public bool saiPhone;
    private bool down;
    public bool tv;
    public bool setArea;

    //sources sorts
    public bool imgSources;
    public bool vidSources;
    public bool audSources;

    public bool PrimarySources;
    public bool SecondarySources;
    //map sort
    public bool MapBox;
    public int MapAnswer;
    //public Material mat;

    bool State;

    public bool DemoStarter;

    public bool gameStateDepend;
    public int gameStateNeeded;

    public GameObject child;

     Component controller;

    public AudioSource AudioOut;
    public AudioClip rightSound;
    public AudioClip wrongSound;

    public int gamStat;

    // Use this for initialization
    void Start()
    {
        ButReq = GetComponent<ButtonReq>();
        
        //glowMode = true;
        GetComponent<Outline>().OutlineWidth = 0;

        if (MapAnswer > 0)
        {
            glowMode = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Home_control.Current != null)
        {
            gamStat = Home_control.Current.gameState;
        }
        else if (SaigonControl.Current != null)
        {
            gamStat = SaigonControl.Current.gameState;
        }
        else if (OfficeControl.Current != null)
        {
            gamStat = OfficeControl.Current.gameState;
        }


        if (!gameStateDepend || gameStateNeeded == gamStat)
        {
            if (!initglow)
            {
                glowMode = true;
                initglow = true;
            }

            if (ButReq.highlighted)
            {
                glowMode = false;
            }


            if (!glowMode)
            {
                if (ButReq.highlighted)
                {
                    GetComponent<Outline>().OutlineWidth = 15f;
                }
                else
                {
                    GetComponent<Outline>().OutlineWidth = 0;
                }
            }
            else
            {
                if (GetComponent<Outline>().OutlineWidth < 15 && !glowPong)
                {
                    GetComponent<Outline>().OutlineWidth += 15f * Time.deltaTime;
                }
                else if (GetComponent<Outline>().OutlineWidth > 0 && glowPong)
                {
                    GetComponent<Outline>().OutlineWidth -= 15f * Time.deltaTime;
                }
                else
                {
                    glowPong = glowPong ? false : true;
                }
            }
        

            

            if (ButReq.pressed)
            {
                if (!down)
                {
                    if(GetComponent<Grabable>() != null)
                    {
                        GetComponent<Grabable>().grabbed = true;
                    }

                    if (DemoStarter)
                    {
                        LzControl.Current.start = true;
                        Destroy(gameObject);
                    }

                    if (collectable)
                    {
                        if (Home_control.Current != null)
                        {
                            Home_control.Current.gameState++;
                        }
                        if (SaigonControl.Current != null)
                        {
                            SaigonControl.Current.gameState++;
                        }
                        gameObject.SetActive(false);
                    }

                    if (tv)
                    {
                        if (State)
                        {
                            GetComponentInChildren<Renderer>().material.DisableKeyword("_EMISSION");
                            State = false;
                            
                        }
                        else if (!State && Home_control.Current.FirstTime)
                        {
                            //GetComponentInChildren<Renderer>().material.EnableKeyword("_EMISSION");
                            child.SetActive(true);
                            child.GetComponent<VideoPlayer>().Play();
                            State = true;
                            Home_control.Current.gameState++;
                            GetComponent<Outline>().OutlineWidth = 0;
                            //GetComponent<BoxCollider>().enabled = false;
                        }
                    }
                    else if (setArea)
                    {

                        child.SetActive(true);
                        GetComponent<Renderer>().enabled = false;
                       
                        GetComponent<BoxCollider>().enabled = false;

                        if (Home_control.Current != null)
                        {
                            Home_control.Current.gameState++;
                        }
                        if (SaigonControl.Current != null)
                        {
                            SaigonControl.Current.gameState++;
                        }
                    }
                    else if (phone)
                    {
                        //gameStateNeeded = 3;
                        OfficeControl.Current.gameState++;
                        GetComponent<Outline>().OutlineWidth = 0;
                    }
                    else if (saiPhone)
                    {
                        gameStateNeeded = 9;
                        if (!SaigonControl.Current.FirstTime)
                        {
                            SaigonControl.Current.gameState++;
                        }
                        
                        GetComponent<Outline>().OutlineWidth = 0;
                    }
                    else if(shelf)
                    {
                        if (SaigonControl.Current.FirstTime)
                        {
                            GetComponent<Animator>().SetTrigger("Activate");
                            SaigonControl.Current.gameState++;
                        }
                        GetComponent<Outline>().OutlineWidth = 0;
                    }
                    else if(door)
                    {
                        if (SaigonControl.Current.FirstTime)
                        {
                            GetComponent<Animator>().SetTrigger("Activate");
                            SaigonControl.Current.gameState++;
                        }
                        GetComponent<Outline>().OutlineWidth = 0;
                    }
                    else if (audSources)
                    {
                        if (Sources_Test.Current.Sources[Sources_Test.Current.index].type == 2 && Sources_Test.Current.hasBeenClick)
                        {
                            GetComponent<AudioSource>().Play();
                            Sources_Test.Current.answerable = true;
                        }
                            
                    }
                    else if (vidSources)
                    {
                        if (Sources_Test.Current.Sources[Sources_Test.Current.index].type == 1 && Sources_Test.Current.hasBeenClick)
                        {
                            GetComponentInChildren<VideoPlayer>().Play();
                            Sources_Test.Current.answerable = true;
                        }
                            
                    }
                    else if (imgSources)
                    {
                        if(Sources_Test.Current.Sources[Sources_Test.Current.index].type == 0 && Sources_Test.Current.hasBeenClick)
                        {
                            transform.GetChild(0).gameObject.SetActive(true);
                            Sources_Test.Current.answerable = true;
                        }
                        
                    }
                    else if (PrimarySources)
                    {
                        if(Sources_Test.Current.Sources[Sources_Test.Current.index].PrimarySource && Sources_Test.Current.answerable)
                        {
                            Sources_Test.Current.TurnIn();
                            Sources_Test.Current.answerable = false;
                            Sources_Test.Current.hasBeenClick = false;
                            Sources_Test.Current.objects[0].SetActive(false);
                            Sources_Test.Current.objects[1].SetActive(false);
                            Sources_Test.Current.objects[2].SetActive(false);
                            AudioOut.GetComponent<AudioSource>().volume = 0.7f;
                            AudioOut.GetComponent<AudioSource>().clip = rightSound;
                            AudioOut.GetComponent<AudioSource>().Play();
                        }
                        else if (!Sources_Test.Current.Sources[Sources_Test.Current.index].PrimarySource && Sources_Test.Current.answerable)
                        {
                            AudioOut.GetComponent<AudioSource>().volume = 1f;
                            AudioOut.GetComponent<AudioSource>().clip = wrongSound;
                            AudioOut.GetComponent<AudioSource>().Play();
                        }
                    }
                    else if (SecondarySources && Sources_Test.Current.answerable)
                    {
                        if (!Sources_Test.Current.Sources[Sources_Test.Current.index].PrimarySource)
                        {
                            Sources_Test.Current.TurnIn();
                            Sources_Test.Current.answerable = false;
                            Sources_Test.Current.hasBeenClick = false;
                            Sources_Test.Current.objects[0].SetActive(false);
                            Sources_Test.Current.objects[1].SetActive(false);
                            Sources_Test.Current.objects[2].SetActive(false);
                            AudioOut.GetComponent<AudioSource>().volume = 0.7f;
                            AudioOut.GetComponent<AudioSource>().clip = rightSound;
                            AudioOut.GetComponent<AudioSource>().Play();
                        }
                        else if(Sources_Test.Current.Sources[Sources_Test.Current.index].PrimarySource && Sources_Test.Current.answerable)
                        {
                            AudioOut.GetComponent<AudioSource>().volume = 1f;
                            AudioOut.GetComponent<AudioSource>().clip = wrongSound;
                            AudioOut.GetComponent<AudioSource>().Play();
                        }

                    }
                    else if(MapBox)
                    {
                        if (!SaigonControl.Current.FirstTime)
                        {
                            Map_Test.Current.OpenBox();
                        }
                           
                    }
                    else if(MapAnswer > 0)
                    {
                        Map_Test.Current.CheckAnswer(MapAnswer);
                    }
                    else
                    {


                    }
                    down = true;
                }
            }
            else
            {
                down = false;
            }

        }
    }//update
}
