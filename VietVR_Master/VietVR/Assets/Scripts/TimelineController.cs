using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TimelineController : MonoBehaviour {

    public GameObject[] Timeline;
    public int i;
    int tempi;

    public static TimelineController Current { get; private set; }

    public Component[] panel;

    public GameObject timelineBut1;
    public GameObject timelineBut2;
    public GameObject timelineBut3;

    public GameObject midVietBut;
    public GameObject mid1968But;

    //public GameObject SelQuad;

    public GameObject SelectionQuad;
    public GameObject VietSelectionQuad;
    public GameObject NinteenSelectionQuad;

    //public Material TLB1reg;
    //public Material TLB1selc;

    private ButtonReq TL1Req = null;
    private ButtonReq TL2Req = null;
    private ButtonReq TL3Req = null;

    private ButtonReq mVietReq = null;
    private ButtonReq m19Req = null;

    public bool backMenu = false;

    public GameObject menuBut;
    private ButtonReq menuReq;

    public GameObject helpBut;
    private ButtonReq helpReq;

    // Use this for initialization
    void Start () {
        Current = this;
        i = 1;
        tempi = 5;

        TL1Req = timelineBut1.GetComponent<ButtonReq>();
        if (TL1Req != null)
        {
            TL1Req.Activated += this.TL1;
        }

        TL2Req = timelineBut2.GetComponent<ButtonReq>();
        if (TL2Req != null)
        {
            TL2Req.Activated += this.TL2;
        }

        TL3Req = timelineBut3.GetComponent<ButtonReq>();
        if (TL3Req != null)
        {
            TL3Req.Activated += this.TL3;
        }

        mVietReq = midVietBut.GetComponent<ButtonReq>();
        if (mVietReq != null)
        {
            mVietReq.Activated += this.TL1;
        }

        m19Req = mid1968But.GetComponent<ButtonReq>();
        if (m19Req != null)
        {
            m19Req.Activated += this.TL2;
        }

        menuReq = menuBut.GetComponent<ButtonReq>();
        if (menuReq != null)
        {
            menuReq.Activated += this.Menu;
        }

        helpReq = helpBut.GetComponent<ButtonReq>();
        if (helpReq != null)
        {
            helpReq.Activated += this.Help;
        }

        if (StaticHolder.Current != null)
        {
            if (StaticHolder.Current.TimelineState == 0)
            {
                TL3();
            }
            if (StaticHolder.Current.TimelineState == 1)
            {
                TL1();
            }
            else if (StaticHolder.Current.TimelineState == 2)
            {
                TL2();
            }
        }

        Hint.Current.index = 10;
        Hint.Current.gameObject.SetActive(true);

    }
	
	// Update is called once per frame
	//void Update () {
		
	//}

        
    public void Selection()
    {
        //print("Selection function");
        if(tempi != i)
        {
            panel = Timeline[i].gameObject.GetComponentsInChildren<TimelinePanelButton>();
            tempi = i;
        }
        
        
        foreach (TimelinePanelButton timelinePanelButton in panel)
        {
            if (timelinePanelButton.gameObject.activeSelf && timelinePanelButton.open)
            {
                timelinePanelButton.Close();
                timelinePanelButton.opened = false;
                timelinePanelButton.GetComponentInParent<Transform>().transform.localPosition = timelinePanelButton.OriginPos;
                
            }
            //timelinePanelButton.diagBox.SetActive(false);
            //timelinePanelButton.open = false;
        }
        

        //GetComponentsInChildren<TimelinePanelButton>().open = false;
    }
    

    public void TL1() //Vietnam
    {
        if (i != 0)
        {
            //THE OTHER WAY
            //Component[] panButs;

            VietSelectionQuad.SetActive(true);
            NinteenSelectionQuad.SetActive(false);
            SelectionQuad.SetActive(false);

            //THE OTHER WAY
            /*
            panButs = Timeline[i].gameObject.GetComponentsInChildren<TimelinePanelButton>();
            foreach(TimelinePanelButton panel in panButs)
            {
                if (panel.gameObject.activeSelf && panel.open)
                {
                    panel.Close();
                    panel.opened = false;
                    panel.GetComponentInParent<Transform>().transform.localPosition = panel.OriginPos;
                }



                //THIS WILL FIX FLICKER BUT WILL COST PERFORMANCE ON CLICK
                //panel.opened = false;
                //panel.GetComponentInParent<Transform>().transform.localPosition = panel.OriginPos;
            }
            
            */

            if (tempi != i)
            {
                panel = Timeline[i].gameObject.GetComponentsInChildren<TimelinePanelButton>();
                tempi = i;
            }

            foreach (TimelinePanelButton timelinePanelButton in panel)
            {
                if (timelinePanelButton.gameObject.activeSelf && timelinePanelButton.open)
                {
                    timelinePanelButton.Close();
                    timelinePanelButton.opened = false;
                    timelinePanelButton.GetComponentInParent<Transform>().transform.localPosition = timelinePanelButton.OriginPos;
                    
                }
            }



            Timeline[i].SetActive(false);
            i = 0;
            Timeline[i].SetActive(true);
        }
        
    }
    public void TL2() //1968
    {
        if (i != 1)
        {
            //Component[] panButs;

            VietSelectionQuad.SetActive(false);
            NinteenSelectionQuad.SetActive(true);
            SelectionQuad.SetActive(false);

            /*
            panButs = Timeline[i].gameObject.GetComponentsInChildren<TimelinePanelButton>();
            foreach (TimelinePanelButton panel in panButs)
            {
                if (panel.gameObject.activeSelf && panel.open)
                {
                    panel.Close();
                    panel.opened = false;
                    panel.GetComponentInParent<Transform>().transform.localPosition = panel.OriginPos;
                }

            }
            */

            if (tempi != i)
            {
                panel = Timeline[i].gameObject.GetComponentsInChildren<TimelinePanelButton>();
                tempi = i;
            }

            foreach (TimelinePanelButton timelinePanelButton in panel)
            {
                if (timelinePanelButton.gameObject.activeSelf && timelinePanelButton.open)
                {
                    timelinePanelButton.Close();
                    timelinePanelButton.opened = false;
                    timelinePanelButton.GetComponentInParent<Transform>().transform.localPosition = timelinePanelButton.OriginPos;
                    
                }
            }

            Timeline[i].SetActive(false);
            i = 1;
            Timeline[i].SetActive(true);
        }
    }
    public void TL3() // Coldwar
    {
        if (i != 2)
        {
            //Component[] panButs;

            VietSelectionQuad.SetActive(false);
            NinteenSelectionQuad.SetActive(false);
            SelectionQuad.SetActive(true);

            /*
            panButs = Timeline[i].gameObject.GetComponentsInChildren<TimelinePanelButton>();
            foreach (TimelinePanelButton panel in panButs)
            {
                if (panel.gameObject.activeSelf && panel.open)
                {
                    panel.Close();
                    panel.opened = false;
                    panel.GetComponentInParent<Transform>().transform.localPosition = panel.OriginPos;
                }
                
            }
            */

            if (tempi != i)
            {
                panel = Timeline[i].gameObject.GetComponentsInChildren<TimelinePanelButton>();
                tempi = i;
            }

            foreach (TimelinePanelButton timelinePanelButton in panel)
            {
                if (timelinePanelButton.gameObject.activeSelf && timelinePanelButton.open)
                {
                    timelinePanelButton.Close();
                    timelinePanelButton.opened = false;
                    timelinePanelButton.GetComponentInParent<Transform>().transform.localPosition = timelinePanelButton.OriginPos;
                    
                }
            }


            Timeline[i].SetActive(false);
            i = 2;
            Timeline[i].SetActive(true);
        }
    }

    /*
    public void Next()
    {
        GetComponentInChildren<TimelinePanelButton>().open = false;
        GetComponentInChildren<TimelinePanelButton>().diagBox.SetActive(false);
        Timeline[i].SetActive(false);
        i++;
        if (i > 2)
        {
            i = 0;
        }
        Timeline[i].SetActive(true);
    }

    public void Prev()
    {
        GetComponentInChildren<TimelinePanelButton>().open = false;
        GetComponentInChildren<TimelinePanelButton>().diagBox.SetActive(false);
        Timeline[i].SetActive(false);
        i--;
        if (i < 0)
        {
            i = 2;
        }
        Timeline[i].SetActive(true);
    }
    */

    public void Menu()
    {
        if(backMenu)
        {
            BackMenu.Current.Close();
            backMenu = false;
            
        }
        else if (!backMenu)
        {
            backMenu = true;
            //BackMenu.Current.Open();
            BackMenu.Current.gameObject.SetActive(true);
        }
    }

    public void Help()
    {
        if (!Hint.Current.gameObject.activeSelf)
        {
            Hint.Current.index = 10;
            Hint.Current.gameObject.SetActive(true);
        }
       
    }


}
