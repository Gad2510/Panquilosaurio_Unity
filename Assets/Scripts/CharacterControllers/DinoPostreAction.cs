// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/CharacterControllers/DinoPostreAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DinoPostreAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DinoPostreAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DinoPostreAction"",
    ""maps"": [
        {
            ""name"": ""DinopostreController"",
            ""id"": ""f28d2638-dd03-4c91-a671-5fd5fa2e61bd"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""61bd6dac-9344-47bd-b767-cbe7a993493e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""StickDeadzone"",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1438558a-4967-4af1-b2cc-7fdd15b98fb8"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": ""Press"",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": ""DinoPostre"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Move-Keys"",
                    ""id"": ""1891d9c8-ad67-4a9b-b81b-c31c7b5ccf0d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3d1dcd59-b815-49ec-9516-9430807bd31f"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DinoPostre"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f25707c4-c885-4551-abb5-cf65e1c025ca"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DinoPostre"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f6d75839-f865-4cb6-8687-ac636c26a09f"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DinoPostre"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3684f8f5-28a9-41eb-a082-2bd76d72dcae"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DinoPostre"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""DinoPostre"",
            ""bindingGroup"": ""DinoPostre"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // DinopostreController
        m_DinopostreController = asset.FindActionMap("DinopostreController", throwIfNotFound: true);
        m_DinopostreController_Movement = m_DinopostreController.FindAction("Movement", throwIfNotFound: true);
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

    // DinopostreController
    private readonly InputActionMap m_DinopostreController;
    private IDinopostreControllerActions m_DinopostreControllerActionsCallbackInterface;
    private readonly InputAction m_DinopostreController_Movement;
    public struct DinopostreControllerActions
    {
        private @DinoPostreAction m_Wrapper;
        public DinopostreControllerActions(@DinoPostreAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_DinopostreController_Movement;
        public InputActionMap Get() { return m_Wrapper.m_DinopostreController; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DinopostreControllerActions set) { return set.Get(); }
        public void SetCallbacks(IDinopostreControllerActions instance)
        {
            if (m_Wrapper.m_DinopostreControllerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_DinopostreControllerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_DinopostreControllerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_DinopostreControllerActionsCallbackInterface.OnMovement;
            }
            m_Wrapper.m_DinopostreControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
            }
        }
    }
    public DinopostreControllerActions @DinopostreController => new DinopostreControllerActions(this);
    private int m_DinoPostreSchemeIndex = -1;
    public InputControlScheme DinoPostreScheme
    {
        get
        {
            if (m_DinoPostreSchemeIndex == -1) m_DinoPostreSchemeIndex = asset.FindControlSchemeIndex("DinoPostre");
            return asset.controlSchemes[m_DinoPostreSchemeIndex];
        }
    }
    public interface IDinopostreControllerActions
    {
        void OnMovement(InputAction.CallbackContext context);
    }
}
