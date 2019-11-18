using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerIndicator : MonoBehaviour {
    public static PlayerIndicator Current { get; private set; }
    public GameObject Hud;
    public GameObject Top;
    

    void Awake()
    {
        Current = this;
    }

    // Use this for initialization
    void Start()
    {
        /*
        Hud.transform.parent = gameObject.transform;
        Hud.transform.rotation = transform.rotation;
        Hud.transform.position = new Vector3(0, 0, -0.15f);
        */
    }
	
    /*
	// Update is called once per frame
	void Update () {
		
	}
    */

    public IEnumerator Objective(string x)
    {
        if (Hud.GetComponent<AudioSource>())
        {
            Hud.GetComponent<AudioSource>().Play();
        }
        
        Hud.GetComponent<TextMeshPro>().SetText(x);
        if(Top != null)
        {
            Top.GetComponent<TextMeshPro>().SetText("Objective Updated:");
        }
       
        yield return new WaitForSeconds(8);

        Hud.GetComponent<TextMeshPro>().SetText("");
        Top.GetComponent<TextMeshPro>().SetText("");
        yield return null;
    }

    public void ObjectiveUpdate(string x)
    {
        StartCoroutine(Objective(x));
    }
    public void WipeObjective()
    {
        Hud.GetComponent<TextMeshPro>().SetText("");
        Top.GetComponent<TextMeshPro>().SetText("");
    }


}
