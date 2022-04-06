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
                },
                {
                    ""name"": ""Attack A"",
                    ""type"": ""Button"",
                    ""id"": ""fdd3df15-0949-42c8-8d28-eb58255b6c08"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack B"",
                    ""type"": ""Button"",
                    ""id"": ""f16d0e71-9eb0-4535-b32d-16fdf19cbc74"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dispacher"",
                    ""type"": ""Button"",
                    ""id"": ""e649180f-3888-4a82-9c1e-8d3d201fcd49"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""990f0b32-9c78-46c0-b7fe-6fc1e2b874b4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
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
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""d275118c-0889-4d28-b772-831a4641956e"",
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
                    ""id"": ""5b54ad32-fffc-4777-8d63-ae91d84b3c2d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DinoPostre"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8b38667d-787e-4a8b-8af5-f5dffa6589ed"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DinoPostre"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a2a6abe2-7c60-4936-b6a4-cff6d052d28b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DinoPostre"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""32bc6933-0fc3-4849-a521-2986c8997316"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DinoPostre"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bb1a03fb-3b82-42b8-a0c5-e9b1dea6891e"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DinoPostre"",
                    ""action"": ""Attack A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f32abeab-042f-4ecc-b1ff-754135527b81"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e6cbf61-17c6-4fa3-ba43-52770fabe2d9"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DinoPostre"",
                    ""action"": ""Dispacher"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""512117dd-4fe4-4511-821e-717d81afc937"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""DinoPostre"",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
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
        m_DinopostreController_AttackA = m_DinopostreController.FindAction("Attack A", throwIfNotFound: true);
        m_DinopostreController_AttackB = m_DinopostreController.FindAction("Attack B", throwIfNotFound: true);
        m_DinopostreController_Dispacher = m_DinopostreController.FindAction("Dispacher", throwIfNotFound: true);
        m_DinopostreController_Interaction = m_DinopostreController.FindAction("Interaction", throwIfNotFound: true);
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
    private readonly InputAction m_DinopostreController_AttackA;
    private readonly InputAction m_DinopostreController_AttackB;
    private readonly InputAction m_DinopostreController_Dispacher;
    private readonly InputAction m_DinopostreController_Interaction;
    public struct DinopostreControllerActions
    {
        private @DinoPostreAction m_Wrapper;
        public DinopostreControllerActions(@DinoPostreAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_DinopostreController_Movement;
        public InputAction @AttackA => m_Wrapper.m_DinopostreController_AttackA;
        public InputAction @AttackB => m_Wrapper.m_DinopostreController_AttackB;
        public InputAction @Dispacher => m_Wrapper.m_DinopostreController_Dispacher;
        public InputAction @Interaction => m_Wrapper.m_DinopostreController_Interaction;
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
                @AttackA.started -= m_Wrapper.m_DinopostreControllerActionsCallbackInterface.OnAttackA;
                @AttackA.performed -= m_Wrapper.m_DinopostreControllerActionsCallbackInterface.OnAttackA;
                @AttackA.canceled -= m_Wrapper.m_DinopostreControllerActionsCallbackInterface.OnAttackA;
                @AttackB.started -= m_Wrapper.m_DinopostreControllerActionsCallbackInterface.OnAttackB;
                @AttackB.performed -= m_Wrapper.m_DinopostreControllerActionsCallbackInterface.OnAttackB;
                @AttackB.canceled -= m_Wrapper.m_DinopostreControllerActionsCallbackInterface.OnAttackB;
                @Dispacher.started -= m_Wrapper.m_DinopostreControllerActionsCallbackInterface.OnDispacher;
                @Dispacher.performed -= m_Wrapper.m_DinopostreControllerActionsCallbackInterface.OnDispacher;
                @Dispacher.canceled -= m_Wrapper.m_DinopostreControllerActionsCallbackInterface.OnDispacher;
                @Interaction.started -= m_Wrapper.m_DinopostreControllerActionsCallbackInterface.OnInteraction;
                @Interaction.performed -= m_Wrapper.m_DinopostreControllerActionsCallbackInterface.OnInteraction;
                @Interaction.canceled -= m_Wrapper.m_DinopostreControllerActionsCallbackInterface.OnInteraction;
            }
            m_Wrapper.m_DinopostreControllerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @AttackA.started += instance.OnAttackA;
                @AttackA.performed += instance.OnAttackA;
                @AttackA.canceled += instance.OnAttackA;
                @AttackB.started += instance.OnAttackB;
                @AttackB.performed += instance.OnAttackB;
                @AttackB.canceled += instance.OnAttackB;
                @Dispacher.started += instance.OnDispacher;
                @Dispacher.performed += instance.OnDispacher;
                @Dispacher.canceled += instance.OnDispacher;
                @Interaction.started += instance.OnInteraction;
                @Interaction.performed += instance.OnInteraction;
                @Interaction.canceled += instance.OnInteraction;
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
        void OnAttackA(InputAction.CallbackContext context);
        void OnAttackB(InputAction.CallbackContext context);
        void OnDispacher(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
    }
}
