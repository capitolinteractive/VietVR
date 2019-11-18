using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RestartChecker : MonoBehaviour {

    public bool IsRestarted;

    private string filePath;

    public GameObject RestartText;

    // Use this for initialization
    void Start () {
        filePath = Application.persistentDataPath + "/VietVrSettings"+StaticHolder.Current.GameVersion.ToString()+".txt";

        if (!File.Exists(filePath))
        {
            Debug.Log("File is being made " + filePath.ToString());
            File.WriteAllText(filePath, "1");
            Home_Menu_Control.Current.Main.SetActive(false);
            RestartText.SetActive(true);
        }
        else
        {
            Debug.Log("File is exist! Loading! " + filePath.ToString());
            loadFile();
        }



    }

    public void loadFile()
    {
        //int.Parse()
        string ReadInfo = File.ReadAllText(filePath);

        if (ReadInfo == "1")
        {
            IsRestarted = true;
        }
        else
        {
            Home_Menu_Control.Current.Main.SetActive(false);
            RestartText.SetActive(true);
        }
    }


}
