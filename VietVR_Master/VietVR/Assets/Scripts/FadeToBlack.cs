using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToBlack : MonoBehaviour {
    public bool FadeFromBlack;
    bool FadeSuccess;
    bool flashed;

    Color myCol;
    public GameObject board;

    public string colorType;

    public GameObject camParent;
    // Use this for initialization
    void Start()
    {
        //particleSystem = GetComponent<particles>
        myCol = gameObject.GetComponent<Renderer>().material.GetColor("_TintColor");

        /*
        if (gameObject.GetComponent<Renderer>().material.GetColor("_TintColor") != null)
        {
            myCol = gameObject.GetComponent<Renderer>().material.GetColor("_TintColor");
        }
        if (gameObject.GetComponent<Renderer>().material.GetColor("_Color") != null)
        {
            myCol = gameObject.GetComponent<Renderer>().material.GetColor("_Colorr");
        }
        */

        //Destroy(gameObject.transform.parent.gameObject, 3f);
        //Destroy(gameObject, 3f);

        //myCol.a = 0;


        if (FadeFromBlack)
        {
            //myCol = gameObject.GetComponent<Renderer>().material.GetColor("_Color");
            if (PlayerIndicator.Current != null)
            {
                /*
                transform.parent.gameObject.transform.position = PlayerIndicator.Current.transform.position;
                transform.parent.gameObject.transform.rotation = PlayerIndicator.Current.transform.rotation;
                transform.parent = PlayerIndicator.Current.transform;
                */
                FadeSuccess = true;
            }

            
           

            myCol.a = 1;
            //Destroy(gameObject.transform.parent.gameObject, 5f);
            Destroy(camParent, 5f);

            if (board != null)
            {
                board.SetActive(false);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {



        if (!FadeFromBlack)
        {
            if (Home_control.Current != null)
            {
                if (Home_control.Current.FirstTime == false)
                {
                    board.SetActive(false);
                }
            }
            /*
            if (SaigonControl.Current != null)
            {
                if (!SaigonControl.Current.FirstTime)
                {
                    if (board != null)
                    {
                        board.SetActive(false);
                    }
                }

            }
            */

            /*
            if (SaigonControl.Current != null)
            {
                if (SaigonControl.Current.FirstTime)
                {
                    if (board != null)
                    {
                        board.SetActive(true);
                    }
                }

            }
            else if (LzControl.Current != null)
            {
                if (board != null)
                {
                    board.SetActive(true);
                }
            }
            */

            myCol.a += 0.4f * Time.deltaTime;
            GetComponent<Renderer>().material.SetColor("_TintColor", myCol);
        }
        else if (FadeFromBlack)
        {
            myCol.a -= 0.25f * Time.deltaTime;
            GetComponent<Renderer>().material.SetColor("_TintColor", myCol);

            if (!FadeSuccess)
            {
               

                if (PlayerIndicator.Current != null)
                {
                    /*
                    transform.parent.gameObject.transform.position = PlayerIndicator.Current.transform.position;
                    transform.parent.gameObject.transform.rotation = PlayerIndicator.Current.transform.rotation;
                    transform.parent = PlayerIndicator.Current.transform;
                    */
                    FadeSuccess = true;
                }
            }

            
        }
    }
}
