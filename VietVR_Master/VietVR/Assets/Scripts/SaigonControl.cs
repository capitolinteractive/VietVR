using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

public class SaigonControl : MonoBehaviour
{
    public static SaigonControl Current { get; private set; }

    Coroutine coru;

    public int gameState;
    public bool FirstTime;

    public bool start;

    public GameObject setArea;
    public GameObject EnterArea;

    public float timer;
    bool timeactive;

    public RogoDigital.Lipsync.LipSync anLip;
    public RogoDigital.Lipsync.LipSyncData[] anLipClip;

    Animator a_Animator;
    public GameObject announcer;
    Vector3 anlastPos;
    public NavMeshAgent anAgent;

    public AudioClip[] carterClip;
    AudioSource carterAudio;

    public AudioClip[] editClip;
    public AudioSource editAudio;

    bool moving;
    const float ArriveDist = 0.4f;
    const float WalkSpeedMin = 0.2f;

    public GameObject lights;
    public GameObject fan;

    public VideoPlayer SkyVid;
    public GameObject SkyPlaceHolder;
    public GameObject SkyBox2;

    public GameObject[] MovePoints;

    public GameObject chair;
    public GameObject radioDoor;
    public GameObject closetDoor;
    public GameObject cabinet;

    public GameObject tutorialText;

    bool crIsRun;
    bool setup;

    int pointlessWait;

    public GameObject mapBoard;
    public GameObject mapBoardTwo;
    public GameObject mapBoardOrigin;
    public GameObject mapBoardOriginTwo;

    public GameObject closetFloor;

    public GameObject fadePrefReplace;

    public GameObject glowCube;
    public GameObject phone;


    // Use this for initialization
    void Start()
    {
        Current = this;
        pointlessWait = 0;
        gameState = 0;

        carterAudio = GetComponent<AudioSource>();
        if(StaticHolder.Current != null)
        {
            if (StaticHolder.Current.saigon)
            {
                FirstTime = false;
                mapBoardTwo.transform.position = mapBoardOriginTwo.transform.position;
                mapBoard.SetActive(false);
                SkyPlaceHolder.SetActive(false);
                Destroy(closetDoor.GetComponent<Highlightable>());
                Destroy(closetDoor.GetComponent<Outline>());
                Destroy(closetDoor.GetComponent<ButtonReq>());
                Destroy(radioDoor.GetComponent<Highlightable>());
                Destroy(radioDoor.GetComponent<Outline>());
                Destroy(radioDoor.GetComponent<ButtonReq>());
                Destroy(cabinet.GetComponent<Highlightable>());
                Destroy(cabinet.GetComponent<Outline>());
                Destroy(cabinet.GetComponent<ButtonReq>());
            }
            else
            {
                FirstTime = true;
                mapBoard.transform.position = mapBoardOrigin.transform.position;

                Destroy(mapBoardTwo);
                //mapBoardTwo.SetActive(false);
                Destroy(SkyBox2);
                //SkyBox2.SetActive(false);
                SkyVid.Prepare();

                Destroy(Map_Test.Current.GetComponent<Highlightable>());
                Destroy(Map_Test.Current.GetComponent<Outline>());
                Destroy(Map_Test.Current.GetComponent<ButtonReq>());

                Destroy(phone.GetComponent<Highlightable>());
                Destroy(phone.GetComponent<Outline>());
                Destroy(phone.GetComponent<ButtonReq>());
            }
        }
        else
        {
            FirstTime = true;
            mapBoard.transform.position = mapBoardOrigin.transform.position;
            //mapBoard.SetActive(false);

            Destroy(mapBoardTwo);
            //mapBoardTwo.SetActive(false);
            Destroy(SkyBox2);
            //SkyBox2.SetActive(false);
            SkyVid.Prepare();


            Destroy(Map_Test.Current.GetComponent<Highlightable>());
            Destroy(Map_Test.Current.GetComponent<Outline>());
            Destroy(Map_Test.Current.GetComponent<ButtonReq>());

            Destroy(phone.GetComponent<Highlightable>());
            Destroy(phone.GetComponent<Outline>());
            Destroy(phone.GetComponent<ButtonReq>());

        }
        

        anlastPos = Vector3.zero;
        anAgent = announcer.GetComponent<NavMeshAgent>();
        //patrolIndex = Random.Range(0, patrolPoints.Length);

        a_Animator = announcer.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!setup)
        {
            if (SkyVid.time > 0.1f)
            {
                SkyVid.time = 0;
                SkyVid.Pause();
                setup = true;
            }
        }
        */

