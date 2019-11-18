using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

public class AnimationControl : MonoBehaviour
{

    public static AnimationControl Current { get; private set; }

    public RogoDigital.Lipsync.LipSync motherlip;
    public RogoDigital.Lipsync.LipSyncData[] motherLipClip;

    public RogoDigital.Lipsync.LipSync fatherlip;
    public RogoDigital.Lipsync.LipSyncData[] fatherLipClip;

    public RogoDigital.Lipsync.LipSync sisterlip;
    public RogoDigital.Lipsync.LipSyncData[] sisterLipClip;

    Animator m_Animator;
    public GameObject mother;
    Vector3 motherlastPo;
    public NavMeshAgent motherAgent;

    Animator f_Animator;
    public GameObject father;
    Vector3 fatherLastPo;
    public NavMeshAgent fatherAgent;

    Animator s_Animator;
    public GameObject sister;
    Vector3 sisterlastPo;
    public NavMeshAgent sisterAgent;

    public AudioClip[] carterClip;
    AudioSource carterAudio;

    public Transform mTarget;
    public Transform sTarget;
    public Transform dTarget;
    public bool mRot;
    public bool sRot;
    public bool dRot;

    public GameObject Televison;
    public GameObject sink;

    public GameObject[] patrolPoints;
    int patrolIndex;

    bool sisTurn;

    bool moving;
    const float ArriveDist = 0.5f;
    const float WalkSpeedMin = 0.2f;

    public VideoClip christmasClip;
    public VideoPlayer vPlayer;

    // Use this for initialization
    void Start()
    {
        Current = this;

        carterAudio = GetComponent<AudioSource>();


        //motherlastPo = Vector3.zero;
        motherAgent = mother.GetComponent<NavMeshAgent>();
        sisterAgent = sister.GetComponent<NavMeshAgent>();
        fatherAgent = father.GetComponent<NavMeshAgent>();
        patrolIndex = Random.Range(0, patrolPoints.Length);
        //motherAgent.destination = patrolPoints[patrolIndex].transform.position;
        //moving = true;
        m_Animator = mother.GetComponent<Animator>();
        f_Animator = father.GetComponent<Animator>();
        s_Animator = sister.GetComponent<Animator>();


        //sisTurn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mRot)
        {
            Quaternion lookOnLook = Quaternion.LookRotation(mTarget.transform.position - mother.transform.position);

            mother.transform.rotation = Quaternion.Slerp(mother.transform.rotation, lookOnLook, Time.deltaTime);
        }

        if (sRot)
        {
            Quaternion lookOnLook = Quaternion.LookRotation(sTarget.transform.position - sister.transform.position);

            sister.transform.rotation = Quaternion.Slerp(sister.transform.rotation, lookOnLook, Time.deltaTime);
        }

        if (dRot)
        {
            Quaternion lookOnLook = Quaternion.LookRotation(dTarget.transform.position - father.transform.position);

            father.transform.rotation = Quaternion.Slerp(father.transform.rotation, lookOnLook, Time.deltaTime);
        }


        if (motherAgent.destination != null)
        {
            if ((motherAgent.transform.position - motherAgent.destination).sqrMagnitude <= ArriveDist * ArriveDist)
            {
                mother.GetComponent<Animator>().SetBool("Walking", false);
                if(Home_control.Current.gameState == 11)
                {
                    mother.transform.LookAt(sister.transform);
                }
            }
            else
            {
                mother.GetComponent<Animator>().SetBool("Walking", true);
            }
        }

        if (sisterAgent.destination != null)
        {
            if ((sisterAgent.transform.position - sisterAgent.destination).sqrMagnitude <= ArriveDist * ArriveDist)
            {
                sister.GetComponent<Animator>().SetBool("isWalking", false);
            }
            else
            {
                sister.GetComponent<Animator>().SetBool("isWalking", true);
            }
        }
        if (fatherAgent.destination != null)
        {
            if ((fatherAgent.transform.position - fatherAgent.destination).sqrMagnitude <= ArriveDist * ArriveDist)
            {
                father.GetComponent<Animator>().SetBool("F_Walk", false);
            }
            else
            {
                father.GetComponent<Animator>().SetBool("F_Walk", true);
            }
        }


        if (sisTurn)
        {
            sister.transform.Rotate(Vector3.up * Time.deltaTime * 90f);
        }
        /*
        if ()
        {

        }
        */

       // Vector3 movement = (transform.position - motherlastPo) * (1f / Time.deltaTime);


        //m_Animator.SetBool("Walking", movement.sqrMagnitude >= WalkSpeedMin * WalkSpeedMin);
        //m_Animator.SetBool("Walking", true);

        /*
        if (moving) // meaning, moving to another hide point. when switching between cover or not, that's just a change in hiding bool
        {
            if ((motherAgent.transform.position - motherAgent.destination).sqrMagnitude <= ArriveDist * ArriveDist)
                moving = false;
        }
        else
        {
            patrolIndex = Random.Range(0, patrolPoints.Length);
            motherAgent.destination = patrolPoints[patrolIndex].transform.position;
            moving = true;
        }
        */

    }

