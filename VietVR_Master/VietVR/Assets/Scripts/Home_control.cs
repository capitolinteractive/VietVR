using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home_control : MonoBehaviour
{
    public static Home_control Current { get; private set; }
    //public static bool firstTime;
    public int gameState;
    public bool FirstTime;
    bool newPos;
    public Transform mPos;
    public Transform sPos;
    public Transform dPos;
    public Transform cPos;
    public GameObject CarterObject;

    public bool start;

    public GameObject setArea;
    public GameObject EnterArea;

    public float timer;
    bool timeactive;
    float pointlessWait;
    bool displayed;

    bool displayedTwo;

    public GameObject tree;

    public Material momcloths;
    public Material sistercloths;
    public Material dadcloths;
    public GameObject momShirt;
    public GameObject sisterShirt;
    public GameObject sisterShirt2;
    public GameObject dadShirt;

    public GameObject finaleText;

    public GameObject flashNoise;

    public GameObject tv;

    public GameObject progBlock;

    public GameObject handPaper;
    public GameObject handPaperVr;

    public GameObject loaderOverwrite;

    // Use this for initialization
    void Start()
    {
        Current = this;
        gameState = 0;
        pointlessWait = 0;

        if (StaticHolder.Current != null)
        {

            if (StaticHolder.Current.home)
            {
                FirstTime = false;
                tree.SetActive(true);
                BGMScript.Current.aud.volume = 0.0f;
                BGMScript.Current.aud.Stop();
                Destroy(tv.GetComponent<Highlightable>());
                Destroy(tv.GetComponent<Outline>());
                Destroy(tv.GetComponent<ButtonReq>());
            }
            else
            {
                FirstTime = true;
                //BGMScript.Current.fading = false;
                //BGMScript.Current.aud.volume = 0.1f;
                //BGMScript.Current.aud.Play();
            }
        }
        else
        {
            FirstTime = true;
        }

        if (!FirstTime && !newPos)
        {
            AnimationControl.Current.mother.transform.position = mPos.position;
            AnimationControl.Current.mother.transform.rotation = mPos.rotation;
            AnimationControl.Current.sister.transform.position = sPos.position;
            AnimationControl.Current.sister.transform.rotation = sPos.rotation;
            AnimationControl.Current.father.transform.position = dPos.position;
            CarterObject.transform.position = cPos.transform.position;
            CarterObject.transform.rotation = cPos.transform.rotation;

            newPos = true;


            //cloths
            momShirt.GetComponent<Renderer>().material = momcloths;
            sisterShirt2.GetComponent<Renderer>().material = sistercloths;
            Material[] sisMat = sisterShirt.GetComponent<Renderer>().materials;
            sisMat[1] = sistercloths;
            //sisterShirt.GetComponent<Renderer>().material [1] = sistercloths;
            dadShirt.GetComponent<Renderer>().material = dadcloths;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
        {
            if (pointlessWait < 10)
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
            //Start Popup
            if (start && gameState == 0)
            {
                //The original way
                /*
                Hint.Current.index = 2;
                //DISABLED INITIAL POPUP
                //Hint.Current.gameObject.SetActive(true);
                gameState++;

                //TO SKIP POPUP
                gameState++;
                */

                //Just Tutorial
                Hint.Current.index = 3;
                Hint.Current.gameObject.SetActive(true);
                //PlayerIndicator.Current.ObjectiveUpdate("Objective Updated: \nGrab the newspaper");
                gameState++;
            }

            //carter get the paper
           else  if (gameState == 2)
            {
                AnimationControl.Current.mTarget = PlayerIndicator.Current.transform;
                AnimationControl.Current.mRot = true;

                gameState++;
                AnimationControl.Current.motherPlaySelClip(0);
                //AnimationControl.Current.CarterStart(AnimationControl.Current.motherLipClip[0].length, 0);
                AnimationControl.Current.CarterStart(0, 0);
                timer = 6f;

            }
            //wait for mom to talk
           else  if (gameState == 3)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    AnimationControl.Current.mTarget = AnimationControl.Current.sink.transform;
                    AnimationControl.Current.mRot = true;

                    gameState++;
                }
            }

            //click popup
           else if (gameState == 4)
            {
                //Moved to start
                /*
                Hint.Current.index = 3;
                Hint.Current.gameObject.SetActive(true);
                //PlayerIndicator.Current.ObjectiveUpdate("Objective Updated: \nGrab the newspaper");
                gameState++;
                */
                gameState++;
                gameState++;
            }
            else if(gameState == 6 && !displayed)
            {
                PlayerIndicator.Current.ObjectiveUpdate("Grab the newspaper \nfrom the table");
                displayed = true;
            }

            //set pickup area
            else if (gameState == 7)
            {
                if (setArea.activeSelf == false)
                {
                    PlayerIndicator.Current.ObjectiveUpdate("Place newspaper on the highlighted \narea of the counter");
                    if(handPaper != null)
                    {
                        handPaper.SetActive(true);
                    }
                    if (handPaperVr != null)
                    {
                        handPaperVr.SetActive(true);
                    }
                    
                    setArea.SetActive(true);
                }

            }

            //go help dad
            else if (gameState == 8)
            {
                if (handPaper != null)
                {
                    handPaper.SetActive(false);
                }
                if (handPaperVr != null)
                {
                    handPaperVr.SetActive(false);
                }
                progBlock.SetActive(false);
                gameState++;
                AnimationControl.Current.motherPlaySelClip(1);
                EnterArea.SetActive(true);
                AnimationControl.Current.sisterAgent.destination = AnimationControl.Current.patrolPoints[6].transform.position;
                //PlayerIndicator.Current.ObjectiveUpdate("Objective Updated: \nHelp dad in the living room");

                timer = 3f;
                AnimationControl.Current.mTarget = PlayerIndicator.Current.transform;
                AnimationControl.Current.mRot = true;
            }

             else if (gameState == 9)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else if(!displayedTwo)
                {
                    displayedTwo = true;
                    PlayerIndicator.Current.ObjectiveUpdate("Help dad in the living room");
                }
            }


            //carter turn on tv
            else if (gameState == 10)
            {
                gameState++;
                AnimationControl.Current.fatherPlaySelClip(0);
                PlayerIndicator.Current.ObjectiveUpdate("Turn on the TV");

                AnimationControl.Current.mRot = false;
            }

            else if(gameState == 11)
            {
                //AnimationControl.Current.dTarget = tv.transform;
                //AnimationControl.Current.dRot = true;
            }

            //argue
            else if (gameState == 12)
            {
                AnimationControl.Current.dRot = false;
                gameState++;
                AnimationControl.Current.startArgument();
            }
            //camera popup
            else if (gameState == 14)
            {
                Hint.Current.index = 4;
                Hint.Current.gameObject.SetActive(true);
                gameState++;
            }
            else if (gameState == 16)
            {
                //AnimationControl.Current.mother.transform.LookAt(PlayerIndicator.Current.gameObject.transform.position);
                //AnimationControl.Current.father.transform.LookAt(PlayerIndicator.Current.gameObject.transform.position);
                //AnimationControl.Current.sister.transform.LookAt(PlayerIndicator.Current.gameObject.transform.position);
                /*
                var pos : Vector3 = destination - transform.position; 
                var newRot = Quaternion.LookRotation(pos);
                transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 2);
                */

                int jitterWait = 0;

                if (jitterWait <= 0)
                {
                    var mtargetRotation = Quaternion.LookRotation(PlayerIndicator.Current.gameObject.transform.position - AnimationControl.Current.mother.transform.position);
                    AnimationControl.Current.mother.transform.rotation = Quaternion.Slerp(AnimationControl.Current.mother.transform.rotation, mtargetRotation, 2f * Time.deltaTime);

                    var stargetRotation = Quaternion.LookRotation(PlayerIndicator.Current.gameObject.transform.position - AnimationControl.Current.sister.transform.position);
                    AnimationControl.Current.sister.transform.rotation = Quaternion.Slerp(AnimationControl.Current.sister.transform.rotation, stargetRotation, 2f * Time.deltaTime);

                    var dtargetRotation = Quaternion.LookRotation(PlayerIndicator.Current.gameObject.transform.position - AnimationControl.Current.father.transform.position);
                    AnimationControl.Current.father.transform.rotation = Quaternion.Slerp(AnimationControl.Current.father.transform.rotation, dtargetRotation, 2f * Time.deltaTime);

                    jitterWait = 10;

                    //AnimationControl.Current.mother.GetComponent<Animator>().SetBool("Photo_pose", true);
                    //AnimationControl.Current.father.GetComponent<Animator>().SetBool("Photo_pose", true);
                }
                else
                {
                    jitterWait -= 1;
                }



            }
            else if (gameState == 17)
            {
                if (StaticHolder.Current != null)
                {
                    StaticHolder.Current.home = true;
                }

                VRpointer.Current.SceneSwap("Saigon_GearVR");
            }
        }
        else if (!FirstTime)
        {
            //Start Popup
            if (start && gameState == 0)
            {
                AnimationControl.Current.father.GetComponent<Animator>().SetBool("Sit", false);
                Hint.Current.index = 9;
                Hint.Current.gameObject.SetActive(true);
                gameState++;
            }

            if (gameState == 2)
            {
                AnimationControl.Current.StartSceneTwoPartOne();
                gameState++;
            }

            if (gameState == 4)
            {
                AnimationControl.Current.StartSceneTwoPartTwo();
                gameState++;
            }

            if (gameState == 6)
            {
                AnimationControl.Current.StartMomApollo();
                gameState++;
            }

            if (gameState == 8)
            {
                AnimationControl.Current.StartWatchTV();
                gameState++;
            }

            if (gameState == 10)
            {
                AnimationControl.Current.StartHaha();
                gameState++;
            }
            if (gameState == 12)
            {
                if (StaticHolder.Current != null)
                {
                    StaticHolder.Current.home = false;
                }

                if (loaderOverwrite != null)
                {
                    VRpointer.Current.fadepref = loaderOverwrite;
                }
                

                GameObject obj;
                obj = Instantiate(VRpointer.Current.fadepref, PlayerIndicator.Current.transform.position, PlayerIndicator.Current.transform.rotation);
                obj.transform.parent = PlayerIndicator.Current.transform;

                finaleText.SetActive(true);
                StartCoroutine(EndPause());
                gameState++;
            }
        }

    }
    public IEnumerator EndPause()
    {
       

        yield return new WaitForSeconds(4.5f);
        

        VRpointer.Current.SceneSwap("HomeMenu_Rainforest");
    }

}
