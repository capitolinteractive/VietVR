using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Map_Test : MonoBehaviour {
    public static Map_Test Current { get; private set; }

    [System.Serializable]
    public struct MapssDef
    {
        public GameObject paper;
        public int QuadrentAnswers;
        public string desc;
    }
    bool IsActive;
    public GameObject pile;
    public MapssDef[] Papers;
    public GameObject TextMPro;
    public GameObject QuestionText;
   
    //null(0),NorthV,SouthV,Gulf,MyLy,Trail,China,Cambodia,null
    //public GameObject Q1, Q2, Q3, Q4, Q5,Q6,Q7,Q8;
    public GameObject[] Buttons;
    public int index;

    public bool Usable;

    public AudioSource AudioOut;
    public AudioClip rightSound;
    public AudioClip wrongSound;
    public GameObject Buttholder;

    public GameObject GlowBox;

    private void Awake()
    {
        Current = this;
    }
    // Use this for initialization
    void Start () {
        
        index = 0;
        IsActive = true;
        Usable = true;
	}
	
    /*
	// Update is called once per frame
	void Update () {

        
    }
    */

    public void OpenBox()
    {
        /*
        if (GlowBox.activeSelf)
        {
            GlowBox.SetActive(false);
        }
        */
         
        if (Buttholder.activeSelf == false)
        {
            Buttholder.SetActive(true);
        }

        if (Usable)
        {
            Papers[index].paper.SetActive(true);
            TextMPro.GetComponent<TextMeshPro>().SetText(Papers[index].desc);
            QuestionText.GetComponent<TextMeshPro>().SetText((index+1) + "/" + Papers.Length);
            Usable = false;
        }

    }
    public void CheckAnswer(int x)
    {
        if(Papers[index].paper.activeSelf == true)
        {
            if (Papers[index].QuadrentAnswers == x)
            {
                Answer();
                StartCoroutine(ReColor(x, true));
                TextMPro.GetComponent<TextMeshPro>().SetText("");
            }
            else
            {
                StartCoroutine(ReColor(x, false));
                AudioOut.GetComponent<AudioSource>().volume = 1f;
                AudioOut.GetComponent<AudioSource>().clip = wrongSound;
                AudioOut.GetComponent<AudioSource>().Play();

            }
        }
       
    }

    public void Answer()
    {
        if(index < 10)
        {
            Papers[index].paper.SetActive(false);
            AudioOut.GetComponent<AudioSource>().volume = 0.7f;
            AudioOut.GetComponent<AudioSource>().clip = rightSound;
            AudioOut.GetComponent<AudioSource>().Play();
            Usable = true;
            index++;
        }
        else
        {
            Papers[index].paper.SetActive(false);
            AudioOut.GetComponent<AudioSource>().volume = 0.7f;
            AudioOut.GetComponent<AudioSource>().clip = rightSound;
            AudioOut.GetComponent<AudioSource>().Play();
            SaigonControl.Current.gameState++;
            Buttholder.SetActive(true);
            QuestionText.SetActive(false);
            Usable = false;

            GlowBox.SetActive(false);
        }
    }

    public IEnumerator ReColor(int x, bool cor)
    {
        if (cor)
        {
            Buttons[x].GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        }
        else
        {
            Buttons[x].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        
        
        yield return new WaitForSeconds(0.5f);
        Buttons[x].GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        yield return null;
    }
}
