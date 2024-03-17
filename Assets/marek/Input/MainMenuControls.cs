//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/marek/Input/MainMenuControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @MainMenuControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MainMenuControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainMenuControls"",
    ""maps"": [
        {
            ""name"": ""MainMenu"",
            ""id"": ""0200de39-1f1a-4927-b6d9-a4d79e6ed4bb"",
            ""actions"": [
                {
                    ""name"": ""RotatingModel"",
                    ""type"": ""Value"",
                    ""id"": ""2d89111c-fd69-4347-86d0-40fa6ca67bfc"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseButtonClicked"",
                    ""type"": ""PassThrough"",
                    ""id"": ""977a2ca5-1a44-4209-82da-7d291157dde8"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""aed48670-7a1f-4561-a31c-57db9a294458"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotatingModel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f7a22b8-fb62-44a8-afc9-660b1b5860b3"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseButtonClicked"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MainMenu
        m_MainMenu = asset.FindActionMap("MainMenu", throwIfNotFound: true);
        m_MainMenu_RotatingModel = m_MainMenu.FindAction("RotatingModel", throwIfNotFound: true);
        m_MainMenu_MouseButtonClicked = m_MainMenu.FindAction("MouseButtonClicked", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // MainMenu
    private readonly InputActionMap m_MainMenu;
    private List<IMainMenuActions> m_MainMenuActionsCallbackInterfaces = new List<IMainMenuActions>();
    private readonly InputAction m_MainMenu_RotatingModel;
    private readonly InputAction m_MainMenu_MouseButtonClicked;
    public struct MainMenuActions
    {
        private @MainMenuControls m_Wrapper;
        public MainMenuActions(@MainMenuControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @RotatingModel => m_Wrapper.m_MainMenu_RotatingModel;
        public InputAction @MouseButtonClicked => m_Wrapper.m_MainMenu_MouseButtonClicked;
        public InputActionMap Get() { return m_Wrapper.m_MainMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainMenuActions set) { return set.Get(); }
        public void AddCallbacks(IMainMenuActions instance)
        {
            if (instance == null || m_Wrapper.m_MainMenuActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MainMenuActionsCallbackInterfaces.Add(instance);
            @RotatingModel.started += instance.OnRotatingModel;
            @RotatingModel.performed += instance.OnRotatingModel;
            @RotatingModel.canceled += instance.OnRotatingModel;
            @MouseButtonClicked.started += instance.OnMouseButtonClicked;
            @MouseButtonClicked.performed += instance.OnMouseButtonClicked;
            @MouseButtonClicked.canceled += instance.OnMouseButtonClicked;
        }

        private void UnregisterCallbacks(IMainMenuActions instance)
        {
            @RotatingModel.started -= instance.OnRotatingModel;
            @RotatingModel.performed -= instance.OnRotatingModel;
            @RotatingModel.canceled -= instance.OnRotatingModel;
            @MouseButtonClicked.started -= instance.OnMouseButtonClicked;
            @MouseButtonClicked.performed -= instance.OnMouseButtonClicked;
            @MouseButtonClicked.canceled -= instance.OnMouseButtonClicked;
        }

        public void RemoveCallbacks(IMainMenuActions instance)
        {
            if (m_Wrapper.m_MainMenuActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMainMenuActions instance)
        {
            foreach (var item in m_Wrapper.m_MainMenuActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MainMenuActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MainMenuActions @MainMenu => new MainMenuActions(this);
    public interface IMainMenuActions
    {
        void OnRotatingModel(InputAction.CallbackContext context);
        void OnMouseButtonClicked(InputAction.CallbackContext context);
    }
}
