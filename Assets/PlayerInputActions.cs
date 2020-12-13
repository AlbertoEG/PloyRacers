// GENERATED AUTOMATICALLY FROM 'Assets/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""PlayerControls"",
            ""id"": ""d80b1f80-d179-495e-a998-1a45318e6800"",
            ""actions"": [
                {
                    ""name"": ""Steer"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8c7541c1-8607-407b-8bbc-61d8c454032e"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Motor"",
                    ""type"": ""PassThrough"",
                    ""id"": ""429fe010-5df0-462a-b99f-afc7d8c364c2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Respawn"",
                    ""type"": ""Button"",
                    ""id"": ""efc74839-112e-425d-ad1c-9932f108a04d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PauseButton"",
                    ""type"": ""Button"",
                    ""id"": ""968b4622-c42d-46b6-97c9-8b52346406d9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""NavigateMenu"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ffd8834d-7833-4536-90f4-18dce8b223d0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CButton"",
                    ""type"": ""Button"",
                    ""id"": ""60cdeea9-971c-47b5-95e5-6667694aa0d1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""829590bf-08c1-4656-886b-bd6d77a4ebaa"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Steer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""AD"",
                    ""id"": ""4e9c3190-8c6b-49f3-b32d-065b9874c48e"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Steer"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""95f45758-c2c8-43ce-9c5c-bd43674e4229"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Steer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a5d4ef9c-807a-4689-8d28-3ee1253df36a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Steer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""816e959a-b31e-46fe-aed8-bfd1cdfb5170"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Motor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WS"",
                    ""id"": ""2ecf199b-cb92-4261-9949-44b6da1e5725"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Motor"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""813b82ef-0407-4a94-9dfd-f4e0bed795a9"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Motor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d0c18124-b7fd-4dff-8735-93b8c1a0f97f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Motor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""MButton"",
                    ""id"": ""c3fb3ce1-1dfd-41ac-800d-bf9ac173f260"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Motor"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3a97025a-0add-4a24-9183-8bbf2a6260ba"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Motor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c12e73c5-733f-43b0-b486-7773ab815ec8"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Motor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""288c97a7-4927-461e-b9f5-0fc300b4f1ed"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Respawn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a71f26a-7134-4884-8cba-3ff235644666"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Respawn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f730b7d-c1da-4b5a-afb2-cd6d0cf93691"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""PauseButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef2d54e1-6ee6-4c1c-9d4e-2111d7d46dbb"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""PauseButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""10359042-15fe-4920-b9f9-c768bd5e663e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""NavigateMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WS"",
                    ""id"": ""33c55175-94d4-4a2a-96fa-577edeefcc85"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NavigateMenu"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d4397e46-7caa-48a4-915c-dd300ae911b9"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""NavigateMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ce68a911-8f46-4af8-ba19-01eed8f45083"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""NavigateMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""60d56b79-0eea-44ed-a1a3-ce380aa2360e"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""CButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerControls
        m_PlayerControls = asset.FindActionMap("PlayerControls", throwIfNotFound: true);
        m_PlayerControls_Steer = m_PlayerControls.FindAction("Steer", throwIfNotFound: true);
        m_PlayerControls_Motor = m_PlayerControls.FindAction("Motor", throwIfNotFound: true);
        m_PlayerControls_Respawn = m_PlayerControls.FindAction("Respawn", throwIfNotFound: true);
        m_PlayerControls_PauseButton = m_PlayerControls.FindAction("PauseButton", throwIfNotFound: true);
        m_PlayerControls_NavigateMenu = m_PlayerControls.FindAction("NavigateMenu", throwIfNotFound: true);
        m_PlayerControls_CButton = m_PlayerControls.FindAction("CButton", throwIfNotFound: true);
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

    // PlayerControls
    private readonly InputActionMap m_PlayerControls;
    private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerControls_Steer;
    private readonly InputAction m_PlayerControls_Motor;
    private readonly InputAction m_PlayerControls_Respawn;
    private readonly InputAction m_PlayerControls_PauseButton;
    private readonly InputAction m_PlayerControls_NavigateMenu;
    private readonly InputAction m_PlayerControls_CButton;
    public struct PlayerControlsActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerControlsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Steer => m_Wrapper.m_PlayerControls_Steer;
        public InputAction @Motor => m_Wrapper.m_PlayerControls_Motor;
        public InputAction @Respawn => m_Wrapper.m_PlayerControls_Respawn;
        public InputAction @PauseButton => m_Wrapper.m_PlayerControls_PauseButton;
        public InputAction @NavigateMenu => m_Wrapper.m_PlayerControls_NavigateMenu;
        public InputAction @CButton => m_Wrapper.m_PlayerControls_CButton;
        public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerControlsActions instance)
        {
            if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
            {
                @Steer.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSteer;
                @Steer.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSteer;
                @Steer.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnSteer;
                @Motor.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMotor;
                @Motor.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMotor;
                @Motor.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMotor;
                @Respawn.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRespawn;
                @Respawn.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRespawn;
                @Respawn.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnRespawn;
                @PauseButton.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPauseButton;
                @PauseButton.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPauseButton;
                @PauseButton.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPauseButton;
                @NavigateMenu.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnNavigateMenu;
                @NavigateMenu.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnNavigateMenu;
                @NavigateMenu.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnNavigateMenu;
                @CButton.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnCButton;
                @CButton.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnCButton;
                @CButton.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnCButton;
            }
            m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Steer.started += instance.OnSteer;
                @Steer.performed += instance.OnSteer;
                @Steer.canceled += instance.OnSteer;
                @Motor.started += instance.OnMotor;
                @Motor.performed += instance.OnMotor;
                @Motor.canceled += instance.OnMotor;
                @Respawn.started += instance.OnRespawn;
                @Respawn.performed += instance.OnRespawn;
                @Respawn.canceled += instance.OnRespawn;
                @PauseButton.started += instance.OnPauseButton;
                @PauseButton.performed += instance.OnPauseButton;
                @PauseButton.canceled += instance.OnPauseButton;
                @NavigateMenu.started += instance.OnNavigateMenu;
                @NavigateMenu.performed += instance.OnNavigateMenu;
                @NavigateMenu.canceled += instance.OnNavigateMenu;
                @CButton.started += instance.OnCButton;
                @CButton.performed += instance.OnCButton;
                @CButton.canceled += instance.OnCButton;
            }
        }
    }
    public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerControlsActions
    {
        void OnSteer(InputAction.CallbackContext context);
        void OnMotor(InputAction.CallbackContext context);
        void OnRespawn(InputAction.CallbackContext context);
        void OnPauseButton(InputAction.CallbackContext context);
        void OnNavigateMenu(InputAction.CallbackContext context);
        void OnCButton(InputAction.CallbackContext context);
    }
}
