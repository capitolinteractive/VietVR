using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HintButton : MonoBehaviour
{

    public int gameStateCheck;

    public bool highlighted;
    public bool pressed;

    public GameObject nextPanel;

    private ButtonReq butReq = null;
    Vector3 initialScale;

    // Use this for initialization
    void Start()
    {
        butReq = GetComponent<ButtonReq>();
        if (butReq != null)
        {
            butReq.Activated += this.ButtonClick;
        }

        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        highlighted = GetComponent<ButtonReq>().highlighted;
        pressed = GetComponent<ButtonReq>().pressed;

        if (highlighted && !pressed)
        {
            transform.localScale = initialScale * 1.3f;
        }
        else if (!highlighted && !pressed)
        {
            transform.localScale = initialScale;
        }
        else if (pressed)
        {
            transform.localScale = initialScale * 1.3f;
        }

    }

    void OnDestroy()
    {
        if (butReq != null)
        {
            butReq.Activated -= this.ButtonClick;
            butReq = null;
        }
    }

    void ButtonClick()
    {
        if (nextPanel != null)
        {
            nextPanel.SetActive(true);
            gameObject.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            Hint.Current.gameObject.SetActive(false);

            if (Home_control.Current != null)
            {
                if(gameStateCheck == Home_control.Current.gameState)
                {
                    Home_control.Current.gameState++;
                    
                    
                    // gameObject.transform.parent.gameObject.SetActive(false);
                    //gameObject.transform.parent.gameObject.transform.parent.gameObject.SetActive(false);
                }
            }
            else if (OfficeControl.Current != null)
            {
                if (gameStateCheck == OfficeControl.Current.gameState)
                {
                    OfficeControl.Current.gameState++;
                }
            }
           else if (LzControl.Current != null)
            {
                if (gameStateCheck == LzControl.Current.gameState)
                {
                    LzControl.Current.gameState++;
                }
            }
            else if (SaigonControl.Current != null)
            {
                if (gameStateCheck == SaigonControl.Current.gameState)
                {
                    SaigonControl.Current.gameState++;
                }
            }
        }
    }

}