    public void motherPlaySelClip(int x, int del = 0)
    {
        motherlip.Play(motherLipClip[x], del);
    }
    public void fatherPlaySelClip(int x, int del = 0)
    {
        fatherlip.Play(fatherLipClip[x], del);
    }
    public void sisterPlaySelClip(int x, int del = 0)
    {
        sisterlip.Play(sisterLipClip[x], del);
    }

    public void startArgument()
    {
        StartCoroutine(Argument());
        StartCoroutine(ArgumentMovements());
    }

    public IEnumerator Argument()
    {

        yield return new WaitForSeconds(7f);
        motherAgent.destination = patrolPoints[5].transform.position;
        yield return new WaitForSeconds(1f);
        sisTurn = true;
        sister.GetComponent<Animator>().SetTrigger("S_TurnL_trigger");
        sisterPlaySelClip(0, 2); // delayed

        yield return new WaitForSeconds(1f);
        sister.GetComponent<Animator>().SetTrigger("S_TurnL_trigger");
        fatherPlaySelClip(1, 1);//delayed

        yield return new WaitForSeconds(1f);
        sisTurn = false;


        motherPlaySelClip(2); //delayedish lol

        //The way it was
        /*
        sisterPlaySelClip(0);
        fatherPlaySelClip(1);
        motherPlaySelClip(2);
        */
        yield return new WaitForSeconds(8);
        father.GetComponent<Animator>().SetBool("Sit", false);
        yield return new WaitForSeconds(motherLipClip[2].clip.length -7);
       // sisterAgent.destination = patrolPoints[7].transform.position;
       // motherAgent.destination = patrolPoints[8].transform.position;
       // fatherAgent.destination = patrolPoints[9].transform.position;

        Home_control.Current.gameState++;
        PlayerIndicator.Current.ObjectiveUpdate("Take a picture");
        yield return null;
    }

    public IEnumerator ArgumentMovements()
    {
        yield return new WaitForSeconds(7f);
        //motherAgent.destination = patrolPoints[5].transform.position;
        yield return new WaitForSeconds(1f);
        //sisTurn = true;
        //sister.GetComponent<Animator>().SetTrigger("S_TurnL_trigger");
        yield return new WaitForSeconds(1f);
        // sister.GetComponent<Animator>().SetTrigger("S_TurnL_trigger");
        yield return new WaitForSeconds(1f);
        //sisTurn = false;
        sTarget = father.transform;
        sRot = true;
        mTarget = sister.transform;
        mRot = true;
        dTarget = sister.transform;
        dRot = true;
        yield return new WaitForSeconds(22f); //22 seconds
        sTarget = mother.transform;
        mTarget = sister.transform;
        yield return new WaitForSeconds(8f); //30 seconds
        sTarget = father.transform;
        mTarget = father.transform;
        yield return new WaitForSeconds(3f); //33 seconds
        dTarget = PlayerIndicator.Current.gameObject.transform;
        yield return new WaitForSeconds(3f); //36 seconds
        sTarget = PlayerIndicator.Current.gameObject.transform;
        mTarget = sister.transform;
        dTarget = sister.transform;
        yield return new WaitForSeconds(10f); // 46 seconds
        dTarget = mother.transform;
        sTarget = mother.transform;
        yield return new WaitForSeconds(2f); // 48 seconds
        mRot = false;
        sRot = false;
        dRot = false;

        yield return null;
    }

