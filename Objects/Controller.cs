using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : Singleton<Controller>
{
    public ControlSource _inputSource;
    public ControlSource InputSource
    {
        get
        {
            return _inputSource;
        }
        set
        {
            _inputSource = value;
            _inputSource.ApplyToController(this);
        }
    }
    public string Map = "";

    private static List<Controller> _controllers;
    public static List<Controller> Controllers => _controllers ??= new();

    private List<Controllable> _possessedControllables = new();
    public List<Controllable> PossessedControllables => _possessedControllables;

    private static Dictionary<string, GameObject> _cachedControllerPrefabs;
    public static Dictionary<string, GameObject> ControllerPrefabs
    {
        get
        {
            if(_cachedControllerPrefabs == null)
            {
                _cachedControllerPrefabs = new();
                var prefabs = ExtendedResources.LoadAllByComponent<Controller>("");
                foreach (var prefab in prefabs)
                    _cachedControllerPrefabs.Add(prefab.name, prefab);
            }
            return _cachedControllerPrefabs;
        }
    }

    void Awake()
    {
        if (!Controllers.Contains(this))
            Controllers.Add(this);
    }

    public virtual void Possess(Controllable controllable)
    {
        if (controllable.Controller == null)
        {
            _possessedControllables.Add(controllable);
            controllable.Possess(this);
            OnControllerPossess.Invoke(this, controllable);
        }

    }

    public virtual void Unpossess(Controllable controllable)
    {
        if (controllable.Controller == null)
        {
            _possessedControllables.Add(controllable);
            controllable.Unpossess(this);
            OnControllerUnpossess.Invoke(this, controllable);
        }

    }

    /// <summary>
    /// Called by an Input Control Source, passing an Axis Value to the Controllables that the Controller has possessed.
    /// </summary>
    /// <param name="value"></param>
    public virtual void OnControllerAxisInput(string map, string actionName, float value, InputActionPhase phase = InputActionPhase.Performed)
    {
        if (this.Map == "" || this.Map == map)
        {
            OnControllerAxisAction.Invoke(this, actionName, phase, value);

            foreach(var controllable in PossessedControllables)
            {
                if (controllable.Map == "" || controllable.Map == map)
                {
                    controllable.OnControllerAxisInput(actionName, value, phase);

                    OnControllableAxisAction.Invoke(controllable, actionName, phase, value);
                }
            }
        }
    }

    /// <summary>
    /// Called by an Input Control Source, passing an Axis 2D Value to the Controllables that the Controller has possessed.
    /// </summary>
    /// <param name="value"></param>
    public virtual void OnControllerAxis2DInput(string map, string actionName, Vector2 value, InputActionPhase phase = InputActionPhase.Performed)
    {
        if (this.Map == "" || this.Map == map)
        {
            OnControllerAxis2DAction.Invoke(this, actionName, phase, value);

            foreach (var controllable in PossessedControllables)
            {
                if (controllable.Map == "" || controllable.Map == map)
                {
                    controllable.OnControllerAxis2DInput(actionName, value, phase);

                    OnControllableAxis2DAction.Invoke(controllable, actionName, phase, value);
                }
            }
        }
    }

    /// <summary>
    /// Called by an Input Control Source, passing an Axis 2D Value to the Controllables that the Controller has possessed.
    /// </summary>
    /// <param name="value"></param>
    public virtual void OnControllerButtonInput(string map, string actionName, bool value, InputActionPhase phase = InputActionPhase.Performed)
    {
        if (this.Map == "" || this.Map == map)
        {
            OnControllerButtonAction.Invoke(this, actionName, phase, value);

            foreach (var controllable in PossessedControllables)
            {
                if (controllable.Map == "" || controllable.Map == map)
                {
                    controllable.OnControllerButtonInput(actionName, value, phase);

                    OnControllableButtonAction.Invoke(controllable, actionName, phase, value);
                }
            }
        }
    }

    /// <summary>
    /// Will first check if there is a Controller Prefab with the given Name in Resources to instantiate and Return,
    /// if no prefab exists, it will create a new GameObject with the Controller Component and Return that.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static Controller CreateController(string name = "Default")
    {
        if (ControllerPrefabs.ContainsKey(name))
            return ControllerPrefabs[name].GetComponent<Controller>() ? Instantiate(ControllerPrefabs[name]).GetComponent<Controller>() : new GameObject("Controller_" + name).AddComponent<Controller>();
        return new GameObject("Controller_" + name).AddComponent<Controller>();
    }
}
