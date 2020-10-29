// GENERATED AUTOMATICALLY FROM 'Assets/Input/Global Actions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GlobalActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GlobalActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Global Actions"",
    ""maps"": [
        {
            ""name"": ""Actions"",
            ""id"": ""622b6b52-0f5b-4fce-8736-bb7b28b05535"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""35619f0c-adc0-410f-9c95-919391e5c901"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reset"",
                    ""type"": ""Button"",
                    ""id"": ""a3fbbad4-74aa-430c-8ab4-4813c94d775d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skip"",
                    ""type"": ""Button"",
                    ""id"": ""d0a16c65-597d-4d14-8fab-0b76bc00b7ec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""575b9c6b-8e30-459e-947c-92e6bd4633e7"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e998881-ff11-4f1c-9bc0-45d3cd990f9d"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reset"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d164d78a-0c97-4b71-9bac-de36b30750e8"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Actions
        m_Actions = asset.FindActionMap("Actions", throwIfNotFound: true);
        m_Actions_Pause = m_Actions.FindAction("Pause", throwIfNotFound: true);
        m_Actions_Reset = m_Actions.FindAction("Reset", throwIfNotFound: true);
        m_Actions_Skip = m_Actions.FindAction("Skip", throwIfNotFound: true);
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

    // Actions
    private readonly InputActionMap m_Actions;
    private IActionsActions m_ActionsActionsCallbackInterface;
    private readonly InputAction m_Actions_Pause;
    private readonly InputAction m_Actions_Reset;
    private readonly InputAction m_Actions_Skip;
    public struct ActionsActions
    {
        private @GlobalActions m_Wrapper;
        public ActionsActions(@GlobalActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_Actions_Pause;
        public InputAction @Reset => m_Wrapper.m_Actions_Reset;
        public InputAction @Skip => m_Wrapper.m_Actions_Skip;
        public InputActionMap Get() { return m_Wrapper.m_Actions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionsActions set) { return set.Get(); }
        public void SetCallbacks(IActionsActions instance)
        {
            if (m_Wrapper.m_ActionsActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnPause;
                @Reset.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnReset;
                @Reset.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnReset;
                @Reset.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnReset;
                @Skip.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSkip;
                @Skip.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSkip;
                @Skip.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnSkip;
            }
            m_Wrapper.m_ActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Reset.started += instance.OnReset;
                @Reset.performed += instance.OnReset;
                @Reset.canceled += instance.OnReset;
                @Skip.started += instance.OnSkip;
                @Skip.performed += instance.OnSkip;
                @Skip.canceled += instance.OnSkip;
            }
        }
    }
    public ActionsActions @Actions => new ActionsActions(this);
    public interface IActionsActions
    {
        void OnPause(InputAction.CallbackContext context);
        void OnReset(InputAction.CallbackContext context);
        void OnSkip(InputAction.CallbackContext context);
    }
}