    public void StartSceneTwoPartOne()
    {
        StartCoroutine(SceneTwoPartOne());
    }
    public IEnumerator SceneTwoPartOne()
    {
        vPlayer.gameObject.GetComponent<AudioSource>().volume = 0.1f;
        vPlayer.gameObject.SetActive(true);
        vPlayer.clip = christmasClip;
        vPlayer.Play();

        sisterPlaySelClip(1);
        fatherPlaySelClip(2);
        motherPlaySelClip(3);
        carterAudio.clip = carterClip[1];
        carterAudio.Play();
;       yield return new WaitForSeconds(motherLipClip[3].clip.length - 0.5f);
        StartCoroutine(VRpointer.Current.Photo());
        Home_control.Current.flashNoise.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.5f);
        Home_control.Current.gameState++;
        yield return null;
    }
    public void StartSceneTwoPartTwo()
    {
        StartCoroutine(SceneTwoPartTwo());
        StartCoroutine(SceneTwoRotation());
    }
    public IEnumerator SceneTwoPartTwo()
    {
        sisterPlaySelClip(2);
        fatherPlaySelClip(3);
        motherPlaySelClip(4);
        carterAudio.clip = carterClip[2];
        carterAudio.Play();
        yield return new WaitForSeconds(motherLipClip[4].clip.length);
        Home_control.Current.gameState++;
        yield return null;
    }

    public IEnumerator SceneTwoRotation()
    {
        sTarget = mother.transform;
        sRot = true;
        mTarget = sister.transform;
        mRot = true;
        dTarget = mother.transform;
        dRot = true;
        yield return new WaitForSeconds(13f); //13
        sTarget = PlayerIndicator.Current.gameObject.transform;
        yield return new WaitForSeconds(3f); // 16
        dTarget = sister.transform;
        yield return new WaitForSeconds(3); // 19
        sTarget = father.transform;
        yield return new WaitForSeconds(36); // 54
        sTarget = PlayerIndicator.Current.gameObject.transform;
        mTarget = PlayerIndicator.Current.gameObject.transform;
        dTarget = PlayerIndicator.Current.gameObject.transform;

        yield return null;
    }

    public void StartMomApollo()
    {
        StartCoroutine(MomApollo());
    }
    public IEnumerator MomApollo()
    {
        
        motherPlaySelClip(5);
        yield return new WaitForSeconds(3);
        //sTarget = Televison.transform;
        sTarget = Home_control.Current.tv.transform;
        mTarget = Televison.transform;
        dTarget = Televison.transform;

        yield return new WaitForSeconds(motherLipClip[5].clip.length -3f);
        father.GetComponent<Animator>().SetBool("Sit", true);
        Home_control.Current.gameState++;

        sRot = false;
        dRot = false;
        mRot = false;

        yield return null;
    }

    public void StartWatchTV()
    {
        StartCoroutine(WatchTV());
    }
    public IEnumerator WatchTV()
    {
        //PLAY VIDEO or ADJUST VOLUME
        vPlayer.gameObject.GetComponent<AudioSource>().volume = 0.45f;

        sisterAgent.destination = patrolPoints[6].transform.position;

        yield return new WaitForSeconds(7f);
        sisterPlaySelClip(3);
        fatherPlaySelClip(4);
        motherPlaySelClip(6);
       
        yield return new WaitForSeconds(motherLipClip[6].clip.length);
        Home_control.Current.gameState++;
        yield return null;
    }

    public void StartHaha()
    {
        StartCoroutine(Haha());
    }
    public IEnumerator Haha()
    {
        yield return new WaitForSeconds(7f);
        sisterPlaySelClip(4);
        fatherPlaySelClip(5);
        motherPlaySelClip(7);
        carterAudio.clip = carterClip[3];
        carterAudio.Play();
        yield return new WaitForSeconds(3f);
        Home_control.Current.gameState++;
        yield return null;
    }

    public void CarterStart(float x,int line)
    {
        StartCoroutine(Carter(x,line));
    }

    public IEnumerator Carter(float x, int line)
    {
        yield return new WaitForSeconds(x);
        carterAudio.clip = carterClip[line];
        carterAudio.Play();

        yield return null;
    }

}
