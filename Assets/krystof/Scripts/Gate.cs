using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour, IInteractable
{
    [SerializeField] private int _sceneToTravelIndex;
    [SerializeField] private string _sceneName;
    [SerializeField] private TextMeshPro _text;
    public static List<int> openedSceneIndex = new List<int>(){0, 1};

    private void Awake()
    {
        _text.text = _sceneName;
    }

    public void Interact()
    {
        if(_sceneToTravelIndex == 0)
        {
            if (!openedSceneIndex.Contains(SceneManager.GetActiveScene().buildIndex + 1))
            {
                openedSceneIndex.Add(SceneManager.GetActiveScene().buildIndex + 1);   
            }
            SceneManager.LoadScene(_sceneToTravelIndex);   
            
        }
        else
        {
            foreach (var VARIABLE in openedSceneIndex)
            {
                if (VARIABLE == _sceneToTravelIndex)
                {
                    SceneManager.LoadScene(_sceneToTravelIndex); 
                    break;
                }
            }   
        }
    }
}
