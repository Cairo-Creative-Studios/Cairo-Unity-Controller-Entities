using UnityEngine;

public abstract class ControlSource : Object
{
    public abstract ControllerSourceType ControllerSourceType { get; }
    public abstract SerializableDictionary<string, SerializableDictionary<string, ControllerActionValueType>> Actions { get; }
    public abstract Controller Controller { get; set; }
}