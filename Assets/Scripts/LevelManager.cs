﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public string nextScene;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