        if (anAgent.destination != null)
        {
            if ((anAgent.transform.position - anAgent.destination).sqrMagnitude <= ArriveDist * ArriveDist)
            {
                announcer.GetComponent<Animator>().SetBool("Run", false);
            }
            else
            {
                announcer.GetComponent<Animator>().SetBool("Run", true);
            }
        }
        if (!start)
        {
            if(pointlessWait < 10)
            {
                pointlessWait++;
            }
            else
            {
                start = true;
            }
            
        }


        if (FirstTime)
        {

            //start popup
            if (start && gameState == 0)
            {
                Hint.Current.index = 5;
                Hint.Current.gameObject.SetActive(true);
                gameState++;
            }
            //talkin
            if (gameState == 2)
            {
                StartCoroutine(Startset());
                gameState++;
                //anPlaySelClip(0);


                //AnimationControl.Current.CarterStart(AnimationControl.Current.motherLipClip[0].length, 0);
                //AnimationControl.Current.CarterStart(0, 0);
            }
            //tank
            else if (gameState == 4)
            {
                //setArea.SetActive(true);
                StartCoroutine(Tank());
                gameState++;
            }
            //block the door
            else if (gameState == 6)
            {
                coru = StartCoroutine(Block());
                gameState++;
            }
            //open door
            else if (gameState == 8)
            {
                StartCoroutine(Escape());
                gameState++;
            }
            //go in closet
            else if (gameState == 10)
            {
                closetFloor.SetActive(true);
                if (crIsRun)
                {
                    StopCoroutine(coru);
                }
                anLip.Play(anLipClip[5], 0f);
                PlayerIndicator.Current.ObjectiveUpdate("Go out the closet window!");
                gameState++;
            }
            //next scene
            else if(gameState == 12)
            {
                StartCoroutine(ClosetExit());
                gameState++;
            }
        } 
        else if (!FirstTime)
        {
            if (start && gameState == 0)
            {
                Hint.Current.index = 7;
                Hint.Current.gameObject.SetActive(true);
                gameState++;
            }

            if(gameState == 2)
            {
                gameState++;
                StartCoroutine(StartTwo());
            }

            if (gameState == 5)
            {
                gameState++;
                StartCoroutine(PhoneCall());
            }

            if (gameState == 8)
            {
                gameState++;
                editAudio.loop = true;
                editAudio.clip = editClip[2];
                editAudio.Play();
                PlayerIndicator.Current.ObjectiveUpdate("Answer the phone");
            }

            if (gameState == 10)
            {
                StartCoroutine(PhoneTwo());
                gameState++;

            }
                if (gameState == 12)
            {
                VRpointer.Current.fadepref = fadePrefReplace;
                VRpointer.Current.SceneSwap("Stateside_Office");
            }
        }

    }

    public void anPlaySelClip(int x)
    {
        anLip.Play(anLipClip[x], 0f);
    }

    public IEnumerator Startset()
    {
        
        //SkyVid.time = 0.0f;
        SkyVid.Play();
        SkyVid.gameObject.GetComponent<AudioSource>().Play();
        SkyPlaceHolder.gameObject.SetActive(false);

        anLip.Play(anLipClip[0], 0f);

        carterAudio.clip = carterClip[0];
        carterAudio.Play();

        yield return new WaitForSeconds(31.3f);
        //announcer stand
        //blast
        if(BGMScript.Current != null)
        {
            BGMScript.Current.fading = false;
            BGMScript.Current.aud.volume = 0.1f;
            BGMScript.Current.aud.Play();
        }
        announcer.GetComponent<Animator>().SetBool("Sit", false);
        yield return new WaitForSeconds(2.5f);
        chair.GetComponent<Animator>().SetBool("MoveChair", true);

        yield return new WaitForSeconds(anLipClip[0].length- 33.3f);
        chair.GetComponent<Animator>().SetBool("MoveChair", false);
        yield return new WaitForSeconds(0.6f);
        announcer.GetComponent<StepBack>().go = true;
        announcer.GetComponent<Animator>().SetBool("Sit", true);
        PlayerIndicator.Current.ObjectiveUpdate("Photograph the streets outside");

        yield return new WaitForSeconds(4f);//added delay reminder how to take pictures

        //COMMENTED THIS OUT BECAUSE IT BREAKS THINGS FOR NOW PUT IT BACK TO RE-ENABLE THE REMINDERS TO TAKE PICTURES!!!!!!!!!!!!
        //Hint.Current.index = 6;
        //Hint.Current.gameObject.SetActive(true);

        yield return new WaitForSeconds(35.4f);//was 39.4f
       
        gameState++;

        yield return null;
    }

    public IEnumerator Tank()
    {
        anLip.Play(anLipClip[1], 0f);

        carterAudio.clip = carterClip[1];
        carterAudio.Play();
       

        yield return new WaitForSeconds(8f);

        chair.GetComponent<Animator>().SetBool("MoveChair", true);
        yield return new WaitForSeconds(2f);
        announcer.GetComponent<Animator>().SetBool("Sit", false);
        
        

        //cut power
        lights.SetActive(false);
        fan.GetComponent<Rotation>().enabled = false;
        transform.GetChild(0).GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(5);
        anAgent.destination = MovePoints[1].transform.position;
        radioDoor.GetComponent<Animator>().SetTrigger("Activate");
        yield return new WaitForSeconds(anLipClip[1].length - 15f);


        PlayerIndicator.Current.ObjectiveUpdate("Move a cabinet to \nblock the door");

        gameState++;
    }

    public IEnumerator Block()
    {
        

        crIsRun = true;
        yield return new WaitForSeconds(10);

        anLip.Play(anLipClip[2], 0f);
        yield return new WaitForSeconds(anLipClip[2].length + 10);

        anLip.Play(anLipClip[3], 0f);
        yield return new WaitForSeconds(anLipClip[3].length + 10);

        anLip.Play(anLipClip[4], 0f);
        yield return new WaitForSeconds(anLipClip[4].length);
        radioDoor.GetComponent<AudioSource>().Play();
        crIsRun = false;


    }
    public IEnumerator Escape()
    {
        PlayerIndicator.Current.ObjectiveUpdate("Open the closet door!");
        //knock
        

        //BANGING NOISE
        

        
        //yield return new WaitForSeconds(anLipClip[5].length);
        radioDoor.GetComponent<AudioSource>().Play();
        yield return null;

    }
    public IEnumerator ClosetExit()
    {
        

        yield return new WaitForSeconds(1);
        if(StaticHolder.Current != null)
        {
            StaticHolder.Current.saigon = true;
        }
        
        VRpointer.Current.SceneSwap("Helicopter_Optimized");
        yield return null;
    }


        // SECOND SAIGON

        public IEnumerator StartTwo()
    {

        anLip.Play(anLipClip[6], 0f);
        yield return new WaitForSeconds(anLipClip[6].length -3);
        //PLAY PHONE NOISE
        editAudio.loop = true;
        editAudio.Play();
        PlayerIndicator.Current.ObjectiveUpdate("Answer the phone");
        gameState++;
    }

    public IEnumerator PhoneCall()
    {
        editAudio.loop = false;

        carterAudio.clip = carterClip[2];
        carterAudio.Play();
        //yield return new WaitForSeconds(carterClip[2].length);

        editAudio.clip = editClip[0];
        editAudio.Play();
        yield return new WaitForSeconds(editClip[0].length);

        tutorialText.gameObject.SetActive(true);
        PlayerIndicator.Current.ObjectiveUpdate("Place photos from the desk \non the map");
        glowCube.SetActive(true);
        gameState++;
    }

    public IEnumerator PhoneTwo()
    {
        editAudio.loop = false;

        carterAudio.clip = carterClip[3];
        carterAudio.Play();
        //yield return new WaitForSeconds(carterClip[7].length);

        editAudio.clip = editClip[1];
        editAudio.Play();
        yield return new WaitForSeconds(editClip[1].length);

        


        gameState++;
    }


}
