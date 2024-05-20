using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class ControlSource : MonoBehaviour
{
    public abstract ControllerSourceType ControllerSourceType { get; }

    [SerializeField]
    protected SerializableDictionary<string, SerializableDictionary<string, ControllerAction>> _actions = new();
    public SerializableDictionary<string, SerializableDictionary<string, ControllerAction>> Actions { get => _actions; }

    protected Controller _controller;
    public Controller Controller { get => _controller; set => _controller = value; }

    public abstract void ApplyToController(Controller controller);


    protected UnityEvent<ControllerActionPhase, object> _onActionPressed;
    public UnityEvent<ControllerActionPhase, object> OnActionPressed => _onActionPressed;

    protected UnityEvent<ControllerActionPhase, object> _onActionHeld;
    public UnityEvent<ControllerActionPhase, object> OnActionHeld => _onActionHeld;

    protected UnityEvent<ControllerActionPhase, object> _onActionReleased;
    public UnityEvent<ControllerActionPhase, object> OnActionReleased => _onActionReleased;

    protected ControllerAction _currentControllerAction;

    private void Update()
    {
        UpdateActions();
    }

    private void UpdateActions()
    {
        foreach(var map in Actions.Keys)
        {
            foreach(var action in Actions[map].Keys)
            {
                UpdateAction(map, action);
            }
        }
    }

    protected virtual void UpdateAction(string map, string action)
    {
        _currentControllerAction = Actions[map][action];

        // Step the Controller Action's into their next Phases
        if (_currentControllerAction.Phase == ControllerActionPhase.Pressed)
        {
            _currentControllerAction.Phase = ControllerActionPhase.Held;
        }
        if (_currentControllerAction.Phase == ControllerActionPhase.Released)
        {
            _currentControllerAction.Phase = ControllerActionPhase.Inactive;
        }


        if(_currentControllerAction.Value != null)
        {
            switch (_currentControllerAction.ValueType)
            {
                case ControllerActionValueType.Axis:
                    Controller.OnControllerAxisInput(map, action, (float)_currentControllerAction.Value, _currentControllerAction.Phase);
                    break;
                case ControllerActionValueType.Axis2D:
                    Controller.OnControllerAxis2DInput(map, action, (Vector2)_currentControllerAction.Value, _currentControllerAction.Phase);
                    break;
                case ControllerActionValueType.Button:
                    Controller.OnControllerButtonInput(map, action, (bool)_currentControllerAction.Value, _currentControllerAction.Phase);
                    break;
            }
        }
    }
}
[Serializable]
public class ControllerAction
{
    [SerializeField]
    [ReadOnly]
    private string _map;
    public string Map => _map;

    [SerializeField]
    [ReadOnly]
    private string _name;
    public string Name => _name;

    public object Value;
    [SerializeField]
    [ReadOnly]
    public ControllerActionValueType ValueType;
    [SerializeField]
    [ReadOnly]
    public ControllerActionPhase Phase;
    public ControllerAction(string map, string name, ControllerActionValueType type, ControllerActionPhase phase)
    {
        this._map = map;
        this._name = name;
        this.ValueType = type;
        this.Phase = phase;
    }
}

