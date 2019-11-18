using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VRpointer : MonoBehaviour {
    public static VRpointer Current { get; private set; }
    Transform point;
    public Transform target;

    public GameObject PreviousSelection;

    public bool buttondown;

    public bool highlighting;
    public bool pressing;

    public bool pressable;

    public LineRenderer line;

    public AudioSource HoverAudioSource;
    public AudioSource ClickAudioSource;
    public AudioClip[] Clicksounds;

    bool audioPlayed;

    public bool moveable;
    bool picMode;
    public GameObject picQuad;

    public GameObject camUI;
    bool camUp;
    Vector3 camPos;
    bool camStop;

    public AudioSource shutter;
    public GameObject flash;

    public GameObject fadepref;

    bool teleDown;

    public GameObject Lhand;
    public GameObject Rhand;
    bool RightHanded = true;

    private void Awake()
    {
        Current = this;
    }
    // Use this for initialization
    void Start () {
        
        pressable = true;
        line = GetComponent<LineRenderer>();
        if(GetComponent<MoveableCheck>() != null)
        {
            moveable = true;
            //picQuad.SetActive(true);
            picQuad = PicQuad.Current.gameObject;
        }
        else if(PicQuad.Current != null)
        {
            PicQuad.Current.gameObject.SetActive(false);
        }

        if(camUI != null)
        {
            camPos = camUI.transform.localPosition;
        }
       
    }
	
	// Update is called once per frame
	void Update () {

        OVRPlugin.Handedness handedness = OVRPlugin.GetDominantHand();
        if (handedness == OVRPlugin.Handedness.RightHanded && !RightHanded)
        {
            transform.parent = Rhand.transform;
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = Quaternion.identity;
            RightHanded = true;
        }
        else if (handedness == OVRPlugin.Handedness.LeftHanded && RightHanded)
        {
            transform.parent = Lhand.transform;
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = Quaternion.identity;
            RightHanded = false;
        }


            bool left = OVRInput.GetDown(OVRInput.Button.DpadLeft) || OVRInput.GetDown(OVRInput.Button.Left) || Input.GetKeyDown(KeyCode.LeftArrow);
        bool right = OVRInput.GetDown(OVRInput.Button.DpadRight) || OVRInput.GetDown(OVRInput.Button.Right) || Input.GetKeyDown(KeyCode.RightArrow);

        if (left || right)
        {
            int dir = left ? -1 : 1;
            transform.parent.gameObject.transform.parent.gameObject.transform.Rotate(0, dir * 45f, 0);
        }

        if (OVRInput.GetDown(OVRInput.Button.Back) || Input.GetKeyDown(KeyCode.Space))
        {
            if(UImanager.Current != null)
            {
                if (!UImanager.Current.backMenu)
                {
                    UImanager.Current.backMenu = true;
                }
                else
                {
                    UImanager.Current.backMenu = false;
                }
                print("detecting uimanager");
            }
            else if (TimelineController.Current != null)
            {
                /*
                if (!TimelineController.Current.backMenu)
                {
                    TimelineController.Current.backMenu = true;
                }
                else
                {
                    TimelineController.Current.backMenu = false;
                }
                print("detecting timelinecontroll");
                */
                TimelineController.Current.Menu();
            }
            /*
            else if (OfficeControl.Current != null)
            {
                if (!OfficeControl.Current.backMenu)
                {
                    OfficeControl.Current.backMenu = true;
                }
                else
                {
                    OfficeControl.Current.backMenu = false;
                }
                print("detecting office");
            }
            else if (LzControl.Current != null)
            {
                if (!LzControl.Current.backMenu)
                {
                    LzControl.Current.backMenu = true;
                }
                else
                {
                    LzControl.Current.backMenu = false;
                }
                print("detecting lz");
            }
            */

            /*
            else if (Home_Menu_Control.Current != null)
            {
                Home_Menu_Control.Current.Revert();
            }
            */

            else
            {
                if(BackMenu.Current != null)
                {
                    if (BackMenu.Current.isActiveAndEnabled)
                    {
                        BackMenu.Current.Close();
                        print("tryin to close");
                    }
                    else
                    {
                        BackMenu.Current.gameObject.SetActive(true);
                        print("tryin to open");
                    }
                }
                
                
                    
            }

        }


            Vector3 fwd = transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, fwd, out hit, 50))
        {

            target = hit.transform;

            
            line.SetPosition(0,new Vector3(0,0, Vector3.Distance(hit.transform.position, this.transform.position)));

            if(target.gameObject == picQuad || target.gameObject.GetComponent<PicQuad>())
            {
                if (!camStop)
                {
                    if (Input.GetMouseButtonDown(0) || OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
                    {

                        StartCoroutine(Photo());

                    }

                    if (!camUp)
                    {
                        camUI.SetActive(true);
                        camUp = true;
                    }
                    if (camUI.transform.localPosition.y <= camPos.y - 0.001f)
                    {
                        camUI.transform.Translate(Vector3.up * Time.deltaTime * 0.6f);
                    }
                    else if (camUI.transform.localPosition.y > camPos.y)
                    {
                        camUI.transform.localPosition = new Vector3(camPos.x, camPos.y, camPos.z);
                    }
                }
                

                line.SetPosition(0, new Vector3(0, 0, 0));
                return;
            }
            else
            {
                if(camUI != null)
                {
                    if (camUI.transform.localPosition.y >= camPos.y - .2f)
                    {
                        camUI.transform.Translate(Vector3.down * Time.deltaTime * 1);
                    }
                    else if (camUp)
                    {
                        camUI.SetActive(false);
                        camUp = false;
                    }
                }
               
            }

            if (target.gameObject != null)
            {

            }

            if (PreviousSelection != target.gameObject)
            {
                if (PreviousSelection != null)
                {
                    PreviousSelection.GetComponent<ButtonReq>().highlighted = false;
                    PreviousSelection.GetComponent<ButtonReq>().pressed = false;
                    audioPlayed = false;
                }
                if (target.gameObject.GetComponent<ButtonReq>())
                {
                    PreviousSelection = target.gameObject;
                }
            }

            if (target.gameObject.GetComponent<ButtonReq>())
            {
                /*
                if(target.GetComponent<ButtonReq>().highlighted == false)
                {
                    HoverAudioSource.Play();
                }
                */
                if (!audioPlayed)
                {
                    if (target.gameObject.GetComponent<Highlightable>() != null)
                    {
                        if (target.gameObject.GetComponent<Highlightable>().gameStateDepend == true)
                        {
                            if(target.gameObject.GetComponent<Highlightable>().gamStat == target.gameObject.GetComponent<Highlightable>().gameStateNeeded)
                            {
                                //highlighting = true;
                                HoverAudioSource.Play();
                                audioPlayed = true;
                            }
                        }
                    } 
                    else
                    {
                        //highlighting = true;
                        HoverAudioSource.Play();
                        audioPlayed = true;
                    }
                    
                    
                }
               
                target.GetComponent<ButtonReq>().highlighted = true;
            }

            /*
            if (target.gameObject.GetComponent<ButtonScript>())
            {
            }
            if (target.gameObject.GetComponent<InterfaceButtons>())
            {
                target.gameObject.GetComponent<InterfaceButtons>().highlighted = true;
            }
            */

            //edited out  
            if (Input.GetMouseButtonDown(0) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {

                if (target.gameObject.GetComponent<ButtonReq>())
                {
                    target.GetComponent<ButtonReq>().pressed = true;
                }

                if (pressable)
                {
                    if(target.GetComponent<ButtonReq>())
                    {
                        target.GetComponent<ButtonReq>().ButtonActivated();
                        pressable = false;

                        ClickAudioSource.Play();
                    }
                    
                }

            }
            //ENABLE IF NOT MOVABLE
            if (!moveable)
            {
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    if (target.gameObject.GetComponent<ButtonReq>())
                    {
                        target.GetComponent<ButtonReq>().pressed = true;
                    }

                    if (pressable)
                    {
                        if (target.GetComponent<ButtonReq>())
                        {
                            target.GetComponent<ButtonReq>().ButtonActivated();
                            pressable = false;

                            ClickAudioSource.Play();
                        }

                    }
                }

                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    if (target.gameObject.GetComponent<ButtonReq>())
                    {
                        target.GetComponent<ButtonReq>().pressed = false;
                    }
                    pressable = true;
                }
            }
            else if (moveable)
            {
                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    teleDown = true;
                   
                }
                if (OVRInput.GetUp(OVRInput.Button.One) && teleDown)
                {
                    StartCoroutine(MoveHint());
                    teleDown = false;
                }

            }

            //edited out || OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch)
            if (Input.GetMouseButtonUp(0) || OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                if (target.gameObject.GetComponent<ButtonReq>())
                {
                    target.GetComponent<ButtonReq>().pressed = false;
                }
                pressable = true;
            }


        }

        else
        {
            line.SetPosition(0, new Vector3(0, 0, 20));
            if(target != null)
            {
                if(target.GetComponent<ButtonReq>() != null)
                {
                    target.GetComponent<ButtonReq>().pressed = false;
                }
                
            }
            pressable = true;

            if (PreviousSelection != null)
            {
                PreviousSelection.GetComponent<ButtonReq>().highlighted = false;
                PreviousSelection.GetComponent<ButtonReq>().pressed = false;
                
                PreviousSelection = null;

            }

            audioPlayed = false;
        }

	}

    public IEnumerator Photo()
    {
        //ScreenCapture.CaptureScreenshot("Assets/picture" + StaticHolder.Current.picIndex.ToString() + ".png");
        //StaticHolder.Current.picIndex++;

        if (shutter != null)
        {
            shutter.Play();
        }
        yield return new WaitForSeconds(0.05f);
        GameObject daFlash;
        daFlash = Instantiate(flash, PlayerIndicator.Current.transform.position, PlayerIndicator.Current.transform.rotation);
        daFlash.transform.parent = PlayerIndicator.Current.transform;
        if(Home_control.Current != null)
        {
            if(Home_control.Current.gameState == 16)
            {
                yield return new WaitForSeconds(0.5f);
                Home_control.Current.gameState++;
            }
        }
        yield return null;
    }

    public void SceneSwap(string x)
    {
        StartCoroutine(SceneChange(x));
    }

    private IEnumerator SceneChange(string x)
    {
        if(BGMScript.Current != null)
        {
            BGMScript.Current.fading = true;
        }

        camStop = true;
        if(camUI != null)
        {
            camUI.SetActive(false);
        }

        if(PlayerIndicator.Current != null)
        {
            if(PlayerIndicator.Current.Hud != null)
            {
                PlayerIndicator.Current.WipeObjective();
            }
            
        }
        
        GameObject daFlash;
        daFlash = Instantiate(fadepref, PlayerIndicator.Current.transform.position, PlayerIndicator.Current.transform.rotation);
        daFlash.transform.parent = PlayerIndicator.Current.transform;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(x);
    }
    private IEnumerator MoveHint()
    {
        yield return new WaitForSeconds(0.25f);
        Hint.Current.transform.rotation = PlayerIndicator.Current.gameObject.transform.rotation;

        Hint.Current.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        Hint.Current.transform.position = PlayerIndicator.Current.gameObject.transform.position;
        yield return null;
    }


}
