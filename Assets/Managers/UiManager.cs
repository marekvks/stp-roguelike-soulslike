using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UiManager : MonoBehaviour
{
    [Header("References - Scripts")]
    [SerializeField] private InputHandler _inputHandler;

    [Header("References - Other")]
    [SerializeField] private GameObject Inventory;

    [Header("Values")]
    private bool _isInventoryOpen;

    #region Singleton
    public static UiManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError($"There can be only one instance of {this.name} script!");
    }
    #endregion

    private void Start()
    {
        SubscribeToInput();
        SetValuesAccordingToEditor();
    }

    private void SetValuesAccordingToEditor()
    {
        if (Inventory.activeInHierarchy)
            OpenInventory();
    }

    private void SubscribeToInput()
    {
        _inputHandler.SubscribeToInventory(ManageInventoryVisibility);
    }

    private void ManageInventoryVisibility(InputAction.CallbackContext context)
    {
        if (_isInventoryOpen)
            CloseInventory();
        else
            OpenInventory();
    }

    private void OpenInventory()
    {
        GameManager.Instance.PauseGame();
        Inventory.SetActive(true);
        _isInventoryOpen = true;
    }

    private void CloseInventory()
    {
        _isInventoryOpen = false;
        Inventory.SetActive(false);
        GameManager.Instance.ResumeGame();
    }
}
