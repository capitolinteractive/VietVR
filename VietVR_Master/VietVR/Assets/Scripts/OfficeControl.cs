using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeControl : MonoBehaviour
{

    public static OfficeControl Current { get; private set; }

    public int gameState;

    public bool start;

    public GameObject setArea;
    public GameObject EnterArea;

    public AudioClip[] carterClip;
    public AudioSource carterAudio;

    public AudioClip[] editClip;
    public AudioSource editorAudio;

    public bool backMenu;
    float pointlessWait;

    public GameObject glowBox;

    // Use this for initialization
    void Start()
    {
        Current = this;
        gameState = 0;
        pointlessWait = 0;
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

        if (gameState == 0 && start)
        {
            Hint.Current.index = 8;
            Hint.Current.gameObject.SetActive(true);
            gameState++;



        }
        if (gameState == 2)
        {
            editorAudio.Play();
            PlayerIndicator.Current.ObjectiveUpdate("Answer the phone");
            gameState++;
        }

        if (gameState == 4)
        {
            gameState++;
            editorAudio.loop = false;
            StartCoroutine(Startset());
        }
        if (gameState == 6)
        {
            gameState++;
            // start sort thingy
            glowBox.SetActive(true);
        }
        if (gameState == 8)
        {
            VRpointer.Current.SceneSwap("Home_Stateside");
        }
    }

    public IEnumerator Startset()
    {
        /*
        editorAudio.clip = editClip[0];
        editorAudio.Play();
        yield return new WaitForSeconds(editClip[0].length);

        carterAudio.clip = carterClip[0];
        carterAudio.Play();
        yield return new WaitForSeconds(carterClip[0].length);

        editorAudio.clip = editClip[1];
        editorAudio.Play();
        yield return new WaitForSeconds(editClip[1].length);

        carterAudio.clip = carterClip[1];
        carterAudio.Play();
        yield return new WaitForSeconds(carterClip[1].length);
        */

        editorAudio.clip = editClip[0];
        editorAudio.Play();


        carterAudio.clip = carterClip[0];
        carterAudio.Play();
        yield return new WaitForSeconds(carterClip[0].length);

        PlayerIndicator.Current.ObjectiveUpdate("Place primary sources in the left \npile and secondary sources in \nthe right");
        gameState++;
    }

}
