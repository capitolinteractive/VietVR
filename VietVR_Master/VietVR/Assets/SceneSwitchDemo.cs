﻿
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchDemo : MonoBehaviour {

  public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
    }
}