using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError($"There can be only one instance of {this.name} script!");
    }

    public void PauseGame() => Time.timeScale = 0f;
    public void ResumeGame() => Time.timeScale = 1f;
}
