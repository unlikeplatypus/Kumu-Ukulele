﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string _sceneName = "";

    public void ChangeScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
