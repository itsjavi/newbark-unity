// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/NewBark/Input/InputActionsMaster.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace NewBark.Input
{
    public class InputActionsMaster : IInputActionCollection
    {
        private InputActionAsset asset;
        public InputActionsMaster()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActionsMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""5bf60cd3-4646-4d62-afe8-61fac49f773c"",
            ""actions"": [
                {
                    ""name"": ""ButtonA"",
                    ""type"": ""Value"",
                    ""id"": ""80033cf8-abff-4a66-b536-2615fefd00fb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonB"",
                    ""type"": ""Value"",
                    ""id"": ""e49d9410-0957-4001-a00e-2eae1453a655"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonDirectional"",
                    ""type"": ""Button"",
                    ""id"": ""9df6fe33-0459-415e-ad33-d948dd83c772"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonSelect"",
                    ""type"": ""Value"",
                    ""id"": ""af96df5f-0690-42c3-a0e7-478c3dddc598"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ButtonStart"",
                    ""type"": ""Value"",
                    ""id"": ""488c0f70-604a-458f-bb0e-77c73089870b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7349bad4-c832-45d0-8c28-b13e68689b02"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard CS"",
                    ""action"": ""ButtonA"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ed2c6fc-bf68-4ce2-9d54-a49dcf99336b"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad CS"",
                    ""action"": ""ButtonA"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3bd7194-ae75-4014-99c9-dc6ac6426e70"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad CS"",
                    ""action"": ""ButtonB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a163e87-a010-4421-9bd8-5b07cc9706d1"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard CS"",
                    ""action"": ""ButtonB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Dpad"",
                    ""id"": ""81003661-89c6-4e9b-a91a-5a5c184de66b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad CS"",
                    ""action"": ""ButtonDirectional"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""629d8e7c-a1f3-47fe-b652-dbae51baeda8"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad CS"",
                    ""action"": ""ButtonDirectional"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""50ae0167-0db0-4f9a-96dd-1b1fa01b7825"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad CS"",
                    ""action"": ""ButtonDirectional"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""d35f274b-3ca5-4594-a9c9-f4d2cfd4c1b4"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad CS"",
                    ""action"": ""ButtonDirectional"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""a4f25778-4d24-4a5c-8363-c0327e2f425b"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad CS"",
                    ""action"": ""ButtonDirectional"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""30972969-7eeb-44b2-b78f-652afa542824"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ButtonDirectional"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""842830f0-d5a0-4c34-afc4-5e4c59bf4799"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard CS"",
                    ""action"": ""ButtonDirectional"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ffc888b9-2b54-4bc6-9d27-6499a3082462"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard CS"",
                    ""action"": ""ButtonDirectional"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""18479475-548c-4a49-80ff-b0e3d7205621"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard CS"",
                    ""action"": ""ButtonDirectional"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fe70dd07-e9fc-462e-8d66-9dd506216672"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard CS"",
                    ""action"": ""ButtonDirectional"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7d119c59-0b31-4867-9577-e5e9323f728a"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad CS"",
                    ""action"": ""ButtonSelect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c60af12a-77fa-450b-968f-ac649e2458ef"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard CS"",
                    ""action"": ""ButtonSelect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9ce68ce-7a4f-407f-8a76-b8dcb032afa6"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad CS"",
                    ""action"": ""ButtonStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0106cfe0-5de5-4c9e-b04c-dbb449214e1f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard CS"",
                    ""action"": ""ButtonStart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard CS"",
            ""bindingGroup"": ""Keyboard CS"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad CS"",
            ""bindingGroup"": ""Gamepad CS"",
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
            // Player
            m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
            m_Player_ButtonA = m_Player.FindAction("ButtonA", throwIfNotFound: true);
            m_Player_ButtonB = m_Player.FindAction("ButtonB", throwIfNotFound: true);
            m_Player_ButtonDirectional = m_Player.FindAction("ButtonDirectional", throwIfNotFound: true);
            m_Player_ButtonSelect = m_Player.FindAction("ButtonSelect", throwIfNotFound: true);
            m_Player_ButtonStart = m_Player.FindAction("ButtonStart", throwIfNotFound: true);
        }

        ~InputActionsMaster()
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
        private readonly InputAction m_Player_ButtonA;
        private readonly InputAction m_Player_ButtonB;
        private readonly InputAction m_Player_ButtonDirectional;
        private readonly InputAction m_Player_ButtonSelect;
        private readonly InputAction m_Player_ButtonStart;
        public struct PlayerActions
        {
            private InputActionsMaster m_Wrapper;
            public PlayerActions(InputActionsMaster wrapper) { m_Wrapper = wrapper; }
            public InputAction @ButtonA => m_Wrapper.m_Player_ButtonA;
            public InputAction @ButtonB => m_Wrapper.m_Player_ButtonB;
            public InputAction @ButtonDirectional => m_Wrapper.m_Player_ButtonDirectional;
            public InputAction @ButtonSelect => m_Wrapper.m_Player_ButtonSelect;
            public InputAction @ButtonStart => m_Wrapper.m_Player_ButtonStart;
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    ButtonA.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButtonA;
                    ButtonA.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButtonA;
                    ButtonA.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButtonA;
                    ButtonB.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButtonB;
                    ButtonB.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButtonB;
                    ButtonB.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButtonB;
                    ButtonDirectional.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButtonDirectional;
                    ButtonDirectional.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButtonDirectional;
                    ButtonDirectional.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButtonDirectional;
                    ButtonSelect.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButtonSelect;
                    ButtonSelect.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButtonSelect;
                    ButtonSelect.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButtonSelect;
                    ButtonStart.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButtonStart;
                    ButtonStart.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButtonStart;
                    ButtonStart.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnButtonStart;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    ButtonA.started += instance.OnButtonA;
                    ButtonA.performed += instance.OnButtonA;
                    ButtonA.canceled += instance.OnButtonA;
                    ButtonB.started += instance.OnButtonB;
                    ButtonB.performed += instance.OnButtonB;
                    ButtonB.canceled += instance.OnButtonB;
                    ButtonDirectional.started += instance.OnButtonDirectional;
                    ButtonDirectional.performed += instance.OnButtonDirectional;
                    ButtonDirectional.canceled += instance.OnButtonDirectional;
                    ButtonSelect.started += instance.OnButtonSelect;
                    ButtonSelect.performed += instance.OnButtonSelect;
                    ButtonSelect.canceled += instance.OnButtonSelect;
                    ButtonStart.started += instance.OnButtonStart;
                    ButtonStart.performed += instance.OnButtonStart;
                    ButtonStart.canceled += instance.OnButtonStart;
                }
            }
        }
        public PlayerActions @Player => new PlayerActions(this);
        private int m_KeyboardCSSchemeIndex = -1;
        public InputControlScheme KeyboardCSScheme
        {
            get
            {
                if (m_KeyboardCSSchemeIndex == -1) m_KeyboardCSSchemeIndex = asset.FindControlSchemeIndex("Keyboard CS");
                return asset.controlSchemes[m_KeyboardCSSchemeIndex];
            }
        }
        private int m_GamepadCSSchemeIndex = -1;
        public InputControlScheme GamepadCSScheme
        {
            get
            {
                if (m_GamepadCSSchemeIndex == -1) m_GamepadCSSchemeIndex = asset.FindControlSchemeIndex("Gamepad CS");
                return asset.controlSchemes[m_GamepadCSSchemeIndex];
            }
        }
        public interface IPlayerActions
        {
            void OnButtonA(InputAction.CallbackContext context);
            void OnButtonB(InputAction.CallbackContext context);
            void OnButtonDirectional(InputAction.CallbackContext context);
            void OnButtonSelect(InputAction.CallbackContext context);
            void OnButtonStart(InputAction.CallbackContext context);
        }
    }
}
