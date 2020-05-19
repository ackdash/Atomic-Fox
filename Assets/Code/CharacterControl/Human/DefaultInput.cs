// GENERATED AUTOMATICALLY FROM 'Assets/Code/CharacterControl/Human/DefaultInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @DefaultInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @DefaultInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DefaultInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""fa426c17-ffa3-4a5f-99dd-677ecdbecbce"",
            ""actions"": [
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""92b025a2-32f1-4c80-97c5-a16ec6d48b9c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""f53b0c23-5efc-4101-b651-c1ad51b6efc4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""a6c9dbb9-aa4f-4a4e-a5ab-353ab02d7051"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DecreaseSpeed"",
                    ""type"": ""Button"",
                    ""id"": ""e5add7eb-751c-4ddb-bcae-567065fca0f7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""IncreaseSpeed"",
                    ""type"": ""Button"",
                    ""id"": ""a1693fbb-312f-41a2-a907-8423878450ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""c25f209f-6f2d-47a0-8f9d-20c7fe8a326b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9da64297-c7b7-459c-b680-91553cb1a8a6"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a3b77166-9392-4153-9102-894928aded5c"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""02b6780d-8a0f-4164-91a5-208fb31b610e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""651f4794-d2f0-4370-9010-af69ac09e220"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DecreaseSpeed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f07841b0-9415-4c88-8f6b-97db491b034f"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""IncreaseSpeed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4174b4d6-9a31-4b41-b262-325709f46e4e"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Test2"",
            ""id"": ""9b5c7ec2-200a-4cd4-96bb-f29df79ed458"",
            ""actions"": [
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""816823b6-b8fc-4e06-b799-38fdd96c86d8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""93742e64-1f94-48a6-abda-f0473f49fe9d"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Left = m_Player.FindAction("Left", throwIfNotFound: true);
        m_Player_Right = m_Player.FindAction("Right", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_DecreaseSpeed = m_Player.FindAction("DecreaseSpeed", throwIfNotFound: true);
        m_Player_IncreaseSpeed = m_Player.FindAction("IncreaseSpeed", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        // Test2
        m_Test2 = asset.FindActionMap("Test2", throwIfNotFound: true);
        m_Test2_Left = m_Test2.FindAction("Left", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Left;
    private readonly InputAction m_Player_Right;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_DecreaseSpeed;
    private readonly InputAction m_Player_IncreaseSpeed;
    private readonly InputAction m_Player_Attack;
    public struct PlayerActions
    {
        private @DefaultInput m_Wrapper;
        public PlayerActions(@DefaultInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Left => m_Wrapper.m_Player_Left;
        public InputAction @Right => m_Wrapper.m_Player_Right;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @DecreaseSpeed => m_Wrapper.m_Player_DecreaseSpeed;
        public InputAction @IncreaseSpeed => m_Wrapper.m_Player_IncreaseSpeed;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Left.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRight;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @DecreaseSpeed.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDecreaseSpeed;
                @DecreaseSpeed.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDecreaseSpeed;
                @DecreaseSpeed.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDecreaseSpeed;
                @IncreaseSpeed.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnIncreaseSpeed;
                @IncreaseSpeed.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnIncreaseSpeed;
                @IncreaseSpeed.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnIncreaseSpeed;
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @DecreaseSpeed.started += instance.OnDecreaseSpeed;
                @DecreaseSpeed.performed += instance.OnDecreaseSpeed;
                @DecreaseSpeed.canceled += instance.OnDecreaseSpeed;
                @IncreaseSpeed.started += instance.OnIncreaseSpeed;
                @IncreaseSpeed.performed += instance.OnIncreaseSpeed;
                @IncreaseSpeed.canceled += instance.OnIncreaseSpeed;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Test2
    private readonly InputActionMap m_Test2;
    private ITest2Actions m_Test2ActionsCallbackInterface;
    private readonly InputAction m_Test2_Left;
    public struct Test2Actions
    {
        private @DefaultInput m_Wrapper;
        public Test2Actions(@DefaultInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Left => m_Wrapper.m_Test2_Left;
        public InputActionMap Get() { return m_Wrapper.m_Test2; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Test2Actions set) { return set.Get(); }
        public void SetCallbacks(ITest2Actions instance)
        {
            if (m_Wrapper.m_Test2ActionsCallbackInterface != null)
            {
                @Left.started -= m_Wrapper.m_Test2ActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_Test2ActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_Test2ActionsCallbackInterface.OnLeft;
            }
            m_Wrapper.m_Test2ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
            }
        }
    }
    public Test2Actions @Test2 => new Test2Actions(this);
    public interface IPlayerActions
    {
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDecreaseSpeed(InputAction.CallbackContext context);
        void OnIncreaseSpeed(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
    }
    public interface ITest2Actions
    {
        void OnLeft(InputAction.CallbackContext context);
    }
}
