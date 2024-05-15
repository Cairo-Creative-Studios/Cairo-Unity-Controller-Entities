using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceControlSource : ControlSource
{
    public override ControllerSourceType ControllerSourceType { get => ControllerSourceType.Device; }

    private SerializableDictionary<string, SerializableDictionary<string, ControllerActionValueType>> _actions = new();
    public override SerializableDictionary<string, SerializableDictionary<string, ControllerActionValueType>> Actions { get => _actions; }

    private Controller _controller;
    public override Controller Controller { get => _controller; set => _controller = value; }

    public DeviceControlSource(InputActionAsset inputActionAsset, string inputMap = "")
    {
        if(inputMap == "")
        {
            foreach (var map in inputActionAsset.actionMaps)
            {
                _actions.Add(map.name, new());
                foreach (var action in map.actions)
                {
                    action.Enable();

                    switch(action.ReadValueAsObject())
                    {
                        case float _:
                            _actions[map.name].Add(action.name, ControllerActionValueType.Axis);
                            break;
                        case Vector2 _:
                            _actions[map.name].Add(action.name, ControllerActionValueType.Axis2D);
                            break;
                        case bool _:
                            _actions[map.name].Add(action.name, ControllerActionValueType.Button);
                            break;
                    }
                }
            }
        }
    }

    public void OnInputAction(InputAction.CallbackContext callbackContext)
    {
        if(Controller != null)
        {
            switch (callbackContext.ReadValueAsObject())
            {
                case float _:
                    Controller.OnControllerAxisInput(callbackContext.action.actionMap.name, callbackContext.action.name, callbackContext.ReadValue<float>(), callbackContext.phase);
                    break;
                case Vector2 _:
                    Controller.OnControllerAxis2DInput(callbackContext.action.actionMap.name, callbackContext.action.name, callbackContext.ReadValue<Vector2>(), callbackContext.phase);
                    break;
                case bool _:
                    Controller.OnControllerButtonInput(callbackContext.action.actionMap.name, callbackContext.action.name, callbackContext.ReadValue<bool>(), callbackContext.phase);
                    break;
            }
        }
    }
}