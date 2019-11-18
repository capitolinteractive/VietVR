using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialSetup : MonoBehaviour {

    public float loadTime = 10;
    bool loaded;
    public GameObject fadepref;





	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        loadTime -= Time.deltaTime;

        if(loadTime < 0 && !loaded)
        {
            loaded = true;
            StartCoroutine(SceneChange("main_scene"));
        }
	}

    private IEnumerator SceneChange(string x)
    {
        GameObject daFlash;
        daFlash = Instantiate(fadepref, PlayerIndicator.Current.transform.position, PlayerIndicator.Current.transform.rotation);
        daFlash.transform.parent = PlayerIndicator.Current.transform;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(x);
    }

}
