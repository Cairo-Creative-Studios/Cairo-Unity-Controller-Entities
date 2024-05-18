using UnityEngine;

public abstract class ControlSource : MonoBehaviour
{
    public abstract ControllerSourceType ControllerSourceType { get; }
    public abstract SerializableDictionary<string, SerializableDictionary<string, ControllerActionValueType>> Actions { get; }
    public abstract Controller Controller { get; set; }
    public abstract void ApplyToController(Controller controller);
}