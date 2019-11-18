using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour {
    public static Hint Current { get; private set; }
    bool HintMenu;

    private bool initialSetup;

    public GameObject[] hints;
    public int index;

    private void Awake()
    {
        Current = this;
        gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
       


        initialSetup = true;
        //gameObject.SetActive(false);

    }

    private void OnEnable()
    {
        if(PlayerIndicator.Current != null)
        {
           
            transform.rotation = PlayerIndicator.Current.gameObject.transform.rotation;
            
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            
            
            //transform.rotation = Quaternion.Euler(0, PlayerIndicator.Current.gameObject.transform.rotation.y, 0);
           
            //transform.rotation = Quaternion.Euler(PlayerIndicator.Current.gameObject.transform.rotation.x, PlayerIndicator.Current.gameObject.transform.rotation.y, PlayerIndicator.Current.gameObject.transform.rotation.z);

            if (VRpointer.Current.moveable)
            {
                transform.position = PlayerIndicator.Current.gameObject.transform.position;
            }
            hints[index].SetActive(true);
        }
        else
        {
            StartCoroutine(EnableFailed());
        }
            
    }

    IEnumerator EnableFailed()
    {
        yield return new WaitForSeconds(0.1f);
        //transform.rotation = PlayerIndicator.Current.gameObject.transform.rotation;
        transform.rotation = PlayerIndicator.Current.gameObject.transform.rotation;

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        if (VRpointer.Current.moveable)
        {
            transform.position = PlayerIndicator.Current.gameObject.transform.position;
        }
        hints[index].SetActive(true);
        yield return null;
    }

   
    // Update is called once per frame
   
 
}
