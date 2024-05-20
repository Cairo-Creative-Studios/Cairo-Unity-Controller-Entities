using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class DeviceControlSource : ControlSource
{
    public override ControllerSourceType ControllerSourceType { get => ControllerSourceType.Device; }
    private SerializableDictionary<string, SerializableDictionary<string, InputAction>> _inputActions = new();
    protected InputAction _currentInputAction;

    public void AssignActions(InputActionAsset inputActionAsset, string inputMap = "")
    {
        if (inputMap == "")
        {
            foreach (var map in inputActionAsset.actionMaps)
            {
                _actions.Add(map.name, new());
                _inputActions.Add(map.name, new());

                foreach (var action in map.actions)
                {
                    action.Enable();
                    _inputActions[map.name].Add(action.name, action);

                    switch (action.type)
                    {
                        case InputActionType.PassThrough:
                            _actions[map.name].Add(action.name, new(map.name, action.name, ControllerActionValueType.Axis, ControllerActionPhase.Inactive));
                            break;
                        case InputActionType.Value:
                            _actions[map.name].Add(action.name, new(map.name, action.name, ControllerActionValueType.Axis2D, ControllerActionPhase.Inactive));
                            break;
                        case InputActionType.Button:
                            _actions[map.name].Add(action.name, new(map.name, action.name, ControllerActionValueType.Button, ControllerActionPhase.Inactive));
                            break;
                    }
                }
            }
        }
    }


    public override void ApplyToController(Controller controller)
    {
    }

    protected override void UpdateAction(string map, string action)
    {
        _currentControllerAction = Actions[map][action];
        _currentInputAction = _inputActions[map][action];

        if (_currentInputAction.IsPressed())
        {
            if (!(_currentControllerAction.Phase == ControllerActionPhase.Pressed && _currentControllerAction.Phase == ControllerActionPhase.Held))
            {
                _currentControllerAction.Phase = ControllerActionPhase.Pressed;
            }
            _currentControllerAction.Value = _currentInputAction.ReadValueAsObject();
        }
        else
        if (_currentControllerAction.Phase == ControllerActionPhase.Pressed || _currentControllerAction.Phase == ControllerActionPhase.Held)
        {
            _currentControllerAction.Phase = ControllerActionPhase.Released;
        }

        base.UpdateAction(map, action);
    }
}